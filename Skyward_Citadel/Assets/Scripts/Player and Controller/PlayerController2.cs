using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : PhysicsObject
{
    Animator animator;
    public Vector2 startpos;
    public int lives;
    public Text livesText;
    public bool leftFace = true;
    // Start is called before the first frame update

    //Movement Variables
    float frictionMultiplier = 0.6f;        //This will be the rate at which the player's x velocity will slow down. Lower number = Higher Friction
    float frictionMultiplier2 = 0.9f;
    float walkSpeedLimit = 12f;             //This is the limit to the player's basic walking speed. The player will never walk faster than this speed
                                            //This limit can be exceeded by other means, such as dashing or stepping on some sort of booster!
    float walkSpeed = 0.6f;

    float gravityValue = -30.0f;                 //Gravity is initially -9.8 but is increased to make movement less floaty overall

    float minJumpHeight = 7.2f;             //Minimum Height of player jump (When space is held for minimum amount of time)
    float jumpSpeed = 1.7f;
    bool rising = false;                    //This rising variable will turn true if the player is holding space during a jump. Once the player let's go, it will become false again
    int dynamicJumpFrames = 9;              //This variable is how many frames a player may hold space to increase the height of their jump. In other words, it is a player's max jump height.
    int dynamicJumpFramesLeft = 0;          //This variable is how many frames a player has left in a dynamic jump. Once it reaches 0, holding space during a jump will no longer increase jump height.

    public bool currentlyJumping = false;   //Used to keep track of if the player is currently in the middle of a jump and holding space. This is so the player doesn't use all of their double jumps by simply holding space.
    public int bonusJumps = 2;              //Helps keeps track of the max amount of additional jumps a player can make after they initiate a chain of jumps
    public int remainingJumps = 0;          //This variable will keep track of how many jumps a player has left whilst in the middle of a string of jumps. It will refresh back to the "bonusJumps" variable upon touching the ground
    public int spaceDownInt = 0;            //This integer will be 0 when space is not pressed, 1 for a single frame when space is pressed, and then 2 while space is held.
                                            //Upon letting go of space, it will go to 3 for a single frame to signify space being released.
                                            //Finally, it will cycle back from 3 to 0, where space will need to be pressed again to start the cycle over.
    public bool doubleJumpOK = false;       //The last double jump integer is this one, which correlates to the spaceDownInt variable above
                                            //Based on whether or not spaceDownInt has reached the value 1 and then the value 3, doubleJumpOK will be available or unavailable


    public int doubleTapA = 0;
    public int doubleTapD = 0;
    public int aDownInt = 0;
    public int dDownInt = 0;
    public bool dashOK = false;
    int doubleTapWindow = 5;                //This is how many frames the player has to input a double tap for left or right movement
    int dashTimer = 60;                     //This is how many frames the player has to wait upon unleashing a dash, before they may dash again.
    int dashTimerCounter = 0;               //This is the actual variable which will increase until it reaches dashTimer's value;
    float dashSetSpeed = 50.0f;             //This is the velocity the player will have upon inputting a dash
    /*/////////////////////////////////////////////////////////*/





    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        lives = 3;
        livesText.text = lives.ToString() + " lives";
        //animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    public void Movement(Vector2 move, bool movex)
    {
        if (move.magnitude < 0.00001f) return;
        RaycastHit2D[] results = new RaycastHit2D[16];
        int cnt = GetComponent<Rigidbody2D>().Cast(move, results, move.magnitude + 0.09f);
        if (cnt > 0)
        {
            for (int i = 0; i < cnt; ++i)
            {
                CollideWith(results[i].collider);

                if (results[i].collider.isTrigger)
                {
                    CollideWithTrigger(results[i].collider);
                }
                else
                {
                    if (Mathf.Abs(results[i].normal.x) > 0.5 && movex)
                    {
                        move.x = 0;
                        velocity.x = 0;
                        CollideWithHorizontal(results[i].collider);
                    }
                    else if (!movex)
                    {
                        move.y = 0;
                        if (velocity.y < 0) grounded = true;
                        velocity.y = 0;
                        CollideWithVertical(results[i].collider);
                    }
                }
            }
        }

        transform.position += (Vector3)(move);
    }








    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (leftFace)
            {
                leftFace = false;
                ResolveRotation();
            }
            animator.SetBool("Moving", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!leftFace)
            {
                leftFace = true;
                ResolveRotation();
            }
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }



        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            animator.SetBool("Grounded", false);
        }
        else
        {
            animator.SetBool("Grounded", true);
        }



        spaceDown();
        aDown();
        dDown();

        Physics2D.gravity = new Vector3(0, gravityValue, 0);


        walkMotion();
        dashMotion();
        jumpMotion();



        velocity += Physics2D.gravity * Time.deltaTime;

        Vector2 move = velocity * Time.deltaTime;
        grounded = false;


        Movement(new Vector2(move.x, 0), true);
        Movement(new Vector2(0, move.y), false);
    }








    public override void CollideWith(Collider2D other)
    {

    }

    public void enactFriction()
    {
        velocity.x = velocity.x * frictionMultiplier * (1);
        if (-0.8 < velocity.x && velocity.x < 0.8)
        {
            velocity.x = 0;
        }
    }

    public void enactFriction2()
    {
        velocity.x = velocity.x * frictionMultiplier2;
        if (-0.8 < velocity.x && velocity.x < 0.8)
        {
            velocity.x = 0;
        }
    }


    public void dynamicJump()
    {
        if (Input.GetKey(KeyCode.Space) && rising && dynamicJumpFramesLeft > 0)
        {
            velocity.y += (float)jumpSpeed;

            if (dynamicJumpFramesLeft > 0) dynamicJumpFramesLeft = dynamicJumpFramesLeft - 1;
            else dynamicJumpFramesLeft = 0;
        }
        else
        {
            velocity.y = velocity.y * 0.7f;
            rising = false;
            dynamicJumpFramesLeft = 0;
        }
    }

    public void spaceDown()
    {
        if (spaceDownInt == 3)
        {
            spaceDownInt = 0;
        }
        else if (spaceDownInt == 2 && !Input.GetKey(KeyCode.Space))
        {
            spaceDownInt = 3;
        }
        else if (spaceDownInt == 1)
        {
            spaceDownInt = 2;
        }
        else if (Input.GetKey(KeyCode.Space) && spaceDownInt == 0)
        {
            spaceDownInt = 1;
        }
    }

    public void aDown()
    {
        if (aDownInt == 3)
        {
            aDownInt = 0;
        }
        else if (aDownInt == 2 && !Input.GetKey(KeyCode.A))
        {
            aDownInt = 3;
        }
        else if (aDownInt == 1)
        {
            aDownInt = 2;
        }
        else if (Input.GetKey(KeyCode.A) && aDownInt == 0)
        {
            aDownInt = 1;
        }
    }

    public void dDown()
    {
        if (dDownInt == 3)
        {
            dDownInt = 0;
        }
        else if (dDownInt == 2 && !Input.GetKey(KeyCode.D))
        {
            dDownInt = 3;
        }
        else if (dDownInt == 1)
        {
            dDownInt = 2;
        }
        else if (Input.GetKey(KeyCode.D) && dDownInt == 0)
        {
            dDownInt = 1;
        }
    }


    public void jumpMotion()
    {
        if (grounded) remainingJumps = bonusJumps;
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            velocity.y = minJumpHeight;      //This is the minimum jump height. Assuming the player holds the spacebar for the least possible amount of time, the character will at least jump this high.
            rising = true;
            dynamicJumpFramesLeft = dynamicJumpFrames;
        }
        else if (Input.GetKey(KeyCode.Space) && !grounded && remainingJumps > 0 && doubleJumpOK)
        {
            remainingJumps = remainingJumps - 1;
            velocity.y = minJumpHeight;
            rising = true;
            dynamicJumpFramesLeft = dynamicJumpFrames;
        }
        if (rising == true)
        {
            dynamicJump();
        }

        if (spaceDownInt == 1)
        {
            doubleJumpOK = false;
        }
        if (spaceDownInt == 3)
        {
            doubleJumpOK = true;
        }
    }

    public void walkMotion()
    {
        if ((velocity.x > 0 && Input.GetKey(KeyCode.A)) || ((velocity.x < 0) && Input.GetKey(KeyCode.D))) { enactFriction(); }
        //This first if clause is to check if the player is holding a direction opposite to their current velocity.
        //To make turning around and changing direction smoother, we'll enable friction if the player is holding a direction opposite to their current velocity.

        else if (Input.GetKey(KeyCode.A))   //First check if player presses left
        {
            if (velocity.x <= -walkSpeedLimit) { enactFriction2(); }
            else
            {
                velocity.x = velocity.x - walkSpeed;    //The player will speed up towards the left.
            }
        }
        else if (Input.GetKey(KeyCode.D))    //Then check if player is pressing right
        {
            if (velocity.x >= walkSpeedLimit) { enactFriction2(); }
            else
            {
                velocity.x = velocity.x + walkSpeed;    //The player will speed up towards the right.
            }
        }
        else          //This final else statement is for when the player is not holding either left or right!!
        {
            if (velocity.x <= -walkSpeedLimit || velocity.x > walkSpeedLimit) enactFriction2();
            else if (velocity.x != 0) enactFriction();
        }
    }


    public void dashMotion()
    {
        intTimer(ref dashTimerCounter, dashTimer);

        if (aDownInt == 3 || dDownInt == 3)
        {
            dashOK = true;
        }

        if (Input.GetKey(KeyCode.A) && (doubleTapA == 0 || doubleTapA == 1) && dashTimerCounter == 0)
        {
            if (doubleTapD > 0) doubleTapD = 0;
            if (doubleTapA == 0)
            {
                doubleTapA = 1;
            }
        }
        else if (Input.GetKey(KeyCode.A) && doubleTapA > 1 && dashTimerCounter == 0 && dashOK)
        {
            dashTimerCounter = 1;
            velocity.x = -dashSetSpeed;
            doubleTapA = 0;
        }
        else if (doubleTapA > 0)
        {
            intTimer(ref doubleTapA, doubleTapWindow);
        }

        if (Input.GetKey(KeyCode.D) && (doubleTapD == 0 || doubleTapD == 1) && dashTimerCounter == 0)
        {
            if (doubleTapA > 0) doubleTapA = 0;
            if (doubleTapD == 0)
            {
                doubleTapD = 1;
            }
        }
        else if (Input.GetKey(KeyCode.D) && doubleTapD > 1 && dashTimerCounter == 0 && dashOK)
        {
            dashTimerCounter = 1;
            velocity.x = dashSetSpeed;
            doubleTapD = 0;
        }
        else if (doubleTapD > 0)
        {
            intTimer(ref doubleTapD, doubleTapWindow);
        }
    }

    public void intTimer(ref int integer, int timer)
    {
        if (integer >= timer) integer = 0;
        if (integer > 0)
        {
            integer++;
        }

    }


    public void ResolveRotation()
    {
        GetComponent<Rigidbody2D>().transform.Rotate(0f, 180f, 0f);

    }
}
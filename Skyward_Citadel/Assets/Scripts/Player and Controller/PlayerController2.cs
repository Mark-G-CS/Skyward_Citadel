using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : PhysicsObject
{
    public Vector2 startpos;
    public int lives;
    public Text livesText;


    //Movement Variables
    float frictionMultiplier = 0.6f;        //This will be the rate at which the player's x velocity will slow down. Lower number = Higher Friction
    float walkSpeedLimit = 12f;             //This is the limit to the player's basic walking speed. The player will never walk faster than this speed
                                            //This limit can be exceeded by other means, such as dashing or stepping on some sort of booster!
    float walkSpeed = 0.6f;

    float gravityMult = 20.0f;                 //Gravity is initially -9.8 but is multiplied by gravityMult. Values greater than 1 will result in higher gravity, whilst values lower result in lower gravity.

    float jumpSpeed = 1.0f;
    bool rising = false;                    //This rising variable will turn true if the player is holding space during a jump. Once the player let's go, it will become false again
    int dynamicJumpFrames = 9;              //This variable is how many frames a player may hold space to increase the height of their jump. In other words, it is a player's max jump height.
    int dynamicJumpFramesLeft = 0;          //This variable is how many frames a player has left in a dynamic jump. Once it reaches 0, holding space during a jump will no longer increase jump height.

    public bool currentlyJumping = false;   //Used to keep track of if the player is currently in the middle of a jump and holding space. This is so the player doesn't use all of their double jumps by simply holding space.
    public int bonusJumps = 3;              //Helps keeps track of the max amount of additional jumps a player can make after they initiate a chain of jumps
    public int remainingJumps = 0;          //This variable will keep track of how many jumps a player has left whilst in the middle of a string of jumps. It will refresh back to the "bonusJumps" variable upon touching the ground
    public int spaceDownInt = 0;               //This integer will be 0 when space is not pressed, 1 for a single frame when space is pressed, and then 2 while space is held.
                                               //Upon letting go of space, it will go to 3 for a single frame to signify space being released.
                                               //Finally, it will cycle back from 3 to 0, where space will need to be pressed again to start the cycle over.
    public bool doubleJumpOK = false;
    /*/////////////////////////////////////////////////////////*/





    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        lives = 3;
        livesText.text = lives.ToString() + " lives";
        Physics2D.gravity = new Vector3(0, 9.8f * gravityMult, 0);
    }

    public void Movement(Vector2 move, bool movex)
    {
        if (move.magnitude < 0.00001f) return;
        RaycastHit2D[] results = new RaycastHit2D[16];
        int cnt = GetComponent<Rigidbody2D>().Cast(move, results, move.magnitude + 0.01f);
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
                        velocity.y = 0;
                        grounded = true;
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
        spaceDown();

        if ((velocity.x > 0 && Input.GetKey(KeyCode.A)) || ((velocity.x < 0) && Input.GetKey(KeyCode.D))) { enactFriction(); }
        //This first if clause is to check if the player is holding a direction opposite to their current velocity.
        //To make turning around and changing direction smoother, we'll enable friction if the player is holding a direction opposite to their current velocity.

        else if (Input.GetKey(KeyCode.A))   //First check if player presses left
        {
            if (velocity.x <= -walkSpeedLimit) {; /*Do nothing if the player's walk speed exceeds the limit!*/}
            else
            {
                velocity.x = velocity.x - walkSpeed;    //The player will speed up towards the left.
            }
        }
        else if (Input.GetKey(KeyCode.D))    //Then check if player is pressing right
        {
            if (velocity.x >= walkSpeedLimit) { /*Do nothing if the player's walk speed exceeds the limit!*/}
            else
            {
                velocity.x = velocity.x + walkSpeed;    //The player will speed up towards the right.
            }
        }
        else          //This final else statement is for when the player is not holding either left or right!!
        {
            if (velocity.x != 0)
            {
                enactFriction();
            }
        }

        if (grounded) remainingJumps = bonusJumps;
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            velocity.y = 5.5f;      //This is the minimum jump height. Assuming the player holds the spacebar for the least possible amount of time, the character will at least jump this high.
            rising = true;
            dynamicJumpFramesLeft = dynamicJumpFrames;
        }
        else if (Input.GetKey(KeyCode.Space) && !grounded && remainingJumps > 0 && doubleJumpOK)
        {
            remainingJumps = remainingJumps - 1;
            velocity.y = 5.5f;
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

}
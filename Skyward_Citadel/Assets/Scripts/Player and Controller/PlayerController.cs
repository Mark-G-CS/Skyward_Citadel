using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : PhysicsObject
{
    Animator animator;
    public Vector2 startpos;
    public int lives;
    public Text livesText;
    public bool leftFace = true;
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
    // Update is called once per frame
    void Update()
    {
        // Handling horizontal movement
        if (Input.GetAxis("Horizontal") > 0)
        {
            desiredx = 3;
            if (leftFace)
            {
                leftFace = false;
                ResolveRotation();
            }
            animator.SetBool("Moving", true);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            desiredx = -3;
            if (!leftFace)
            {
                leftFace = true;
                ResolveRotation();
            }
            animator.SetBool("Moving", true);
        }
        else
        {
            desiredx = 0;
            animator.SetBool("Moving", false);

        }
        
        // Handling jump
        if (Input.GetButton("Jump") && grounded)
        {
            velocity.y = 6.5f;
            animator.SetBool("Grounded", false);
        }
        else
        {

            animator.SetBool("Grounded", true);
        }
       
    }

    public void ResolveRotation()
    {
        GetComponent<Rigidbody2D>().transform.Rotate(0f, 180f, 0f);

    }


}
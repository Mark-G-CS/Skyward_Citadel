using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : PhysicsObject
{
    public Vector2 startpos;
    public int lives;
    public Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        lives = 3;
        livesText.text = lives.ToString() + " lives";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0) desiredx = 3;
        else if (Input.GetAxis("Horizontal") < 0) desiredx = -3;
        else desiredx = 0;
        if (Input.GetButton("Jump") && grounded)
        {
            velocity.y = 6.5f;
        }
        
       
    }


}
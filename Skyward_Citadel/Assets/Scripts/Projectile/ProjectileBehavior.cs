using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    //The only thing the projectile need to do is 
    //move forward and destroy itself when it hits something. 

    //INSTANCE VARIABLE
    public float Speed = 4.5f;
    [SerializeField] public bool PlayerSource = false;
    [SerializeField] public float LifeSpan = 15f;
    private float timer = 0.0f;
    private void Update()
    {
        //Task 1: now we make it moving forward
        //somehow this line of code relates to the fact that
        //the character and projectile's orientaiton is left BY DEFAULT

        transform.position -= (transform.right) * Time.deltaTime * Speed;
        
        //If the projectile never hits anything, destroy it so it doesnt forever take up resources
        timer += Time.deltaTime; 
        if (timer > LifeSpan)
        {
            Destroy(gameObject);
        }
        //transform.position refers to the current position of the object in the game world
        // += : adds the computed value on the right hand side to the current position. 
        //This means the object is being moved incrementally. 

        //transform.right: represents the object's local right direction
        //(a unit vector pointing to the right of the object, based on its orientation). 

        //-transform.right: negates transform.right, effectively pointing to the left of object. 
        //This means the movement will be towards the left of the object's current orientation. 

        //Time.deltaTime: represents the time (in seconds) that has elapsed since the last frame. 
        //Multiplying by Time.deltaTime ensures the movement is frame-rate indepdendent. 
        //Object moves consistently regardless of the frame rate. 

        //Speed: a variable that controls how fast the object moves. It is a scalar value. 
    }

    //Task 2: It needs to destroy itself when a collision occurs 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //this function will be invoked (triggered) when my 2D object got collision
        if (collision.tag == "Player" && PlayerSource == true || (collision.tag == tag))
        {
         
        }
        else
        {
            if (collision.tag != tag)
            {
                Destroy(gameObject);
            }
        }
 
        
    }

}

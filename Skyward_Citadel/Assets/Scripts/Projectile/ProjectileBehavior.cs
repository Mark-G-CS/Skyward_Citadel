using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    //The only thing the projectile need to do is 
    //move forward and destroy itself when it hits something. 

    //INSTANCE VARIABLE
    public float Speed = 4.5f;

    private void Update()
    {
        //Task 1: now we make it moving forward
        //somehow this line of code relates to the fact that
        //the character and projectile's orientaiton is left BY DEFAULT

        transform.position += (transform.right) * Time.deltaTime * Speed;

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
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        //this function will be invoked (triggered) when my 2D object got collision
        Destroy(gameObject);
        
    }

}

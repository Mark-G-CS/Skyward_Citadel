using Unity.VisualScripting;
using UnityEngine;

public class ChauCharacter2DController : MonoBehaviour
{
    //instance variable
    //speed: we make it public so that we can adjust the movement speed from the editor. 
    public float MovementSpeed = 3f;
    public float JumpForce = 3f;

    public ProjectileBehavior ProjectilePrefab;
    public Transform LaunchOffset;

    private Rigidbody2D myRigidBody;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        
        //check the y-axis to only allow single jump. 
        if(Input.GetButtonDown("Jump") && Mathf.Abs(myRigidBody.linearVelocityY) < 0.001f)
        {
            myRigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        //check if the button "Fire1" is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }
       
    }
}

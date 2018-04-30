using UnityEngine;
using System.Collections;

public class Soveliss : MonoBehaviour
{
    public string logtext = "Hello world again";
    public float speed = 4;
    public float jumpspeed = 4;
    public int allowedAirJumps = 1;

    private int numAirJumps = 0;

    // Use this for initialization
    void Start()
    {
        Debug.Log(logtext);
        Debug.Log("Konnichiwa world");

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Konnichiwa world");
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        //Number beteen -1 and 1 based upon player presing left or right/a or d
        float horizontal = Input.GetAxis("Horizontal");
        Debug.Log(horizontal);

        //Boolean (true or false) based or player pressing space bar
        bool jump = Input.GetButtonDown("Jump");

        //Find out if touching the ground
        //get the collider component attached to this object
        Collider2D collider = GetComponent<Collider2D>();
        //Find out if we are coliding with the ground 
        LayerMask GroundLayer = LayerMask.GetMask("Ground");
        bool touchingGround = collider.IsTouchingLayers(GroundLayer);
        if (touchingGround)
            numAirJumps = 0;
        bool allowedToJump = touchingGround;
        if (allowedAirJumps>numAirJumps)
        {
            allowedToJump = true;
        }

        //Get weather crouching
        bool crouch = Input.GetButton("crouch");

        //get the animator component off of our game object
        Animator animatorComponent = GetComponent<Animator>();
        animatorComponent.SetBool("Crouch", crouch);
        animatorComponent.SetBool("Touching ground", touchingGround);
        //caache a local copy of or rigidbody's velocity
        Vector2 velocity = rigidbody.velocity;

        //set the x (left/right/a/d) components of the velocty bsed on our input
        if (crouch == false)
        {
            velocity.x = horizontal * speed;
        }

        if (crouch == true)
        {
            velocity.x = 0;
        }
        
        //Determin the speed for the animator
        float animatorSpeed = Mathf.Abs(velocity.x);
       
       
        //Set the speed of the Animator
        animatorComponent.SetFloat("Speed", animatorSpeed);

        //Get the sprite compnent from our object
        SpriteRenderer spriteComponent = GetComponent<SpriteRenderer>();

        //set flib based on x velocity
        if (Mathf.Abs(velocity.x) > 0)
        {
            spriteComponent.flipX = (velocity.x < 0); //This will turn true or false
        }


        //set the y (up/down) component of the velocity based on jump
        if (jump == true && allowedToJump == true)
        {
            velocity.y = jumpspeed;
            if (touchingGround != true)
            {
                numAirJumps = numAirJumps + 1;

            }
        }

        bool movingUp = velocity.y > 0;
        animatorComponent.SetBool("Jump", movingUp);

        //set our ridgedbody's velocity based on our local copy
        rigidbody.velocity = velocity;

        //print a log when the mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            //print a log of mouse position
            Vector2 mousePosition = Input.mousePosition;
            Debug.Log("Mouse position is" + mousePosition);

        }


    }
}

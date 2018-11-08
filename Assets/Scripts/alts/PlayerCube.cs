using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube: MonoBehaviour {

    //Movement
    public float moveSpeed = 7f;
    public float moveForce = 3000f;

    public float jumpForce = 100f;

    public Rigidbody playerRigidbody;
    public Collider m_Collider;
    public Vector3 resetPlayerPosition = new Vector3(-6, 1.2f, 0.2f);
   // public Animator animator;
    private Vector3 playerVec3Rotation = new Vector3(0, 0, 0);
    private Quaternion playerStartRotation;
    //Grounding
    private float distanceToGround = 1f;
    private bool isRunning = false;

    private bool leftSideGrounded = false;
    private bool rightSideGrounded = false;

    private bool doubleJump = false;

    private Quaternion lookLeft;
    private Quaternion lookRight;
    private Vector3 moveDirection = Vector3.zero;

    private ClickToCreateGH localClickToCreate;

    private void Awake()
    {
        localClickToCreate = GetComponent<ClickToCreateGH>();
    }



    // Use this for initialization
    void Start()
    {


        //ColliderBounds
        //Fetch the Collider from the GameObject
        m_Collider = GetComponent<Collider>();
       
       

        playerRigidbody = GetComponent<Rigidbody>();
        //abstand zum boden
        distanceToGround = m_Collider.bounds.extents.y;

        playerStartRotation = transform.rotation;

        //animator = GetComponent<Animator>();

        lookRight = transform.rotation;
        lookLeft = lookRight * Quaternion.Euler(0, 180, 0);
    }

    //is player on ground, check which side has to be blocked on air
    bool isGrounded()
    {
        leftSideGrounded = Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), Vector3.down, distanceToGround + 0.1f);
        rightSideGrounded = Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector3.down, distanceToGround + 0.1f);
        return (leftSideGrounded || rightSideGrounded);
    }



    void resetPlayer()
    {
        Debug.Log(playerRigidbody.velocity);
        //reset position
        transform.position = resetPlayerPosition;
        transform.rotation = playerStartRotation;
        //transform.rotation = Quaternion.Euler(playerVec3Rotation);
        //zero velocity
        playerRigidbody.velocity = new Vector3(0, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "DeathZone")
        {
            resetPlayer();
            Debug.Log("player died");
        }
    }

    void Update()
    {
        if((isGrounded() || !doubleJump) && Input.GetButton("Jump"))
        {
          //  GetComponent<Animator>().SetBool("isGrounded", false);
            //animator.SetBool("isGrounded", false);
            playerRigidbody.AddForce(new Vector3(0, jumpForce, 0));

            if (!doubleJump && !isGrounded())
                doubleJump = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
       // GetComponent<Animator>().SetBool("isGrounded", isGrounded());
        
         if (Input.GetButton("MoveRight") && !Input.GetButton("MoveLeft") && isGrounded())
         {
             if (playerRigidbody.velocity.x < moveSpeed)
             {
                 playerRigidbody.AddForce(moveForce * Vector3.right);
                 isRunning = true;
                 transform.rotation = lookRight;

             }
                
         }
         else if (Input.GetButton("MoveLeft") && !Input.GetButton("MoveRight") && isGrounded())
         {
             if (playerRigidbody.velocity.x > moveSpeed * -1)
             {
                 playerRigidbody.AddForce(moveForce * Vector3.left);
                 isRunning = true;
               
                 transform.rotation = lookLeft;
            }
                
         }
         else
        {
            isRunning = false;
        }

       // if(Input.GetButton("Horizontal"))

        if (Input.GetButton("Reset")|| gameObject.tag == "DeathZone")
        {
            resetPlayer();
        }

        //GetComponent<Animator>().SetBool("isRunning", isRunning);
        //var Vector3 movement = new Vector3(Input.GetButton("Horizontal"), transform.position.y, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation())
        moveDirection = new Vector3(-(Input.GetAxis("Vertical")), 0, Input.GetAxis("Horizontal"));



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRB : MonoBehaviour {
    
    
    //Movement
    public float moveSpeed = 7f;
    public float moveForce = 3000f;

    public float jumpForce = 1000f;

    public Rigidbody playerRigidbody;
    public Collider m_Collider;
    public Vector3 resetPlayerPosition = new Vector3(-6, 1.2f, 0.2f);
    public Animator animator;
    private Vector3 playerVec3Rotation = new Vector3(0, 0, 0);
    private Quaternion playerStartRotation;
    //Grounding
    private float distanceToGround = 1f;
    private bool isRunning = false;

    private bool leftSideGrounded = false;
    private bool rightSideGrounded = false;

    private bool doubleJump = false;
    private bool isClambering = false;
    private Quaternion lookLeft;
    private Quaternion lookRight;
    private Vector3 moveDirection = Vector3.zero;

    public ClickToCreateGH localClickToCreate;
    private GrapplingHook gH;

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

        animator = GetComponent<Animator>();
        gH = GetComponent<GrapplingHook>();

        lookRight = transform.rotation;
        lookLeft = lookRight * Quaternion.Euler(0, 180, 0);
    }

    //is player on ground, check which side has to be blocked on air
    bool IsGrounded()
    {
        leftSideGrounded = Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), Vector3.down, distanceToGround + 0.1f);
        rightSideGrounded = Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector3.down, distanceToGround + 0.1f);
        return (leftSideGrounded || rightSideGrounded);
    }

    



    void ResetPlayer()
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
            ResetPlayer();
            Debug.Log("player died");
        }
    }

    void Update()
    {
        if((IsGrounded() || !doubleJump) && Input.GetButton("Jump"))
        {
            animator.SetBool("isGrounded", false);
            playerRigidbody.AddForce(new Vector3(0, jumpForce, 0));

            if (!doubleJump && !IsGrounded())
                doubleJump = true;
        }

        if (gH.IsClambering())
           isClambering = true;        
        else
            isClambering = false;

    }

    private void PlayerMovement()
    {
        animator.SetBool("isClambering", localClickToCreate.grappleDeployed);
        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isRunning", isRunning);
        if (IsGrounded())
        {
            if (localClickToCreate.grappleDeployed == false)
            {
                //transform.rotation = Quaternion.identity;
              //  transform.rotation = playerStartRotation;

                if (Input.GetButton("MoveRight") && !Input.GetButton("MoveLeft"))
                {
                    if (playerRigidbody.velocity.x < moveSpeed)
                    {
                        playerRigidbody.AddForce(moveForce * Vector3.right);
                        isRunning = true;
                        transform.rotation = lookRight;

                    }

                }
                else if (Input.GetButton("MoveLeft") && !Input.GetButton("MoveRight"))
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

                if (Input.GetButton("Reset") || gameObject.tag == "DeathZone")
                {
                    ResetPlayer();
                }
                //var Vector3 movement = new Vector3(Input.GetButton("Horizontal"), transform.position.y, 0);
                //transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation())
                moveDirection = new Vector3(-(Input.GetAxis("Vertical")), 0, Input.GetAxis("Horizontal"));
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {      
        PlayerMovement();
    }
}

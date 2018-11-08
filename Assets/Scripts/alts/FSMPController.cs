using UnityEngine;
public class FSMPController : MonoBehaviour
{

    [Header("Debug Mode Toggle - Draw Line")]
    public bool debugOn = true;
    [Header("Player Control Attributes")]
    Rigidbody rb;
    Collider m_Collider;
    public float speed = 10.0f;
    public float jumpSpeed = 5f;
    public bool isGrounded;
    public Vector3 resetPlayerPosition = new Vector3(-6, 1.2f, 0.2f);
    private float distanceToGround = 0.1f;
    private Quaternion lookLeft;
    private Quaternion lookRight;
    private Quaternion playerStartRotation;
    private Vector3 playerVec3Rotation = new Vector3(0, 0, 0);
    //private bool leftSideGrounded = false;
    //private bool rightSideGrounded = false;
    private GrapplingHook gH;
    private FSMPlayer fsm;
   // private Jump jump;


    void Start()
    {
        fsm = GetComponent<FSMPlayer>();
        m_Collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        //abstand zum boden
        distanceToGround = m_Collider.bounds.extents.y;
        playerStartRotation = transform.rotation;
        lookRight = transform.rotation;
        lookLeft = lookRight * Quaternion.Euler(0, 180, 0);
        gH = GetComponent<GrapplingHook>();
        // rb.freezeRotation = true;
    }

    void OnCollisionStay()
    {
            isGrounded = true;
    }

    void Update()
    {
         
         DrawDebugRays(debugOn);
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal != 0)
        {

            rb.AddForce(new Vector3(moveHorizontal, 0.0f, 0.0f) * speed);
        }
    }

    void MovePlayerAirbourne()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector3(moveHorizontal/2, 0.0f, 0.0f) * speed/2);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //isGrounded = false;
            rb.AddForce(new Vector3(0, 5, 0) * jumpSpeed, ForceMode.Impulse);
        }
        isGrounded = false;
    }

    void Fall()
    {
        if(!isGrounded)
            {

            }
    }

    void DrawDebugRays(bool active)
    {
             if (active)
             {
                 Vector3 forward = transform.TransformDirection(Vector3.right) * 5;
                 Vector3 down = transform.TransformDirection(Vector3.down) * distanceToGround;
                 Debug.DrawRay(transform.position, forward, Color.green); // Draws ray ( length 5 ) so we can see forward direction
                 Debug.DrawRay(transform.position, down, Color.green); // Draws ray ( length 5 ) so we can see forward direction
             }
    }
}

    //void HandleInput(Input input)
    //{
    //    switch (state_)
    //    {
    //
    //        case PlayerState.STATE_JUMPING:
    //            if (input == PRESS_DOWN)
    //            {
    //                state_ = STATE_DIVING;
    //
    //            }
    //            break;
    //
    //        case PlayerState.STATE_FALLING:
    //            if (gH.grappleDeployed)
    //            {
    //                state_ = STATE_STANDING;
    //                setGraphics(IMAGE_STAND);
    //            }
    //            break;
    //
    //        case PlayerState.STATE_GRAPPLE:
    //            if (gH.grappleDeployed)
    //            {
    //                state_ = STATE_STANDING;
    //                setGraphics(IMAGE_STAND);
    //            }
    //            break;
    //
    //        case PlayerState.STATE_IDLE:
    //            if (input == PRESS_B)
    //            {
    //                state_ = STATE_JUMPING;
    //                yVelocity_ = JUMP_VELOCITY;
    //
    //            }
    //            else if (input == PRESS_DOWN)
    //            {
    //                state_ = STATE_FALLING;
    //
    //            }
    //            break;
    //
    //        case PlayerState.STATE_RUNNING:
    //            if (input == PRESS_DOWN)
    //            {
    //                state_ = STATE_DIVING;
    //
    //            }
    //            break;
    //    }
    //}



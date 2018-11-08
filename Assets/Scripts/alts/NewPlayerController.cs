//works but unorganized
using UnityEngine;
public class NewPlayerController : MonoBehaviour
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
   // private bool leftSideGrounded = false;
    //private bool rightSideGrounded = false;
    private GrapplingHook gH;
    //private LineRenderer Lr;
    //private Renderer Rend;
   // private Jump jump;


        //switches from jump to fall immideatelly after jump, since check for isGrounded = false for fall
    public enum PlayerState
    {
        STATE_IDLE,
        STATE_RUNNING,
        STATE_JUMPING,
        STATE_FALLING,
        STATE_GRAPPLE
    };

    public PlayerState state_;

    void PlayerFSM()
    {
        switch (state_)
        {
            case PlayerState.STATE_IDLE:
                Debug.Log("state: Idle");
                
                break;
            case PlayerState.STATE_JUMPING:
                rb.AddForce(new Vector3(0, 5, 0) * jumpSpeed, ForceMode.Impulse);
                isGrounded = false;
                Debug.Log("state: Jumping");
                break;
            case PlayerState.STATE_FALLING:
                MovePlayerAirbourne(0.5f);
                Debug.Log("state: falling");
                break;
            case PlayerState.STATE_RUNNING:
                
                Debug.Log("state: running");
                break;
            case PlayerState.STATE_GRAPPLE:
                MovePlayerAirbourne(1f);
                


                Debug.Log("state: grapple");
                break;




        }
    }

    void Start()
    {
       //Lr = GetComponent<LineRenderer>();
       //Rend = GetComponent<Renderer>();

        state_ = PlayerState.STATE_RUNNING;

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

    void ResetPlayer()
    {
        Debug.Log(rb.velocity);
        //reset position
        transform.position = resetPlayerPosition;
        transform.rotation = playerStartRotation;
        //transform.rotation = Quaternion.Euler(playerVec3Rotation);
        //zero velocity
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            ResetPlayer();
            Debug.Log("player died");
        }
    }

    void Update()
    {
        MovePlayer();
        Jump();
        ResetAction();
        //Fall();
        PlayerFSM();
        DrawDebugRays(debugOn);

        //Lr.SetPosition(0, transform.position);
        //Lr.SetPosition(1, gH.CursorPosition());
        //Rend.material.mainTextureScale = new Vector2((int)Vector2.Distance(transform.position, gH.CursorPosition()));
   
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
         if (moveHorizontal == 0)
            state_ = PlayerState.STATE_IDLE;
        else
        {
            state_ = PlayerState.STATE_RUNNING;
            rb.AddForce(new Vector3(moveHorizontal, 0.0f, 0.0f) * speed);
        }
    }

    void ResetAction()
    {
        if (Input.GetButton("Reset") || gameObject.tag == "DeathZone")
        {
            ResetPlayer();
        }
    }

    void MovePlayerAirbourne(float moveHorizontalAirbourne)
    {
        moveHorizontalAirbourne = Input.GetAxis("Horizontal");
        Debug.Log("moveAirbourne");
        rb.AddForce(new Vector3(moveHorizontalAirbourne / 2, 0.0f, 0.0f) * speed/2);
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            state_ = PlayerState.STATE_JUMPING;
        }

        else if (!isGrounded && !gH.grappleDeployed)
            state_ = PlayerState.STATE_FALLING;
        else if (gH.grappleDeployed)
        {
            isGrounded = false;
            state_ = PlayerState.STATE_GRAPPLE;
        }
    }

   //void Fall()
   //{
   //    if(!isGrounded)
   //        {
   //            state_ = PlayerState.STATE_FALLING;
   //            
   //        }
   //}

    void DrawDebugRays(bool active)
    {
             if (active)
             {
                 Vector3 forward = transform.TransformDirection(Vector3.right) * 5;
                 Vector3 down = transform.TransformDirection(Vector3.down) * distanceToGround * 5;
                 Debug.DrawRay(transform.position, forward, Color.green); // Draws ray ( length 5 ) so we can see forward direction
                 Debug.DrawRay(transform.position, down, Color.green); // Draws ray ( length 5 ) so we can see forward direction
                //Debug.DrawRay(transform.position, gH.HitPoint, Color.black);
                 //draw line from player to mouse position
                 Debug.DrawRay(transform.position, gH.CursorPosition() - transform.position, Color.blue);
        }
    }
}

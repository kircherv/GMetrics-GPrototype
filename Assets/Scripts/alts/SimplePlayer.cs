//not working
//not working
//not working
//not working

using UnityEngine;

public class SimplePlayer : MonoBehaviour
{
    public float speed = 1f;
    public float jumpSpeed = 3f;
    public bool groundCheck;
    public bool isSwinging;
    private MeshRenderer playerSprite;
    private Rigidbody rBody;
    private bool isJumping;
   // private Animator animator;
    private float jumpInput;
    private float horizontalInput;

    void Awake()
    {
        playerSprite = GetComponent<MeshRenderer>();
        rBody = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        jumpInput = Input.GetAxis("Jump");
        horizontalInput = Input.GetAxis("Horizontal");
        var halfHeight = transform.GetComponent<MeshRenderer>().bounds.extents.y;
        groundCheck = Physics.Raycast(new Vector3(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector3.down, 0.025f);
        Debug.DrawRay(new Vector3(transform.position.x,0,0), Vector3.down);
    }

    void FixedUpdate()
    {
        if (horizontalInput < 0f || horizontalInput > 0f)
        {
           // animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
           // playerSprite.flipX = horizontalInput < 0f;

            if (groundCheck)
            {
                var groundForce = speed * 2f;
                rBody.AddForce(new Vector3((horizontalInput * groundForce - rBody.velocity.x) * groundForce, 0));
                rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y);
            }
        }


        if (!groundCheck) return;

        isJumping = jumpInput > 0f;
        if (isJumping)
        {
            rBody.velocity = new Vector3(rBody.velocity.x, jumpSpeed);
        }

    }

}

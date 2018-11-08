using UnityEngine;
public class FSMPlayer : MonoBehaviour
{



    public enum PlayerState
    {
        STATE_IDLE,
        STATE_RUNNING,
        STATE_JUMPING,
        STATE_FALLING,
        STATE_GRAPPLE
    };

    public PlayerState state_;

    void Start()
    {
        state_ = PlayerState.STATE_IDLE;

    }

    void Update()
    {
        switch (state_)
        {
            case PlayerState.STATE_IDLE:
                Debug.Log("state: Idle");

                break;
            case PlayerState.STATE_JUMPING:
                // Jump();
                Debug.Log("state: Jumping");
                break;
            case PlayerState.STATE_FALLING:
                // Fall();
                Debug.Log("state: falling");
                break;
            case PlayerState.STATE_RUNNING:
                // MovePlayer();
                Debug.Log("state: running");
                break;
            case PlayerState.STATE_GRAPPLE:
                // if (gH.grappleDeployed)
                //     MovePlayer();


                Debug.Log("state: grapple");
                break;




        }
    }
}

   



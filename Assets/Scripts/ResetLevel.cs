//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class ResetLevel : MonoBehaviour {
//
//    public Rigidbody playerRigidbody;
//    public Vector3 resetPlayerPosition = new Vector3(-6, 1.2f, 0);
//
//    void Start()
//    {
//        playerRigidbody = GetComponent<Rigidbody>();
//    }
//
//    // Update is called once per frame
//    void FixedUpdate ()
//    {
//		if(Input.GetButton("Reset"))
//        {
//            resetPlayer();
//        }
//	}
//
//    void resetPlayer()
//    {
//        Debug.Log(playerRigidbody.velocity);
//        //reset position
//        transform.position = resetPlayerPosition;
//         //zero velocity
//         playerRigidbody.velocity = new Vector3(0, 0, 0);        
//    }
//}

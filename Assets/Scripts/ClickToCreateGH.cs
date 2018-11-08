using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToCreateGH : MonoBehaviour {

    //plaziere auf cursorpoint
    public GameObject cursorMarkerObject;
    public Object oldCursorMarkerObject;
    public bool grappleDeployed = false;

    //baue plane auf deren normale auf die camera zeigt
    private Plane zEqualsZero = new Plane(new Vector3(0, 0, -1), new Vector3(0, 0, 0));
    //distance to z=0 plane
    private float distanceFromCameraToZEqualsZero = 0f;
    private float ropeLength;
    private HingeJoint myRope;
    public float retractionSpeed = 0.3f;
    private Vector3 directionToGrapple = new Vector3(0,0,0);
    private Vector3 grapplePoint = new Vector3(0, 0, 0);

    public Vector3 CursorPosition()
    {
            //raytrace von camera auf cursor
            Ray rayFromCameratoMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Use a function of this plane to set that variable to the distance along the ray that it intersects
            zEqualsZero.Raycast(rayFromCameratoMouse, out distanceFromCameraToZEqualsZero);
             
            return rayFromCameratoMouse.GetPoint(distanceFromCameraToZEqualsZero);
    }
    //set 


    public void UpdateGrappleAncher()
    {
        if(GetComponent<HingeJoint>())
        {
           // hingeJoint.anch
        }
    }


	// Update is called once per frame
	void Update ()
    {
        //onClick
        if (Input.GetMouseButtonDown(0))
        //if(Input.GetButtonDown("Click"))
        {

            

            //create object in this position
            Instantiate(cursorMarkerObject, CursorPosition(), transform.rotation);
        }
	}
}

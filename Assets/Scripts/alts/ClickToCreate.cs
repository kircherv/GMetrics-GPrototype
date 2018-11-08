using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToCreate : MonoBehaviour {

    //baue plane auf deren normale auf die camera zeigt
    private Plane zEqualsZero = new Plane(new Vector3(0, 0, -1), new Vector3(0, 0, 0));

    //plaziere auf cursorpoint
    public GameObject cursorMarkerObject;


	// Update is called once per frame
	void Update ()
    {
        //onClick
        if (Input.GetMouseButtonDown(0))
        //if(Input.GetButtonDown("Click"))
        {
            //raytrace von camera auf cursor
            Ray rayFromCameratoMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

            //distance to z=0 plane
            float distanceFromCameraToZEqualsZero = 0f;

            //Use a function of this plane to set that variable to the distance along the ray that it intersects
            zEqualsZero.Raycast(rayFromCameratoMouse, out distanceFromCameraToZEqualsZero);

            //Define a new vector as the position given by following our ray by the distance we just found
            Vector3 worldPositionOfCursor = rayFromCameratoMouse.GetPoint(distanceFromCameraToZEqualsZero);

            //create object in this position
            Instantiate(cursorMarkerObject, worldPositionOfCursor, transform.rotation);
        }
	}
}

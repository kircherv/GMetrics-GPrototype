using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {

    //Crosshair lags on player movement
    public GameObject player;
    public GameObject crosshair;
    //public float radius = 40f;
    //private Vector3 centerPosition;
    //private Vector3 newLocation;
    //private float distance;
    //private Vector3 fromOriginToObject;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
       // player = GetComponent<GameObject>();
       // centerPosition = transform.localPosition;
       // distance = Vector3.Distance(newLocation, centerPosition);
        

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        ////if(CursorPosition()-player.transform.position>maxDistance)
        //fromOriginToObject = newLocation - centerPosition;
        //fromOriginToObject *= radius / distance;
        //newLocation = centerPosition + fromOriginToObject;
        //            //
        //crosshair.transform.position = newLocation;
        CursorPosition();
        crosshair.transform.position = CursorPosition();
    }

    public Vector3 CursorPosition()
    {
        //raytrace von camera auf cursor
        Ray rayFromCameratoMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        //baue plane auf deren normale auf die camera zeigt
        //Use a function of this plane to set that variable to the distance along the ray that it intersects
        Plane zEqualsZero = new Plane(new Vector3(0, 0, -1), new Vector3(0, 0, 0));
        //distance to z=0 plane
        float distanceFromCameraToZEqualsZero = 0f;
        zEqualsZero.Raycast(rayFromCameratoMouse, out distanceFromCameraToZEqualsZero);

        return rayFromCameratoMouse.GetPoint(distanceFromCameraToZEqualsZero);
    }
}

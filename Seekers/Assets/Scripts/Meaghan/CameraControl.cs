using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    //Variables
    public float rotateSpeed = 10.0f;
    public bool debug;
    public Transform target;
    public float height;
    public float distance;
    private Vector3 idealPos;
    public float cameraSpeed;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (debug == true)
        {
            //Rotate the camera
            transform.Rotate(Input.GetAxis("RightHorizontal") * Vector3.up * Time.deltaTime * rotateSpeed);
            transform.Rotate(Input.GetAxis("RightVertical") * Vector3.right * Time.deltaTime * rotateSpeed);
        }
    }

    void LateUpdate()
    {
        if (debug == false)
        {
            //Set the ideal pos is the target's position
            Vector3 targetForward = target.forward;
            targetForward.y = 0.0f;

            idealPos = target.position + Vector3.up * height - targetForward * distance;

            //Move the camera based on idealPos
            transform.position = idealPos;// Vector3.Lerp(transform.position, idealPos, Time.deltaTime * cameraSpeed);

            Vector3 vecToPlayer = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(vecToPlayer.normalized); //point camera z along vector to player
        }
    }
}

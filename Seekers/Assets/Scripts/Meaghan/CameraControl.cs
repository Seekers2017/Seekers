using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    //Variables

    [Tooltip("Decreases camera jitter.")]
    [SerializeField]
    private float smoothing = 100f;
    [Tooltip("Smooths the camera turning on a drift")]
    [SerializeField]
    private float driftSmoothing = 0.2f;
    [Tooltip("Camera position in the world.")]
    [SerializeField]
    private Transform positionTarget;
    [Tooltip("What the camera should look at.")]
    [SerializeField]
    private Transform lookAtTarget;
    [Tooltip("What the camera should look at while drifting.")]
    [SerializeField]
    private Transform driftTarget;
    [Tooltip("Camera's Original Position")]
    [SerializeField]
    private Transform originalPos;


    private WheelDrive playerWheelDrive;
    private bool playerHasDrifted;

    // Use this for initialization
    void Start ()
    {
        playerWheelDrive = GameObject.FindGameObjectWithTag("Player").GetComponent<WheelDrive>();
	}
	
    void LateUpdate()
    {
        //Set the ideal pos is the target's position
        transform.position = Vector3.Lerp(transform.position, positionTarget.position, Time.deltaTime * smoothing);


       ////Altering the camera for a drift
       //if (playerWheelDrive.Drifting == false)
       //{ 
       //    if(playerHasDrifted == false)
       //    {
       //        //Set the ideal pos is the target's position
       //        transform.position = Vector3.Lerp(transform.position, positionTarget.position, Time.deltaTime * smoothing);
       //    }
       //    else
       //    {
       //        //Set the ideal pos is the target's position
       //        transform.position = Vector3.Lerp(transform.position, positionTarget.position, Time.deltaTime * smoothing);
       //
       //        //Lerp the position to the drift camera position
       //        positionTarget.transform.position = Vector3.Lerp(positionTarget.position, originalPos.transform.position, Time.deltaTime * driftSmoothing);
       //    }
       //}
       //else
       //{
       //    //Set the ideal pos is the target's position
       //    transform.position = Vector3.Lerp(transform.position, positionTarget.position, Time.deltaTime * smoothing);
       //
       //    //Lerp the position to the drift camera position
       //    positionTarget.transform.position = Vector3.Lerp(positionTarget.position, driftTarget.position, Time.deltaTime * driftSmoothing);
       //
       //    playerHasDrifted = true;
       //}

        transform.LookAt(lookAtTarget);
    }
}

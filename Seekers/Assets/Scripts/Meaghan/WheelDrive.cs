using UnityEngine;
using System;

[Serializable]
public enum DriveType
{
	RearWheelDrive,
	FrontWheelDrive,
	AllWheelDrive
}

public class WheelDrive : MonoBehaviour
{
    //TODO:
    //Private SF on public variables

    [Tooltip("Maximum steering angle of the wheels.")]
	public float maxAngle = 30f;
	[Tooltip("Maximum torque applied to the driving wheels.")]
	public float torque = 1000f;
	[Tooltip("Maximum brake torque applied to the driving wheels.")]
	public float brakeTorque = 30000f;
    [Tooltip("Increases the speed of the car after a boost is consumed.")]
    public float boostSpeed = 1500.0f;
    [Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
	public GameObject wheelShape;

	[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
	public float criticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
	public int stepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
	public int stepsAbove = 1;
    [Tooltip("Increases the drag of the car while not moving")]
    public float dragAmount;
    [Tooltip("Adds a drag to the drift")]
    public float driftDrag = 2.5f;

    [Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
	public DriveType driveType;

    private WheelCollider[] wheels;
    private float speed;
    private bool drifting;


    private PlayerManager player;
    private Rigidbody carRigidbody;


	void Start()
	{
        //Various stuff
        player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerManager>();
        wheels = GetComponentsInChildren<WheelCollider>();
        carRigidbody = GetComponent<Rigidbody>();
        drifting = false;

        //Create the wheels
		for (int i = 0; i < wheels.Length; ++i) 
		{
			var wheel = wheels [i];

			// Create wheel shapes only when needed.
			if (wheelShape != null)
			{
				var ws = Instantiate (wheelShape);
				ws.transform.parent = wheel.transform;
			}
		}
	}

	void Update()
    { 
        //Changes speed based on criticals 
		wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

        //Alters the angle of the car
		float angle = maxAngle * Input.GetAxis("Horizontal");

		foreach (WheelCollider wheel in wheels)
		{
            

            //Maintain the RPM
            if (wheel.rpm > 500)
                wheel.motorTorque = 0;


            if(Input.GetAxis("Right Trigger") == 1)
            {
                drifting = true;
                if (wheel.transform.localPosition.z > 0)
                    wheel.steerAngle = 0.0f;
            }
            else
            {
                drifting = false;
                if (wheel.transform.localPosition.z < 0)
                    wheel.steerAngle = 0.0f;
            }

            // A simple car where front wheels steer while rear ones drive
            if(drifting == false)
            {
                if (wheel.transform.localPosition.z > 0)
                    wheel.steerAngle = angle;
            }
            else
            {
                if (wheel.transform.localPosition.z < 0)
                    wheel.steerAngle = -angle * driftDrag;
            }
            

            //Checking if they are boosting, increase torque
            if(player.IsBoosting == true)
            {
                speed = boostSpeed;
            }
            else
            {
                speed = torque;
            }

            //If we are holding down the A button, move forward
            if(Input.GetButton("Fire1"))
            {
                //Allow the car to move
                wheel.brakeTorque = 0.0f;
                carRigidbody.drag = 0.0f;
                //Back wheels
                if (wheel.transform.localPosition.z < 0 && driveType != DriveType.FrontWheelDrive)
                {
                    wheel.motorTorque = speed;
                }

                //Front wheels
                if (wheel.transform.localPosition.z >= 0 && driveType != DriveType.RearWheelDrive)
                {
                    wheel.motorTorque = speed;
                }
            }
            else
            {
                wheel.motorTorque = 0.0f;
                wheel.brakeTorque = Mathf.Infinity;
                carRigidbody.drag = dragAmount;
            }
			

			// Update visual wheels if any
			if (wheelShape) 
			{
                if(drifting == false)
                {
                    Quaternion q;
                    Vector3 p;
                    wheel.GetWorldPose(out p, out q);

                    // Assume that the only child of the wheelcollider is the wheel shape.
                    Transform shapeTransform = wheel.transform.GetChild(0);
                    shapeTransform.position = p;
                    shapeTransform.rotation = q;
                }
				
			}
		}
	}
}

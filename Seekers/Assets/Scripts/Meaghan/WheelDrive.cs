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
    [Tooltip("Increases the drag of the car while not moving.")]
    public float dragAmount;
    [Tooltip("Increases the drift angle.")]
    public float driftAngle = 2.5f;
    [Tooltip("Increases the drift drag when it is released.")]
    public float driftDrag = 2.5f;
    [Tooltip("The maximum time for the timer that controls the drag after drifting.")]
    public float releaseTimerMax = 1f;
    [Tooltip("Alters the angle of the front tyres while drifting.")]
    public float frontTyreDriftAngle;

    [Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
	public DriveType driveType;

    private WheelCollider[] wheels;
    private float speed;
    private bool drifting;
    private bool releaseDrift;
    private float releaseTimer;
    private bool storeBackAngle;
    private float driftWheelAngle;


    private PlayerManager player;
    private Rigidbody carRigidbody;


    public bool Drifting
    {
        get { return drifting; }
    }


	void Start()
	{
        //Various stuff
        player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerManager>();
        wheels = GetComponentsInChildren<WheelCollider>();
        carRigidbody = GetComponent<Rigidbody>();
        drifting = false;
        storeBackAngle = true;

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

		for(int wheelNum = 0; wheelNum < 4; ++wheelNum)
		{
            WheelCollider wheel = wheels[wheelNum];
            bool frontWheel = (wheelNum < 2); //if wheelnum is 0 or 1, it's a front wheel.

            //Maintain the RPM
            if (wheel.rpm > 500)
                wheel.motorTorque = 0;


            if(Input.GetAxis("Right Trigger") == 1)
            {
                //Allow the car to drift with no drag
                drifting = true;
                carRigidbody.angularDrag = 0.0f;
                releaseTimerMax = 0.0f;

                //Collect the back wheel angle
                if (storeBackAngle == true)
                {
                    driftWheelAngle = -angle * driftAngle;
                    storeBackAngle = false;
                }

                //Update the back wheel angles
                if (!frontWheel)
                    wheel.steerAngle = driftWheelAngle;
            }
            else
            {
                //Reset values and start the drift drag timer
                drifting = false;
            }

            //Update the front wheel angles
            if (drifting == false)
            {
                if (frontWheel)
                    wheel.steerAngle = angle;

                //Reset the drift angles
                driftWheelAngle = 0.0f;
                storeBackAngle = true;
                if (!frontWheel)
                    wheel.steerAngle = 0.0f;

                //Start timer
                releaseTimer += Time.deltaTime;

                if (releaseTimer > releaseTimerMax)
                    releaseDrift = true;
            }
            else
            {
                if (frontWheel)
                    wheel.steerAngle = angle * frontTyreDriftAngle;
            }

            //Change the angular drag depending on whether or not you have just released the drift button
            if (releaseDrift == true)
                carRigidbody.angularDrag = driftDrag;
            else
                carRigidbody.angularDrag = 0.0f;

            //Checking if they are boosting, increase torque
            if (player.IsBoosting == true)
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
                carRigidbody.drag = 0.0f;
              

                //Back wheels
                if (!frontWheel && driveType != DriveType.FrontWheelDrive)
                {
                    wheel.motorTorque = speed;
                }

                //Front wheels
                if (frontWheel && driveType != DriveType.RearWheelDrive)
                {
                    wheel.motorTorque = speed;
                }
            }
            else if (Input.GetButton("Fire2"))
            {
                //Allow the car to move
                carRigidbody.drag = 0.0f;


                //Back wheels
                if (!frontWheel && driveType != DriveType.FrontWheelDrive)
                {
                    wheel.motorTorque = -speed;
                }

                //Front wheels
                if (frontWheel && driveType != DriveType.RearWheelDrive)
                {
                    wheel.motorTorque = -speed;
                }
            }
            else
            {
                wheel.motorTorque = 0.0f;
                carRigidbody.drag = dragAmount;
            }


			

			// Update visual wheels if any
			if (wheelShape) 
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

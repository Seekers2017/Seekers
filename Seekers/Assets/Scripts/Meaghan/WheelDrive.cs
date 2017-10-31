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
    //Fix reverse


    [Tooltip("Maximum steering angle of the wheels.")]
    [SerializeField]
    private float maxAngle = 30f;
	[Tooltip("Maximum torque applied to the driving wheels.")]
    [SerializeField]
    private float torque = 1000f;
	[Tooltip("Maximum brake torque applied to the driving wheels.")]
    [SerializeField]
    private float brakeTorque = 30000f;
    [Tooltip("Increases the speed of the car after a boost is consumed.")]
    [SerializeField]
    private float boostSpeed = 1500.0f;
    [Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
    [SerializeField]
    private GameObject wheelShape;

	[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
    [SerializeField]
    private float criticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
    [SerializeField]
    private int stepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
    [SerializeField]
    private int stepsAbove = 1;
    [Tooltip("Increases the drag of the car while not moving.")]
    [SerializeField]
    private float dragAmount;
    [Tooltip("Increases the drift angle.")]
    [SerializeField]
    private float driftAngle = 2.5f;
    [Tooltip("Increases the drift drag when it is released.")]
    [SerializeField]
    private float driftDrag = 2.5f;
    [Tooltip("The maximum time for the timer that controls the drag after drifting.")]
    [SerializeField]
    private float releaseTimerMax = 1f;
    [Tooltip("Alters the angle of the front tyres while drifting.")]
    [SerializeField]
    private float frontTyreDriftAngle;
    [Tooltip("The force of the forward momentum during a drift.")]
    [SerializeField]
    private float forwardForce = 1000f;

    [Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
    [SerializeField]
    private DriveType driveType;


    private WheelCollider[] wheels;
    private float speed;
    private bool drifting;
    private bool releaseDrift;
    private float releaseTimer;
    private bool storeBackAngle;
    private float driftWheelAngle;
    private bool abilityToDrive;
    private float angleStore;

    private PlayerManager player;
    private Rigidbody carRigidbody;


    public bool Drifting
    {
        get { return drifting; }
    }

    public bool AbilityToDrive
    {
        get { return abilityToDrive; }
        set { abilityToDrive = value; }
    }

    void Start()
	{
        //Various stuff
        player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerManager>();
        carRigidbody = GetComponent<Rigidbody>();
        drifting = false;
        releaseDrift = false;
        releaseTimer = releaseTimerMax;
        wheels = gameObject.GetComponentsInChildren<WheelCollider>();

        storeBackAngle = true;
        abilityToDrive = true;

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



        if(abilityToDrive)
        {
            //Changes speed based on criticals 
            wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

            //Alters the angle of the car
            float angle = maxAngle * Input.GetAxis("Horizontal");

            for (int wheelNum = 0; wheelNum < 4; ++wheelNum)
            {
                WheelCollider wheel = wheels[wheelNum];
                bool frontWheel = (wheelNum < 2); //if wheelnum is 0 or 1, it's a front wheel.

                //Maintain the RPM
                if (wheel.rpm > 500)
                    wheel.motorTorque = 0;


                if (Input.GetAxis("Right Trigger") == 1)
                {
                    //Allow the car to drift with no drag
                    drifting = true;
                    carRigidbody.angularDrag = 3.5f;
                    releaseDrift = false;
                    releaseTimer = 0.0f;

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

                    if (releaseTimer < releaseTimerMax)
                        releaseDrift = true;
                    else 
                        releaseDrift = false;
   
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
                if (Input.GetButton("Fire1"))
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
}

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

    [Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
    [SerializeField]
    private DriveType driveType;


    private WheelCollider[] wheels;
    private float speed;
    private bool abilityToDrive;

    private PlayerManager player;
    private Rigidbody carRigidbody;



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
		//carRigidbody.centerOfMass = new Vector3 (0f, -0.2f, 0f);
        wheels = gameObject.GetComponentsInChildren<WheelCollider>();
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

                //Update the front wheel angles
                if (frontWheel)
                    wheel.steerAngle = angle;

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

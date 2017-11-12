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
    //Fix speed of the car
    /// <summary>
    /// ////////////
    /// </summary>
    //ROTATION OF THE WHEELS

    [Tooltip("Maximum steering angle of the wheels.")]
    [SerializeField]
    private float maxAngle = 30f;
	[Tooltip("Maximum torque applied to the driving wheels.")]
    [SerializeField]
    private float torque = 1000f;
	[Tooltip("Maximum brake torque applied to the driving wheels.")]
    [SerializeField]
    private float brakeTorque = 15000f;
    [Tooltip("Increases the speed of the car after a boost is consumed.")]
    [SerializeField]
    private float boostSpeed = 1500.0f;
    [Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
    [SerializeField]
    private GameObject wheelShape;


    [Tooltip("Increases the drag of the car while not moving.")]
    [SerializeField]
    private float dragAmount;
	[Tooltip("The amount that the back wheels rotate during a sharp turn.")]
	[SerializeField]
	private float sharpTurn = 1.2f;

    [Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
    [SerializeField]
    private DriveType driveType;

    private float idealRPM = 500f;
    private float maxRPM = 1000f;


    private WheelCollider[] wheels;

    private float speed;
    private bool abilityToDrive;
    private float criticalSpeed = 5f;
    private int stepsBelow = 5;
    private int stepsAbove = 1;

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
        wheels = gameObject.GetComponentsInChildren<WheelCollider>();
        abilityToDrive = true;

       //Create the wheels
		for (int i = 0; i < wheels.Length; ++i) 
		{
			// Create wheel shapes only when needed
			if (wheelShape != null)
			{
				var ws = Instantiate (wheelShape);
                ws.transform.parent = wheels[i].transform;
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
                //Get the wheel colliders
                WheelCollider wheel = wheels[wheelNum];
                bool frontWheel = (wheelNum < 2); //if wheelnum is 0 or 1, it's a front wheel
                bool frontLeftWheel = (wheelNum == 0); //Left front wheel
                bool backLeftWheel = (wheelNum == 2); //Left back wheel 

                //Maintain the RPM
                if (wheel.rpm < idealRPM)
                    speed = Mathf.Lerp(speed / 10f, speed, wheel.rpm / idealRPM);
                else
                    speed = Mathf.Lerp(speed, 0, (wheel.rpm - idealRPM) / (maxRPM - idealRPM));

                //Checking if they are boosting, increase torque
                if (player.IsBoosting == true)
                {
                    speed = boostSpeed;
                }
                else
                {
                    speed = torque;
                }

                //Sharp turn
                if (Input.GetAxis("Right Trigger") == 1)
                {
                    if (!frontWheel)
						wheel.steerAngle = -angle * sharpTurn;
                    else
                        wheel.steerAngle = angle * 1.2f;
                }
                else
                {
                    if (!frontWheel)
                        wheel.steerAngle = 0.0f;
                    else
                        wheel.steerAngle = angle;
                }


                //If we are holding down the A button, move forward
                if (Input.GetButton("Fire1"))
                {
                    wheel.brakeTorque = 0.0f;

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

                    wheel.brakeTorque = 0.0f;

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
                    wheel.brakeTorque = brakeTorque;
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

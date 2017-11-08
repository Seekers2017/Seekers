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
    //Break problem

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


    [Tooltip("Increases the drag of the car while not moving.")]
    [SerializeField]
    private float dragAmount;
	[Tooltip("The amount that the back wheels rotate during a sharp turn.")]
	[SerializeField]
	private float sharpTurn = 1.2f;
    [Tooltip("The limit that the ")]
    [SerializeField]
    private float rpmLimit = 100000f;

    [Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
    [SerializeField]
    private DriveType driveType;

    [Tooltip("The ideal rotation for the wheels (effects the speed.)")]
    [SerializeField]
    private float idealRPM = 750f;
    [Tooltip("The max rotation for the wheels (effects the speed.)")]
    [SerializeField]
    private float maxRPM = 1250;


    private WheelCollider[] wheels;

    private float speed;
    private bool abilityToDrive;
    private float criticalSpeed = 5f;
    private int stepsBelow = 5;
    private int stepsAbove = 1;
    private bool turning;

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
        turning = false;
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

                UpdateWheelAnimation(wheel);
                Movement(wheel, frontWheel);
                SharpTurn(wheel, frontWheel, angle);
            }
        }  
	}

    void UpdateWheelAnimation(WheelCollider a_wheel)
    {
        // Update visual wheels
        Quaternion q;
        Vector3 p;

        a_wheel.GetWorldPose(out p, out q);
        // Assume that the only child of the wheelcollider is the wheel shape.
        Transform shapeTransform = a_wheel.transform.GetChild(0);

        shapeTransform.position = p;
        shapeTransform.rotation = q;
    }

    void Movement(WheelCollider a_wheel, bool a_frontWheel)
    {

        //If we are holding down the A button, move forward
        if (Input.GetButton("Fire1"))
        {
            a_wheel.brakeTorque = 0.0f;

            //Back wheels
            if (!a_frontWheel && driveType != DriveType.FrontWheelDrive)
            {
                a_wheel.motorTorque = speed;
            }

            //Front wheels
            if (a_frontWheel && driveType != DriveType.RearWheelDrive)
            {
                a_wheel.motorTorque = speed;
            }
        }
        else if (Input.GetButton("Fire2"))
        {
            a_wheel.brakeTorque = 0.0f;

            //Back wheels
            if (!a_frontWheel && driveType != DriveType.FrontWheelDrive)
            {
                a_wheel.motorTorque = -speed;
            }

            //Front wheels
            if (a_frontWheel && driveType != DriveType.RearWheelDrive)
            {
                a_wheel.motorTorque = -speed;
            }
        }
        else
        {
            //Stop moving
            a_wheel.motorTorque = 0.0f;
            a_wheel.brakeTorque = brakeTorque;
        }
    }

    void SharpTurn(WheelCollider a_wheel, bool a_frontWheel, float a_angle)
    {
        //Sharp turn
        if (Input.GetAxis("Right Trigger") == 1)
        {
            //Turn the back wheels
            if (!a_frontWheel)
                a_wheel.steerAngle = -a_angle * sharpTurn;
            else
                a_wheel.steerAngle = 0.0f;

            turning = true;
        }
        else
        {
            //Normal steering
            if (!a_frontWheel)
                a_wheel.steerAngle = 0.0f;
            else
                a_wheel.steerAngle = a_angle;

            turning = false;
        }
    }
}

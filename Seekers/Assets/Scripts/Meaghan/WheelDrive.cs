using UnityEngine;
using System;
using XboxCtrlrInput;

[Serializable]
public enum DriveType
{
	RearWheelDrive,
	FrontWheelDrive,
	AllWheelDrive
}


public class WheelDrive : MonoBehaviour
{
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

    [Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
    [SerializeField]
    private DriveType driveType;

    [SerializeField]
    private XboxController controller;

    [SerializeField]
    private AudioSource driving;

    [Tooltip("Ideal rotation of the wheels of the car.")]
    [SerializeField]
    private float idealRPM = 500f;
    [Tooltip("The max rotation of the wheels of the car.")]
    [SerializeField]
    private float maxRPM = 1000f;
    [Tooltip("Traction of the car.")]

    private WheelCollider[] wheels;

    private float speed;
    private bool abilityToDrive;
    private float criticalSpeed = 5f;
    private int stepsBelow = 5;
    private int stepsAbove = 1;
    private bool aHasBeenPressed;
    private float brakeTimer;



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
        player = GetComponent<PlayerManager>();
        carRigidbody = GetComponent<Rigidbody>();
        wheels = gameObject.GetComponentsInChildren<WheelCollider>();
        abilityToDrive = true;
        aHasBeenPressed = false;

        player.dustParticleLeft.Stop();
        player.dustParticleRight.Stop();
        driving.Stop();

    }

	void Update()
    { 

        if(abilityToDrive)
        {
            //Alters the angle of the car
            float angle = maxAngle * XCI.GetAxis(XboxAxis.LeftStickX, controller);

            for (int wheelNum = 0; wheelNum < 4; ++wheelNum)
            {
                //Get the wheel colliders
                WheelCollider wheel = wheels[wheelNum];
                bool frontWheel = (wheelNum < 2); //if wheelnum is 0 or 1, it's a front wheel
                bool frontLeftWheel = (wheelNum == 0); //Left front wheel
                bool backLeftWheel = (wheelNum == 2); //Left back wheel 

                //Changes speed based on criticals 
                wheel.ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

                //If the player is not boosting
                if(player.IsBoosting == false)
                {
                    //Maintain the RPM
                    if (wheel.rpm < idealRPM)
                        speed = Mathf.Lerp(speed / 10f, speed, wheel.rpm / idealRPM);
                    else
                        speed = Mathf.Lerp(speed, 0, (wheel.rpm - idealRPM) / (maxRPM - idealRPM));
                }

                //Checking if they are boosting, increase torque
                if (player.IsBoosting == true)
                {
                    //Boost speed
                    speed = boostSpeed;
                }
                else
                {
                    //Regular speed
                    speed = torque;
                }

                //Sharp turn
                if (XCI.GetAxis(XboxAxis.RightTrigger, controller) == 1)
                {
                    //Move the back wheels
                    if (!frontWheel)
                        wheel.steerAngle = -angle * sharpTurn;
                    else
                        wheel.steerAngle = 0.0f;
                }
                else
                {
                    //Move the front wheels
                    if (!frontWheel)
                        wheel.steerAngle = 0.0f;
                    else
                        wheel.steerAngle = angle;
                }


                //If we are holding down the A button, move forward
                if (XCI.GetButton(XboxButton.A, controller))
                {             
                    //Set variables   
                    wheel.brakeTorque = 0.0f;
                    carRigidbody.drag = 1.0f;
                    carRigidbody.isKinematic = false;
                    aHasBeenPressed = true;
                    player.dustParticleLeft.Play();
                    player.dustParticleRight.Play();
                    driving.Play();

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
                else if (XCI.GetButton(XboxButton.B, controller))
                {

                    if(aHasBeenPressed == true)
                    {
                        brakeTimer += Time.deltaTime;

                        if(brakeTimer < 1.5f)
                        {
                            if (wheel.isGrounded == true)
                            {
                                //Stop the car moving and resetting values
                                wheel.motorTorque = 0.0f;
                                wheel.brakeTorque = Mathf.Infinity;
                                carRigidbody.drag = dragAmount;
                                carRigidbody.isKinematic = false;
                                player.dustParticleLeft.Stop();
                                player.dustParticleRight.Stop();
                                driving.Stop();
                            }
                            else
                            {
                                player.dustParticleLeft.Stop();
                                player.dustParticleRight.Stop();
                                driving.Stop();
                            }
                        }
                        else
                        {
                            //Use the reverse function
                            aHasBeenPressed = false;
                        }
                    }
                    else
                    {
                        //Set variables
                        wheel.brakeTorque = 0.0f;
                        carRigidbody.drag = 1.0f;
                        carRigidbody.isKinematic = false;
                        player.dustParticleLeft.Play();
                        player.dustParticleRight.Play();
                        driving.Stop();

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
                }
                else
                {
                    if(wheel.isGrounded == true)
                    {
                        //Stop the car moving and resetting values
                        wheel.motorTorque = 0.0f;
                        wheel.brakeTorque = Mathf.Infinity;
                        carRigidbody.drag = dragAmount;
                        carRigidbody.isKinematic = false;
                        aHasBeenPressed = false;
                        player.dustParticleLeft.Stop();
                        player.dustParticleRight.Stop();
                        driving.Stop();
                    }
                    else
                    {
                        player.dustParticleLeft.Stop();
                        player.dustParticleRight.Stop();
                        driving.Stop();
                    }
                    
                }

                    //Create vector and quanternion
                    Quaternion q;
                    Vector3 p;

                    wheel.GetWorldPose(out p, out q);
                    // Assume that the only child of the wheelcollider is the wheel shape.
                    Transform shapeTransform = wheel.transform.GetChild(0);

                    //Alter the transform
                    shapeTransform.position = p;
                    shapeTransform.rotation = q;
            }
        }  
	}
}



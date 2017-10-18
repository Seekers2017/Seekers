using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{

    //Variables
    [Header("Player Exclusive Content")]
    [SerializeField]
    private float positiveDrift = 200.0f;
    [SerializeField]
    private float turnReduction = 2.0f;
    [SerializeField]
    private float turnSpeed = 500.0f;
    [SerializeField]
    private float driftRotation = 100.0f;
    [SerializeField]
    private float driftReduction = 2.0f;
    [SerializeField]
    private float driftSpeed = 50.0f;

    private enum PlayerState
    {
        STOPPED,
        DRIVING,
        REVERSING
    }

    //Designers do not need to see

    private float prevTurn;
    private Quaternion targetRotation;
    private Rigidbody rb;
    private float turn;
    private bool canStoreTurn;
    private PlayerState state;
    private bool drifting;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canStoreTurn = true;

        leftBumper.SetActive(false);
        rightBumper.SetActive(false);
        rearBumper.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Get the horizontal axis
        turn = Input.GetAxis("Horizontal") * rotation * Mathf.Deg2Rad;

        //Clamp speed
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        //If it isn't drifting
        if (drifting == false)
        {
            //Turn based on rotation and the turn (turn is what direction it's turning)
            targetRotation = Quaternion.Euler(Vector3.up * (turn / turnReduction) * turnSpeed * Time.fixedDeltaTime) * transform.rotation;
        }

        //Slurp rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 2.0f);

        //Call functions
        Drifting();
        Movement();
    }

    void Update()
    {
        //Change states
        if (Input.GetButton("Fire1"))
        {
            state = PlayerState.DRIVING;
        }
        else if (Input.GetButton("Fire2"))
        {
            state = PlayerState.REVERSING;
        }
        else
        {
            state = PlayerState.STOPPED;
        }



        Items();
    }

    void Drifting()
    {
        //Drift mechanic
        //If the current state is drifting
        if (Input.GetAxis("Right Trigger") == 1)
        {
            drifting = true;

            //If we can store the turn
            if (canStoreTurn == true)
            {
                //store the turn
                prevTurn = turn;
                //say we can't store the turn
                canStoreTurn = false;
            }

            if (canStoreTurn == false)
            {
                //Clamps turn
                if (prevTurn > 0.0f && prevTurn < 1.0f)
                {
                    prevTurn = 1.0f;
                }
                else if (prevTurn < 0.0f && prevTurn > -1.0f)
                {
                    prevTurn = -1.0f;
                }

                //Drift
                targetRotation = transform.rotation * Quaternion.Euler(new Vector3(0.0f, driftRotation * prevTurn -1.0f, 0.0f));

                //If right turn
                if (prevTurn > 0.0f)
                {
                    //Add a diagonal force and restrict the forward velocity
                    rb.AddForce((transform.forward).normalized * 20.0f * positiveDrift * Time.fixedDeltaTime);
                }
                //Else if left turn
                if (prevTurn < 0.0f)
                {
                    //Add a diagonal force and restrict the forward velocity
                    rb.AddForce((transform.forward).normalized * 20.0f * -positiveDrift * Time.fixedDeltaTime);
                }

            }

        }
        else
        {
            drifting = false;
        }

    }


    void Movement()
    {
        //If the "A" button is pressed
        if (state == PlayerState.DRIVING)
        {
            if(isBoosting == false && drifting == false)
            {
                //Move forward
                rb.AddForce(transform.forward * speed * Time.fixedDeltaTime);
            }
            else if (isBoosting == true && drifting == false)
            {
                //Boost
                rb.AddForce(transform.forward * boostSpeed * Time.fixedDeltaTime);
            }
            else if(isBoosting == false && drifting == true)
            {
                //Drift
                rb.AddForce(transform.forward * (speed / driftReduction) * Time.fixedDeltaTime);
            }
            else if(isBoosting == true && drifting == true)
            {
                //Boost Drift
                rb.AddForce(transform.forward * (boostSpeed / driftReduction) * Time.fixedDeltaTime);
            }
            
        }
        //If the "B" button is pressed
        else if (state == PlayerState.REVERSING)
        {
            //Move backward
            rb.AddForce((transform.forward * -1.0f) * speed * Time.fixedDeltaTime);
        }
    }

    void Items()
    {
        if (hasItem == true)
        {
            if (hasBumper == true)
            {
                if (Input.GetButtonDown("LeftBumper"))
                {
                    //Traverse through item selection
                    if (bumperSelect >= 3)
                    {
                        bumperSelect = 0;
                    }
                    else
                    {
                        bumperSelect++;
                    }
                }

                if (Input.GetButton("Fire3"))
                {
                    //Input the selection to the car
                    if (bumperSelect == 0)
                    {
                        Bumper(leftBumper);
                        hasItem = false;
                    }
                    else if (bumperSelect == 1)
                    {
                        Bumper(rightBumper);
                        hasItem = false;
                    }
                    else if (bumperSelect == 2)
                    {
                        Bumper(rearBumper);
                        hasItem = false;
                    }
                }
            }
            else
            {
                if (Input.GetButton("Fire3"))
                {
                    //Go fast
                    SpeedBoost();
                    hasItem = false;
                }
            }
        }
    }
}

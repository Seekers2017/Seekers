using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //TODO:
    //Items  (collisions and effects)
    //Hit counter
    //Realistic car movement (default turning is too high)
    //Create entity class
    //Raycasting to the walls to detect position on the track
    //Change pivot point of car
    //Slerp turning rotation

    //Variables
    //Public
    [Header("Alter")]
    public float speed = 3500.0f;
    public float rotation = 200.0f; 
    public float negativeDrift = 0.25f;
    public float positiveDrift = 2.0f;
    public float driftRotation = 100.0f;
    public float driftHopVelocity = 2.0f;
    public float jumpSpeed = 1000.0f;
    public float jumpAmount = 0.15f;
    public float driftReduction = 2.0f;
    public int maxHits = 3;
    public float maxRespawnTime = 3.0f;
    public float turnReduction = 2.0f;
    public float maxBoostTimer = 1.0f;
    public float boostSpeed = 500.0f;


    [Header("Do Not Alter")]
    public Transform playerMesh;
    public GameObject frontBumper;
    public GameObject leftBumper;
    public GameObject rearBumper;
    public GameObject rightBumper;

    //Private
    private float driftSpeed;
    private float prevTurn;
    private Quaternion targetRotation;
    private bool canRotate;
    private Rigidbody rb;
    private float turn;
    private bool canStoreYPos;
    private float storePrevYpos;
    private float timer;
    private int playerHit;
    private float centreValue;
    private int bumperSelect = 0;
    private bool hasBumper;
    private bool hasItem;
    private float boostTimer;
    private bool canStoreTurn;

    //Change to enum
    private bool canJump;
    private bool grounded;
    private bool hasJumped;
    private bool falling;
    private bool canMove;
    private bool drifting;


    //Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        canRotate = true;
        canJump = false;
        canStoreYPos = true;
        grounded = true;
        drifting = false;
        canMove = true;
        falling = false;
        hasItem = false;
        canStoreTurn = true;

        rightBumper.gameObject.SetActive(false);
        leftBumper.gameObject.SetActive(false);
        rearBumper.gameObject.SetActive(false);
    }
	
	//Update is called once per frame
	void FixedUpdate ()
    {
        //Get the horizontal axis
        turn = Input.GetAxis("Horizontal") * rotation * Mathf.Deg2Rad;
       
       //If it isn't drifting
       if(drifting == false)
       {
  
            //Turn based on rotation and the turn (turn is what direction it's turning)
            transform.Rotate(Vector3.up, (turn / turnReduction));
            targetRotation = transform.rotation;
        }
        

        //rb.AddTorque(transform.up * rotation * (turn / 2.0f));

        //Slurp rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 2.0f);
        
        //Various functions
        Movement();
        Drifting();
        Respawn();
        Items();
    }


    void Items()
    {
        //Depending on whether or not they have an item
        if(hasItem == true)
        {
            //If they are holdinbg the bumper
            if(hasBumper == true)
            {
                if(Input.GetButtonDown("LeftBumper"))
                {
                    //If it is greater than three, make it zero
                    //Otherwise add each time the button is pressed
                    if(bumperSelect > 3)
                    {
                        bumperSelect = 0;
                    }
                    else
                    {
                        bumperSelect++;
                    }
                }

                if(Input.GetButtonDown("Fire3"))
                {
                    //Check what number it is then spawn based on the number
                    if(bumperSelect == 0)
                    {
                        leftBumper.GetComponent<BumperScript>().isAlive = true;
                        leftBumper.GetComponent<BumperScript>().lifeSpan = 5.0f;
                        leftBumper.SetActive(true);
                    }
                    else if(bumperSelect == 1)
                    {
                        rightBumper.GetComponent<BumperScript>().isAlive = true;
                        rightBumper.GetComponent<BumperScript>().lifeSpan = 5.0f;
                        rightBumper.SetActive(true);
                    }
                    else if(bumperSelect == 2)
                    {
                        rearBumper.GetComponent<BumperScript>().isAlive = true;
                        rearBumper.GetComponent<BumperScript>().lifeSpan = 5.0f;
                        rearBumper.SetActive(true);
                    }

                    //Doesn't have an item anymore
                    hasItem = false;
                }
            }
            else
            {
                if(Input.GetButtonDown("Fire3"))
                {
                    //They have the speed boost
                    boostTimer += Time.fixedDeltaTime;

                    //Go faster
                    speed += boostSpeed;

                    //Go slower
                    if (boostTimer >= maxBoostTimer)
                    {
                        speed -= boostSpeed;

                        //Return out
                        hasItem = false;
                    }
                }
                
            }
          
        }
        else
        {
            //Reset
            bumperSelect = 0;
        }

    }

    void Respawn()
    {
        //TODO:
        //Centre spawning ( leave until later)
        //Invisible Object 


        //If the player has exceeded hit amounts
        if(playerHit >= maxHits)
        {
            //Start the timer
            timer += Time.fixedDeltaTime;

            //It's not active
            //gameObject.SetActive(false);

            //Respawn the player based on time
            if(timer >= maxRespawnTime)
            {
                //gameObject.SetActive(true);
                transform.position = new Vector3((transform.position.x - 3.0f), transform.position.y, transform.position.z);
            }


        }
    }

    void Drifting()
    {
        //Drift mechanic
        //If the right trigger is pressed
        if (Input.GetAxis("Right Trigger") == 1)
        {
            drifting = true;

            if (canJump == true)
            {
                //Don't push the player forward
                canMove = false;

                //If we can store the Y pos
                if (canStoreYPos == true)
                {
                    //Store the y pos
                    storePrevYpos = transform.position.y;

                    //Stored so break out
                    canStoreYPos = false;
                }

                if (falling == false)
                {
                    //Jump
                    rb.AddForce(transform.up * speed * Time.fixedDeltaTime);

                    if (transform.position.y > storePrevYpos + jumpAmount)
                    {
                        falling = true;
                    }
                }

                if (falling == true)
                {
                    //Fall
                    rb.AddForce((transform.up * -1.0f) * -speed * Time.fixedDeltaTime);
                    hasJumped = true;

                    //If we have hit the ground
                    if (grounded == true)
                    {
                        canJump = false;
                    }
                }
            }

            //If we can't jump
            if (canJump == false)
            {
                canMove = true;

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
                    targetRotation = transform.rotation * Quaternion.Euler(new Vector3(0.0f, driftRotation * prevTurn * -1.0f, 0.0f));

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

        }


        if (Input.GetAxis("Right Trigger") != 1)
        {
            //Setting variables if the tigger is not pressed back to non drifitng state
            playerMesh.transform.localRotation = Quaternion.identity;
            canRotate = true;
            canStoreTurn = true;
            canStoreYPos = true;
            canJump = false;
            hasJumped = false;
            falling = false;
            drifting = false;
        }
    }

    void Movement()
    {
        //If the "A" button is pressed
        if (Input.GetButton("Fire1"))
        {
            if (canMove == true)
            {
                if(drifting == false)
                {
                    //Move forward
                    rb.AddForce(transform.forward * speed * Time.fixedDeltaTime);
                }
                else
                {
                    //Move forward
                    rb.AddForce(transform.forward * (speed / driftReduction) * Time.fixedDeltaTime);
                }
                
            }
            else
            {
                rb.AddForce(Vector3.zero);
            }

        }
        //If the "B" button is pressed
        else if (Input.GetButton("Fire2"))
        {
            //Move backward
            rb.AddForce((transform.forward * -1.0f) * speed * Time.fixedDeltaTime);
        }
    }



    void OnCollisionEnter(Collision a_other)
    { 
        if(a_other.transform.tag == ("AI"))
        {
            a_other.gameObject.GetComponent<AI>().transform.position = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider a_other)
    {
        //If they are the items
        if(a_other.tag == "BumperItem")
        {
            hasItem = true;
            hasBumper = true;
        }
        else if(a_other.tag == "SpeedBoost")
        {
            hasItem = true;
            hasBumper = false;
        }
    }
}




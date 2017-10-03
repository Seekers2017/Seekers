using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //TODO:
    //Bumper (collisions)
    //Items  (collisions and effects)
    //Drifting (turning bug)


    //Variables
    //Public
    [Header("Alter")]
    public float speed = 3500.0f;
    public float rotation = 200.0f; 
    public float negativeDrift = 0.25f;
    public float positiveDrift = 2.0f;
    public float driftSpeed = 0.25f;
    public float driftRotation = 100.0f;
    public float driftHopVelocity = 2.0f;
    public float jumpSpeed = 1000.0f;
    public float jumpAmount = 0.15f;

    [Header("Do Not Alter")]
    public Transform playerMesh;

    //Private
    private bool canStoreTurn;
    private float prevTurn;
    private bool isDriftingRight;
    private bool isDriftingLeft;
    private Quaternion targetRotation;
    private bool canRotate;
    private Rigidbody rb;
    private float turn;
    private bool canJump;
    private float storePrevYpos;
    private bool canStoreYPos;
    private bool grounded;
    private bool hasJumped;
    private bool falling;
    private bool canMove;


    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        canStoreTurn = true;
        canRotate = true;
        canJump = true;
        canStoreYPos = true;
        grounded = true;
        canMove = true;
        falling = false;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {   
        //Get the horizontal axis
        turn = Input.GetAxis("Horizontal");

        //Check if it is drifting before you apply turn
        if (isDriftingRight == true)
        {
            //Restrict turn based on what direction you're travelling
            if (turn < 0.0f)
            {
                turn *= negativeDrift;
            }
            else if (turn > 0.0f)
            {
                turn *= positiveDrift;
            }
        }
        else if (isDriftingLeft == true)
        {
            //Restrict turn based on what direction you're travelling
            if (turn < 0.0f)
            {
                turn *= positiveDrift;
            }
            else if (turn > 0.0f)
            {
                turn *= negativeDrift;
            }
        }

        //Turn based on rotation and the turn (turn is what direction it's turning)
        rb.AddTorque(transform.up * rotation * (turn / 2.0f));

        //Slurp rotation
        playerMesh.transform.rotation = Quaternion.Slerp(playerMesh.transform.rotation, targetRotation, Time.fixedDeltaTime * 2.0f);

        //If the "A" button is pressed
        if (Input.GetButton("Fire1"))
        {
            if(canMove == true)
            {
                //Move forward
                rb.AddForce(transform.forward * speed * Time.fixedDeltaTime);
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

        //Drift mechanic
        //If the right trigger is pressed
        if (Input.GetAxis("Right Trigger") == 1)
        {
            if(canJump == true)
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
            if(canJump == false)
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
                    if (canRotate == true)
                    {
                        //Makes the 
                        if(prevTurn > 0.0f && prevTurn < 1.0f)
                        {
                            prevTurn = 1.0f;
                        }
                        else if (prevTurn < 0.0f && prevTurn > -1.0f)
                        {
                            prevTurn = -1.0f;
                        }

                        //Drift
                        targetRotation = transform.rotation * Quaternion.Euler(new Vector3(0.0f, driftRotation * prevTurn, 0.0f));
                        canRotate = false;
                    }

                    //If right turn
                    if (prevTurn > 0.0f)
                    {
                        //Add a diagonal force and restrict the forward velocity
                        rb.AddForce((transform.forward + transform.right).normalized * speed * driftSpeed * Time.fixedDeltaTime);

                        //It's drifting
                        isDriftingRight = true;
                    }
                    //Else if left turn
                    if (prevTurn < 0.0f)
                    {
                        //Add a diagonal force and restrict the forward velocity
                        rb.AddForce((transform.forward + transform.right).normalized * speed * driftSpeed * Time.fixedDeltaTime);

                        //It's drifting
                        isDriftingLeft = true;
                    }

                }
            }
           
        }

        
        if(Input.GetAxis("Right Trigger") != 1)
        {
            //Setting variables if the tigger is not pressed back to non drifitng state
            playerMesh.transform.localRotation = Quaternion.identity;
            canStoreTurn = true;
            isDriftingRight = false;
            isDriftingLeft = false;
            canRotate = true;
            canStoreYPos = true;
            canJump = true;
            hasJumped = false;
            falling = false;
        }


        //Lock the X axis
        if (transform.rotation.x != 0)
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }
    }


    void OnCollisionEnter(Collision a_other)
    {
        //If you have jumped, check the collision
        if(hasJumped == true)
        {
            //If it is the track edge
            if (a_other.collider.gameObject.CompareTag("Ground"))
            {
                grounded = true;
            }
        } 
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //TODO:
    //Diagonal movement on drift




    //Variables
    public float speed = 20.0f;
    public float rotation = 5.0f;
    public Rigidbody rb;
    public Transform playerMesh;
    private float z;
    private float turn;
    private bool canStoreTurn;
    private float prevTurn;
    private bool isDriftingRight;
    private bool isDriftingLeft;
    private Quaternion targetRotation;
    private bool canRotate;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        canStoreTurn = true;
        canRotate = true;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Update z float
        z = Input.GetAxis("Vertical");
        
        //Get the horizontal axis
        turn = Input.GetAxis("Horizontal");

        //Check if it is drifting before you apply turn
        if (isDriftingRight == true)
        {
            //Restrict turn based on what direction you're travelling
            if (turn < 0.0f)
            {
                turn *= 0.25f;
            }
            else if (turn > 0.0f)
            {
                turn *= 2.0f;
            }
        }
        else if (isDriftingLeft == true)
        {
            //Restrict turn based on what direction you're travelling
            if (turn < 0.0f)
            {
                turn *= 2.0f;
            }
            else if (turn > 0.0f)
            {
                turn *= 0.25f;
            }
        }

        //Turn based on rotation and the turn (turn is what direction it's turning)
        rb.AddTorque(transform.up * rotation * (turn / 2.0f));

        //Slurp rotation
        playerMesh.transform.rotation = Quaternion.Slerp(playerMesh.transform.rotation, targetRotation, Time.fixedDeltaTime * 2.0f);

        //If the "A" button is pressed
        if (Input.GetButton("Fire1"))
        {
            //Move forward
            rb.AddForce(transform.forward * speed * Time.fixedDeltaTime);
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
            //If we can store the turn
            if(canStoreTurn == true)
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
                    //Drift
                    targetRotation = transform.rotation * Quaternion.Euler(new Vector3(0.0f, 100.0f * prevTurn, 0.0f));
                    canRotate = false;
                }
               
                //If right turn
                if (prevTurn > 0.0f)
                {
                    //Add a diagonal force and restrict the forward velocity
                    rb.AddForce((transform.forward + transform.right).normalized * speed * 0.25f * Time.fixedDeltaTime);

                    //It's drifting
                    isDriftingRight = true;
                }
                //Else if left turn
                if(prevTurn < 0.0f)
                {
                    //Add a diagonal force and restrict the forward velocity
                    rb.AddForce((transform.forward + transform.right).normalized * speed * 0.25f * Time.fixedDeltaTime);

                    //It's drifting
                    isDriftingLeft = true;
                }
               
            }
        }
        else
        {
            playerMesh.transform.localRotation = Quaternion.identity;
            canStoreTurn = true;
            isDriftingRight = false;
            isDriftingLeft = false;
            canRotate = true;
            targetRotation = Quaternion.Euler(new Vector3(0, turn, 0));
        }


        //Lock the X axis
        if (transform.rotation.x != 0)
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }
    }
}

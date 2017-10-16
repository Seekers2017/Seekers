using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    //Variables
    [Header("General")]
    public float speed = 20.0f;
    public float rotation = 5.0f;
    public int maxHits;
    public int hits;
    private Rigidbody rb;
    private float accelTimer;
    private Node targetNode;
    private NodeManage nodeManage;

    //Variables for steering
    [Header("Steering")]
    public float sensorLength = 5.0f;
    public Vector3 frontSensorPos = new Vector3(0.0f, 0.2f, 0.5f);
    public float frontSideSensorPos = 0.2f;
    public float frontSensorAngle = 30.0f;
    public float angledTurn = 1.5f;
    public float straightTurn = 4.0f;
    public float steerSpeed = 100.0f;
    private bool avoidingBox = false;
    private float avoidMultiplier = 0.0f;
    private bool canDetect = true;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        nodeManage = GameObject.FindGameObjectWithTag("Mother Node").GetComponent<NodeManage>();
    }
	

	// Update is called once per frame
	void FixedUpdate ()
    {
        Sensors();

        if(rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }


        if(targetNode == null)
        {
            targetNode = nodeManage.nodeList[0];
        }

        //Start the timer
        accelTimer += Time.fixedDeltaTime;

        //If the timer is greater than 3 seconds
        if(accelTimer > 3.0f)
        {
            //Get the direction then move to the direction
            Vector3 moveDirection = targetNode.gameObject.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rb.velocity), rotation * Time.fixedDeltaTime);
        
            //Move towards node
            rb.AddForce(moveDirection.normalized * speed * Time.fixedDeltaTime);
        }
	}


    private void Sensors()
    {
        //Create variables
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        

        //Initialising
        sensorStartPos += transform.forward * frontSensorPos.z;
        sensorStartPos += transform.up * frontSensorPos.y;
        avoidingBox = false;

        if(canDetect == true)
        {
            //Front right sensor
            sensorStartPos += transform.right * frontSideSensorPos;
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                if (hit.collider.CompareTag("Box"))
                {
                    //For debug
                    Debug.DrawLine(sensorStartPos, hit.point);
                    avoidingBox = true;
                    avoidMultiplier -= straightTurn;
                }
            }
            //Front right angled sensor
            else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
            {
                if (hit.collider.CompareTag("Box"))
                {
                    //For debug
                    Debug.DrawLine(sensorStartPos, hit.point);
                    avoidingBox = true;
                    avoidMultiplier -= angledTurn;
                }
            }

            //Front left sensor
            sensorStartPos -= transform.right * (frontSideSensorPos * 2.0f);
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                if (hit.collider.CompareTag("Box"))
                {
                    //For debug
                    Debug.DrawLine(sensorStartPos, hit.point);
                    avoidingBox = true;
                    avoidMultiplier += straightTurn;
                }
            }
            //Front left angled sensor
            else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
            {
                if (hit.collider.CompareTag("Box"))
                {
                    //For debug
                    Debug.DrawLine(sensorStartPos, hit.point);
                    avoidingBox = true;
                    avoidMultiplier += angledTurn;
                }
            }

            //Front centre sensor
            if (avoidMultiplier == 0.0f)
            {
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
                {
                    if (hit.collider.CompareTag("Box"))
                    {
                        //For debug
                        Debug.DrawLine(sensorStartPos, hit.point);
                        avoidingBox = true;
                    }

                    if (hit.normal.x < 0.0f)
                    {
                        avoidMultiplier = -straightTurn;
                    }
                    else
                    {
                        avoidMultiplier = straightTurn;
                    }
                }
            }

            //If the car is avoiding something
            if (avoidingBox == true)
            {
                //Move towards node
                rb.AddForce((transform.right * avoidMultiplier) * steerSpeed * Time.fixedDeltaTime);

            }
        }
       
    }

    void OnCollisionEnter(Collision a_other)
    {
        //If it is the track edge
        if (a_other.collider.gameObject.CompareTag("Border"))
        {
            canDetect = false;
            Debug.Log("Collided with border");
        }
    }

    void OnTriggerEnter(Collider a_other)
    {

        //If we have collided with the node
        if(a_other.CompareTag("Node"))
        {
            Node nodeScript = a_other.GetComponent<Node>();

            //If we are at the current node
            if (nodeScript == targetNode)
            {
                //We've reached the node we were trying to get to
                //Change targetNode to nextNode
                targetNode = targetNode.next;
            }
        }
    }
}

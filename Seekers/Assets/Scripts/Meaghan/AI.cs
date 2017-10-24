using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Entity
{

    //Variables
    [SerializeField]
    private float itemSpawnTime = 2.0f;
    [SerializeField]
    protected float boostSpeed = 500.0f;
    [SerializeField]
    protected float maxSpeed = 4000.0f;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float rotation;

    private Rigidbody rb;
    private float accelTimer;
    private Node targetNode;
    private NodeManage nodeManage;

    //Variables for steering
    private float sensorLength = 10.0f;
    private Vector3 frontSensorPos = new Vector3(0.0f, 0.2f, 0.5f);
    private float frontSideSensorPos = 0.2f;
    private float frontSensorAngle = 30.0f;
    private float angledTurn = 1.5f;
    private float straightTurn = 4.0f;
    private  float steerSpeed = 100.0f;
    private bool avoidingBox = false;
    private float avoidMultiplier = 0.0f;
    private bool canDetect = true;
    private float itemTimer;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nodeManage = GameObject.FindGameObjectWithTag("Mother Node").GetComponent<NodeManage>();

        leftBumper.SetActive(false);
        rightBumper.SetActive(false);
        rearBumper.SetActive(false);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Functions
        Sensors();

        //Restart the following
        if (targetNode == null)
        {
            targetNode = nodeManage.nodeList[0];
        }

        //Start the timer
        accelTimer += Time.fixedDeltaTime;

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }


        //If the timer is greater than 3 seconds
        if (accelTimer > 3.0f)
        {
            //Get the direction then move to the direction
            Vector3 moveDirection = targetNode.gameObject.transform.position - transform.position;

            //Rotation
            if (rb.velocity.magnitude > 5.0f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rb.velocity), rotation * Time.fixedDeltaTime);
            }

            //Move towards node
            rb.AddForce(moveDirection.normalized * speed * Time.fixedDeltaTime);
        }
    }

    void Update()
    {
        Items();
    }

    private void Items()
    {
        if (hasItem == true)
        {
            itemTimer += Time.deltaTime;

            if (hasBumper == true)
            {
                bumperSelect = Random.Range(0, 3);

                if (itemTimer > itemSpawnTime)
                {
                    //Input the selection to the car
                    if (bumperSelect == 0)
                    {
                        Bumper(leftBumper);
                        itemTimer = 0.0f;
                        hasItem = false;
                    }
                    else if (bumperSelect == 1)
                    {
                        Bumper(rightBumper);
                        itemTimer = 0.0f;
                        hasItem = false;
                    }
                    else if (bumperSelect == 2)
                    {
                        Bumper(rearBumper);
                        itemTimer = 0.0f;
                        hasItem = false;
                    }
                }
            }
            else
            {
                if (itemTimer > itemSpawnTime)
                {
                    //Go fast
                    SpeedBoost();
                    itemTimer = 0.0f;
                    hasItem = false;
                }
            }
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

        if (canDetect == true)
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

    protected override void OnTriggerEnter(Collider a_other)
    {
        //Call base as well as doing it's own things
        base.OnTriggerEnter(a_other);

        //If we have collided with the node
        if (a_other.CompareTag("Node"))
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

    protected override void OnCollectItem(SpawnerScript item)
    {
        base.OnCollectItem(item);

        //other ai related stuff
    }
}
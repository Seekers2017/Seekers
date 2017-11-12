using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Entity
{ 
    //TODO:
    //Behaviours (move)
    //Boost speed
    //Bumper issue

    //Variables
    [SerializeField]
    private float itemSpawnTime = 2.0f;
    [SerializeField]
    private float boostSpeed = 20.0f;
    [SerializeField]
    private float maxSpeed = 4000.0f;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotation;

    protected List<Entity> carList;

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
    private float closestDistance = 20;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nodeManage = GameObject.FindGameObjectWithTag("Mother Node").GetComponent<NodeManage>();

        leftBumper.SetActive(false);
        rightBumper.SetActive(false);
        rearBumper.SetActive(false);

        carList = new List<Entity>();

        GameObject[] AIs = GameObject.FindGameObjectsWithTag("AI");

        foreach (GameObject car in AIs)
        {
            carList.Add(car.GetComponent<Entity>());
        }

        carList.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>());
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        ////Boost
        //if (isBoosting)
        //{
        //    //Alter the speed and boost
        //    maxSpeed += boostSpeed;
        //    UpdateSpeedBoost();
        //}

        //Functions
        Sensors();

        //Restart the following
        if (targetNode == null)
        {
            targetNode = nodeManage.nodeList[0];
        }

        //Start the timer
        accelTimer += Time.fixedDeltaTime;

        //If the timer is greater than 3 seconds
        if (accelTimer > 3.0f)
        {
            //Get the direction then move to the direction
            Vector3 moveDirection = targetNode.gameObject.transform.position - transform.position;
            moveDirection.y = 0.0f;


            //Rotation
            if (rb.velocity.magnitude > 5.0f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rb.velocity), rotation * Time.fixedDeltaTime);
            }


       //    rb.velocity = Vector3.Lerp(rb.velocity, moveDirection.normalized * speed, Time.fixedDeltaTime * 2.5f);

            ////Move towards node
            rb.AddForce(moveDirection.normalized * speed * Time.fixedDeltaTime);

            if(rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }


        }
    }

    void Update()
    {
        //Functions
        Items();
        Interaction();
    }

    private void Interaction()
    {
        //Iterate through cars
        foreach(Entity car in carList)
        {
            if(car != this)
            {
                Vector3 vecBetween = transform.position - car.transform.position;

                //Check the distance between two things
                float distance = vecBetween.magnitude;

                if (distance < closestDistance)
                {
                    //Check angles
                    float angle = Vector3.SignedAngle(transform.position, vecBetween, Vector3.up);

                    //Do behaviours
                    if(angle >= -24.0f && angle <= 50.0f)
                    {
                        //Right side
                        //If I have a right bumper and the other car does not
                        if(rightBumper.activeSelf == true && car.leftBumper.activeSelf == false)
                        {
                            //Drive towards the other car
                        }
                        //If we both have a bumper on the corresponding sides
                        else if (rightBumper.activeSelf == true && car.leftBumper.activeSelf == true)
                        {
                            //Drive towards the car
                        }
                        //If we can see that they have a bumper and we don't
                        else if (rightBumper.activeSelf == false && car.leftBumper.activeSelf == true)
                        {
                            //Drive away from the car
                        }
                    }
                    else if (angle >= -175 && angle <= -85)
                    {
                        //Left side
                        //Right side
                        //If I have a left bumper and the other car does not
                        if (leftBumper.activeSelf == true && car.rightBumper.activeSelf == false)
                        {
                            //Drive towards the other car
                        }
                        //If we both have a bumper on the corresponding sides
                        else if (leftBumper.activeSelf == true && car.rightBumper.activeSelf == true)
                        {
                            //Drive towards the car
                        }
                        //If we can see that they have a bumper and we don't
                        else if (leftBumper.activeSelf == false && car.rightBumper.activeSelf == true)
                        {
                            //Drive away from the car
                        }
                    }
                    else if (angle >= -84 && angle <= -25)
                    {
                        //Front side
                        
                        //Ram the backside
                    }

                }
            }
        }
    }



    private void Items()
    {
        //Has an item
        if (hasItem == true)
        {
            //Start the timer
            itemTimer += Time.deltaTime;

            if (hasBumper == true)
            {
                //Randomise the bumper select
                bumperSelect = Random.Range(0, 3);

                //We can use the item
                if (itemTimer > itemSpawnTime)
                {
                    //Input the selection to the car
                    if (bumperSelect == 0)
                    {
                        //Set the bumper
                        Bumper(leftBumper);
                        itemTimer = 0.0f;
                        hasItem = false;
                    }
                    else if (bumperSelect == 1)
                    {
                        //Set the bumper
                        Bumper(rightBumper);
                        itemTimer = 0.0f;
                        hasItem = false;
                    }
                    else if (bumperSelect == 2)
                    {
                        //Set the bumper
                        Bumper(rearBumper);
                        itemTimer = 0.0f;
                        hasItem = false;
                    }
                }
            }
            else
            {
                //We can use the timer
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
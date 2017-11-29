using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Entity
{
    //Variables
    [SerializeField]
    private float itemSpawnTime = 2.0f;
    [SerializeField]
    private float boostSpeed = 40.0f;
    [SerializeField]
    private float maxSpeed = 30.0f;
    [SerializeField]
    private float speed = 10000.0f;
    [SerializeField]
    private float rotation = 3.0f;
    [SerializeField]
    private float maxMoveTimer = 5.0f;

    protected List<Entity> carList;

    private Rigidbody rb;
    private Node targetNode;
    private NodeManage nodeManage;
    private float accelTimer;

    //Variables for steering
    private float sensorLength = 10.0f;
    private Vector3 frontSensorPos = new Vector3(0.0f, 0.2f, 0.5f);
    private float frontSideSensorPos = 0.2f;
    private float frontSensorAngle = 30.0f;
    private float angledTurn = 1.5f;
    private float straightTurn = 4.0f;
    private float steerSpeed = 100.0f;
    private bool avoidingBox = false;
    private float avoidMultiplier = 0.0f;
    private bool canDetect = true;
    private float itemTimer;
    private float closestDistance = 20;
    private float driveTimer;
    private Vector3 vecBetween;
    private Vector3 vecAway;
    private Vector3 moveDirection;
    private bool detectedCar;
    private float frontTimer;
    private bool frontDetectedCar;
    private float storedSpeed;
    private float randDist;
    private bool drive;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nodeManage = GameObject.FindGameObjectWithTag("Mother Node").GetComponent<NodeManage>();

        leftBumper.SetActive(false);
        rightBumper.SetActive(false);
        rearBumper.SetActive(false);
        drive = true;


        storedSpeed = maxSpeed;

        carList = new List<Entity>();

        GameObject[] AIs = GameObject.FindGameObjectsWithTag("AI");

        foreach (GameObject car in AIs)
        {
            carList.Add(car.GetComponent<Entity>());
        }

        carList.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>());

        randDist = Random.Range(-10.0f, 10.0f);
        dustParticleLeft.Stop();
        dustParticleRight.Stop();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Boost
        if (isBoosting)
        {
            //Alter the speed and boost
            maxSpeed = boostSpeed;
            UpdateSpeedBoost();
        }
        else
        {
            //Return to normal
            maxSpeed = storedSpeed;
        }

        //Functions
        Sensors();

        //Restart the following
        if (targetNode == null)
        {
            targetNode = nodeManage.nodeList[0];
        }

        //Start the timer
        accelTimer += Time.fixedDeltaTime;
        
        //If we can't drive
        if(drive == false)
        {
            //Turn off the particles
            dustParticleLeft.Stop();
            dustParticleRight.Stop();
        }


        //Get the direction then move to the direction
        //If there are no other cars around
        if (!detectedCar && !frontDetectedCar)
        {
            moveDirection = (targetNode.gameObject.transform.position + targetNode.gameObject.transform.right * -10.0f) - transform.position;
        }


        //Lock y axis
        moveDirection.y = 0.0f;

        Movement();
    }

    void Update()
    {
        if(accelTimer > 6.0f)
        {
            //Respawn check
            if (hits >= maxHits)
            {
                //Set all values back and respawn
                drive = false;
                rb.drag = 3.0f;
                hasLowHealth = false;
                Respawn();
            }
            else
            {
                //Playe can drive and store position to respawn at
                drive = true;
                rb.drag = 0.0f;
                PositionTimer();
            }
        }

        //Functions
        Items();
        Interaction();
    }


    private void Movement()
    {
        //If the timer is greater than 3 seconds
        if (accelTimer > 3.0f)
        {
            if(drive == true)
            {
                dustParticleLeft.Play();
                dustParticleRight.Play();

                //Rotation
                if (rb.velocity.magnitude > 5.0f)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rb.velocity), rotation * Time.fixedDeltaTime);
                }

                ////Move towards node
                rb.AddForce(moveDirection.normalized * speed * Time.fixedDeltaTime);

                if (rb.velocity.magnitude > maxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * maxSpeed;
                }
            }
        }
    }

    private void Interaction()
    {
        //Iterate through cars
        foreach (Entity car in carList)
        {
            if (car != this)
            {
                //Vectors
                vecBetween = transform.position - car.transform.position;

                //Check the distance between two things
                float distance = vecBetween.magnitude;

                if (distance < closestDistance)
                {
                    //Check angles
                    float angle = Vector3.SignedAngle(transform.position, vecBetween, Vector3.up);

                    //Do behaviours (handles right side)
                    if (angle >= -24.0f && angle <= 50.0f)
                    {
                        //Start timers
                        frontTimer += Time.deltaTime; 
                        driveTimer += Time.deltaTime;

                        //Right side
                        //If I have a right bumper and the other car does not
                        if (rightBumper.activeSelf == true && car.leftBumper.activeSelf == false)
                        {
                            Seek(car);

                        }
                        //If we both have a bumper on the corresponding sides
                        else if (rightBumper.activeSelf == true && car.leftBumper.activeSelf == true)
                        {
                            Seek(car);
                        }
                        //If we can see that they have a bumper and we don't
                        else if (rightBumper.activeSelf == false && car.leftBumper.activeSelf == true)
                        {

                            Flee(car);
                        }
                    }
                    else if (angle >= -175 && angle <= -85) 
                    {
                        //Left side            
                        //If I have a left bumper and the other car does not
                        if (leftBumper.activeSelf == true && car.rightBumper.activeSelf == false)
                        {
                            Seek(car);
                        }
                        //If we both have a bumper on the corresponding sides
                        else if (leftBumper.activeSelf == true && car.rightBumper.activeSelf == true)
                        {
                            Seek(car);
                        }
                        //If we can see that they have a bumper and we don't
                        else if (leftBumper.activeSelf == false && car.rightBumper.activeSelf == true)
                        {
                            Flee(car);
                        }
                    }
                    else if (angle >= -84 && angle <= -25) //Front of the car
                    {
                        if(angle >= -54) //Right
                        {
                            MoveRight(car);
                        }
                        else if (angle < -54) //Left
                        {
                            MoveLeft(car);
                        }
                    }
                }
            }
        }
    }


    private void MoveLeft(Entity a_car)
    {
        moveDirection = (a_car.gameObject.transform.position + a_car.gameObject.transform.right * -2.0f) - transform.position;
    }

    private void MoveRight(Entity a_car)
    {
        moveDirection = (a_car.gameObject.transform.position + a_car.gameObject.transform.right * 2.0f) - transform.position;
    }

    private void Flee(Entity a_car)
    {
        //Drive away from the car
        if (driveTimer < maxMoveTimer)
        {
            detectedCar = true;
            moveDirection = a_car.transform.position + transform.position;
        }
        else
        {
            detectedCar = false;
        }
    }

    private void Seek(Entity a_car)
    {
        //Drive towards the car
        if (driveTimer < maxMoveTimer)
        {
            detectedCar = true;
            moveDirection = a_car.transform.position - transform.position;
        }
        else
        {
            detectedCar = false;
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

                randDist = Random.Range(-5.0f, 5.0f);
            }
        }
    }

    //protected override void OnCollectItem(SpawnerScript item)
    //{
    //    //base.OnCollectItem(item);

    //    //other ai related stuff
    //}

    //protected override void Bumper(GameObject a_bumper)
    //{
    //    //do nothing
    //}
}
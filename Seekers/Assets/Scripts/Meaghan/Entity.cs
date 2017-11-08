using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    //TODO:
    //[AI]
    //Improved movement 
    //Radius check (if another car is nearby)


    //Variables
    [Header("General")]
    [SerializeField]
    protected int maxHits;
    [Tooltip("Max lifespan for the bumpers.")]
    [SerializeField]
    protected float maxLifeSpan = 5.0f;
    [SerializeField]
    protected float maxBoostTimer = 1.0f;
    [Tooltip("Time it takes to respawn the car.")]
    [SerializeField]
    private float respawnTime = 2.0f;



    [Header("Do Not Tamper With")]
    public GameObject frontBumper;
    public GameObject leftBumper;
    public GameObject rearBumper;
    public GameObject rightBumper;
    


    //He attac, but he also protec (the variables)
    protected int hits;
    protected bool hasBumper;
    protected bool hasItem;
    protected int bumperSelect = 0;
    protected float boostTimer;
    protected bool isBoosting;
    protected float timer;
    protected Vector3 storedPosition;
    protected bool respawn;
    protected bool hasLowHealth;
    protected float deathTimer;


    //Getters and setters
    public int Hits
    {
        get { return hits; }
        set { hits = value; }
    }

    public bool HasItem
    {
        get { return hasItem; }
        set { hasItem = value; }
    }

    public bool HasBumper
    {
        get { return hasBumper; }
        set { hasBumper = value; }
    }

    public bool IsBoosting
    {
        get { return isBoosting; }
        set { isBoosting = value; }
    }

    protected void Bumper(GameObject a_bumper)
    {
        if(a_bumper != null)
        {
            //Set the bumper to active
            a_bumper.GetComponent<BumperScript>().isAlive = true;
            a_bumper.GetComponent<BumperScript>().lifeSpan = maxLifeSpan;
            a_bumper.SetActive(true);
        }
    }

    protected void Respawn()
    {
        timer = 0.0f;

        deathTimer += Time.deltaTime;

        if(deathTimer > respawnTime)
        {
            //Reset the positon of the entity
            transform.position = storedPosition;
            hits = 0;
        }
    }

    protected void PositionTimer()
    {
        deathTimer = 0.0f;

        //Start the timer
        timer += Time.deltaTime;

        //If the timer exceeds 3 seconds
        if (timer > 2.5f)
        {
            storedPosition = transform.position;
            timer = 0.0f;
        }
    }

    //Start
    protected void SpeedBoost()
    {
        //Go faster
        isBoosting = true;
    }

    protected void UpdateSpeedBoost()
    {
        //They have the speed boost
        boostTimer += Time.deltaTime;

        //Go slower
        if (boostTimer >= maxBoostTimer)
        {
            Debug.Log("Timer has gone off");

            boostTimer = 0.0f;
            isBoosting = false;
        }
    }


    protected virtual void OnTriggerEnter(Collider a_other)
    {
        if (a_other.tag == "Box")
        {
            hits++;
        }

        //If they are the items
        if (a_other.tag == "BumperItem")
        { 
            //If do not have item, get item
            if (hasItem != true)
            {
                hasItem = true;
                hasBumper = true;

                OnCollectItem(a_other.transform.parent.GetComponent<SpawnerScript>());

            }
        }
        else if (a_other.tag == "SpeedBoost")
        {
            //If they have item, get item
            if (hasItem != true)
            {
                hasItem = true;
                hasBumper = false;

                OnCollectItem(a_other.transform.parent.GetComponent<SpawnerScript>());
            }
        }
    }

    //Function runs when an item is collected
    protected virtual void OnCollectItem(SpawnerScript item)
    {
        item.Collect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    //TODO:
    //[PLAYER]
    //Items  (collisions and effects)
    //Hit counter
    //Realistic car movement (default turning is too high)
    //Create entity class
    //Raycasting to the walls to detect position on the track
    //Change pivot point of car
    //
    //[AI]
    


    //Variables
    [Header("General")]
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float rotation;
    [SerializeField]
    protected int maxHits;
    [SerializeField]
    protected float boostSpeed = 500.0f;
    [SerializeField]
    protected float maxBoostTimer = 1.0f;
    [SerializeField]
    protected float maxSpeed = 4000.0f;
    [SerializeField]
    private float maxLifeSpan = 5.0f;



    [Header("Do Not Tamper With")]
    [SerializeField]
    protected GameObject frontBumper;
    [SerializeField]
    protected GameObject leftBumper;
    [SerializeField]
    protected GameObject rearBumper;
    [SerializeField]
    protected GameObject rightBumper;
    


    //He attac, but he also protec (the variables)
    protected int hits;
    protected bool hasBumper;
    protected bool hasItem;
    protected int bumperSelect = 0;
    protected float boostTimer;
    protected bool isBoosting;
    

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


    protected void Bumper(GameObject a_bumper)
    {
        //Set the bumper to active
        a_bumper.GetComponent<BumperScript>().isAlive = true;
        a_bumper.GetComponent<BumperScript>().lifeSpan = maxLifeSpan;
        a_bumper.SetActive(true);
    }


    protected void SpeedBoost()
    {
        //They have the speed boost
        boostTimer += Time.deltaTime;

        //Go faster
        isBoosting = true;

        //Go slower
        if (boostTimer >= maxBoostTimer)
        {
            isBoosting = false;

            Debug.Log("Timer has gone off");

            boostTimer = 0.0f;
        }
    }


    protected virtual void OnTriggerEnter(Collider a_other)
    {
        //If they are the items
        if (a_other.tag == "BumperItem")
        {
            //IF do not have item, get item
            if (hasItem != true)
            {
                hasItem = true;
                hasBumper = true;
            }

        }
        else if (a_other.tag == "SpeedBoost")
        {
            //If they have item, get item
            if (hasItem != true)
            {
                hasItem = true;
                hasBumper = false;
            }
        }
    }
}

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
    public float speed;
    public float rotation;
    public int maxHits;
    public float boostSpeed = 500.0f;
    public float maxBoostTimer = 1.0f;
    public float maxSpeed = 4000.0f;

    [SerializeField]
    private bool isAI;

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
        a_bumper.GetComponent<BumperScript>().lifeSpan = 5.0f;
        a_bumper.SetActive(true);
        hasItem = false;
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

            //Return out
            boostTimer = 0.0f;
            hasItem = false;
        }
    }


    void OnTriggerEnter(Collider a_other)
    {
        //If they are the items
        if (a_other.tag == "BumperItem")
        {
            if (hasItem != true)
            {
                hasItem = true;
                hasBumper = true;
            }

        }
        else if (a_other.tag == "SpeedBoost")
        {
            if (hasItem != true)
            {
                hasItem = true;
                hasBumper = false;
            }
        }
    }
}

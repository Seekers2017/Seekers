using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    //TODO:
    //[PLAYER]
    //Raycasting to the walls to detect position on the track
    //[AI]
    //Improved movement 
    //Radius check (if another car is nearby)


    //Variables
    [Header("General")]
    [SerializeField]
    protected int maxHits;
    [SerializeField]
    protected float maxLifeSpan = 5.0f;
    [SerializeField]
    protected float maxBoostTimer = 1.0f;



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

    public bool IsBoosting
    {
        get { return isBoosting; }
        set { isBoosting = value; }
    }


    protected void Bumper(GameObject a_bumper)
    {
        //Set the bumper to active
        a_bumper.GetComponent<BumperScript>().isAlive = true;
        a_bumper.GetComponent<BumperScript>().lifeSpan = maxLifeSpan;
        a_bumper.SetActive(true);
    }


    //FIX TIMER BUG!!!! START
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
        //If they are the items
        if (a_other.tag == "BumperItem")
        { 

            //IF do not have item, get item
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

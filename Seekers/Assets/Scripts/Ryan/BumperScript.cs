using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    //Check if it's front bumper
    public bool isFrontBumper;

    //Getter and setter later
    public bool isAlive;

    //Setup Bumper's life span
    public float lifeSpan;

    private bool canHit;

    // Use this for initialization
    void Start()
    {
        //set Bumper is live
        isAlive = true;
        canHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        //check bumper's span every frame
        CheckBumperLiveSpan();
    }

    //when collide with the bumper
    void OnCollisionEnter(Collision a_other)
    {
        //if the other object is tagged Bumper
        if (a_other.transform.tag == ("Bumper"))
        {
            //if it's not colliding with front bumper
            if (a_other.gameObject.GetComponent<BumperScript>().isFrontBumper == false)
            {
                //set other's bumper not alive (bumper destryed)
                a_other.gameObject.GetComponent<BumperScript>().isAlive = false;
                a_other.gameObject.GetComponent<BumperScript>().gameObject.SetActive(false);
            }

            //if it's colliding with the bumper which is not entity itself's front bumper
            if (isFrontBumper == false)
            {
                //set self's bumper not alive (bumper destryed)
                isAlive = false;
                gameObject.SetActive(false);
            }
        }

        //if we are colliding with AI cars
        if (a_other.transform.tag == ("AI"))
        {
            if (canHit == true)
            {
                //add one hit to the AI car
                a_other.gameObject.GetComponent<AI>().Hits++;

                //Can't hit the car anymore
                canHit = false;
            }
            

            //at the same time destroy own bumper if its not a front bumper
            if (isFrontBumper == false)
            {
                //Destroy the bumper
                isAlive = false;
                gameObject.SetActive(false);
            }
        }

        //if we are colliding with a Player or a Player2 car
        if (a_other.transform.tag == ("Player") || a_other.transform.tag == ("Player2"))
        {
            if(canHit == true)
            {
                //add one hit to the player's car
                a_other.gameObject.GetComponent<PlayerManager>().Hits++;

                //Can't hit the car anymore
                canHit = false;
            }
            

            //at the same time destroy own bumper if its not a front bumper
            if (isFrontBumper == false)
            {
                //Destroy the bumper
                isAlive = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionStay(Collision a_other)
    {
        if (a_other.transform.tag == ("Player") || a_other.transform.tag == ("Player2") || a_other.transform.tag == ("AI"))
        {
            canHit = false;
        }
    }


    private void OnCollisionExit(Collision a_other)
    {
        if (a_other.transform.tag == ("Player") || a_other.transform.tag == ("Player2") || a_other.transform.tag == ("AI"))
        {
            canHit = true;
        }
    }

    //Check if bumper is alive
    private void CheckBumperLiveSpan()
    {
        //if life span <=0 and is alive is true and it's not a front bumper
        if (lifeSpan <= 0 && isAlive == true && isFrontBumper == false)
        {
            //set alive to false and make
            isAlive = false;
            //set it inactive
            gameObject.SetActive(false);
        }
        else //else keeps counting down
        {
            lifeSpan -= Time.deltaTime;
        }
    }
}
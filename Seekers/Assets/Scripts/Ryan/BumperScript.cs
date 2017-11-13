using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    public bool isFrontBumper;

    //Getter and setter later
    public bool isAlive;

    public float lifeSpan;
    
	// Use this for initialization
	void Start ()
    {
		isAlive = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckBumperLiveSpan();
    }

   ////Set up what will happen after collision
   //private void OnTriggerEnter(Collider other)
   //{
   //    //if the collision target is a Bumper and it's alive
   //    if (other.CompareTag("Bumper") && isFrontBumper == false)
   //    {
   //        //set isAlive to false
   //        other.gameObject.GetComponent<BumperScript>().isAlive = false;
   //        //Destroy the bumper
   //        gameObject.SetActive(false);
   //    }
   //}

    void OnCollisionEnter(Collision a_other)
    {
        if (a_other.transform.tag == ("Bumper"))
        {
            //set isAlive to false
            if(a_other.gameObject.GetComponent<BumperScript>().isFrontBumper == false)
            {
                a_other.gameObject.GetComponent<BumperScript>().isAlive = false;
                a_other.gameObject.GetComponent<BumperScript>().gameObject.SetActive(false);
            }
            
            if(isFrontBumper == false)
            {
                //Destroy the bumper
                isAlive = false;
                gameObject.SetActive(false);
            }
        }

        if (a_other.transform.tag == ("AI"))
        {
            a_other.gameObject.GetComponent<AI>().Hits++;

            if (isFrontBumper == false)
            {
                //Destroy the bumper
                isAlive = false;
                gameObject.SetActive(false);
            }
        }

        if(a_other.transform.tag == ("Player"))
        {
            a_other.gameObject.GetComponent<PlayerManager>().Hits++;

            if (isFrontBumper == false)
            {
                //Destroy the bumper
                isAlive = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void CheckBumperLiveSpan()
    {
        if (lifeSpan <= 0 && isAlive == true && isFrontBumper == false)
        {
            isAlive = false;
            gameObject.SetActive(false);
        }
        else
        {
            lifeSpan -= Time.deltaTime;
        }
    }
}

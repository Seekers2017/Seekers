using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    public bool isFrontBumper;
    private bool isAlive;

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

    //Set up what will happen after collision
    private void OnTriggerEnter(Collider other)
    {
        //if the collision target is a Bumper and it's alive
        if (other.CompareTag("Bumper") && isFrontBumper == false)
        {
            //set isAlive to false
            other.gameObject.GetComponent<BumperScript>().isAlive = false;
            //Destroy the bumper
            Destroy(other.transform.gameObject);
        }
    }

    private void CheckBumperLiveSpan()
    {
        if (lifeSpan <= 0 && isAlive == true && isFrontBumper == false)
        {
            isAlive = false;
            Destroy(this);
        }
        else
        {
            lifeSpan -= Time.deltaTime;
        }
    }
}

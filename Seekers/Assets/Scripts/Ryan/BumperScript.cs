using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource collisionSound;


    //Hit collision cooldown variables
    private float hitCooldown;
    private bool hasHit;
    private bool collidedPlayer;
    private PlayerManager currPlayer;
    private AI currAI;
    private bool canCollide;
    private bool addHit;
    private bool collided;
    private bool stillConnected;
    private bool playSound;

    //Check if it's front bumper
    public bool isFrontBumper;

    //Getter and setter later
    public bool isAlive;

    //Setup Bumper's life span
    public float lifeSpan;

    public bool Collided
    {
        get { return Collided; }
        set { Collided = value; }
    }

    // Use this for initialization
    void Start ()
    {
        //set Bumper is live
		isAlive = true;
        playSound = false;
        canCollide = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //check bumper's span every frame
        CheckBumperLiveSpan();
        Hit();

        //If we have just 
        if(playSound == true)
        {
            //PLay the sound
            collisionSound.Play();

            //Break out of the statement (and only play the sound once)
            playSound = false;
        }
        else
        {
            collisionSound.Stop();
        }
    }

    private void Hit()
    {
        //If we have hit something
        if (hasHit == true)
        {
            canCollide = false;

            //Start the timer
            hitCooldown += Time.deltaTime;

            //If we can add a hit
            if(addHit)
            {
                //If we have collided with the player
                if (collidedPlayer == true)
                {
                    //Add a hit onto the player
                    currPlayer.Hits++;
                    addHit = false;
                }
                else
                {
                    //Add hit to the AI
                    currAI.Hits++;
                    addHit = false;
                }

                //If the cooldown is greater than five seconds
                if (hitCooldown > 5.0f && stillConnected == false)
                {
                    canCollide = true;
                }
            }
            
        }
    }


    //when collide with the bumper
    void OnCollisionEnter(Collision a_other)
    {
        

        //if the other object is tagged Bumper
        if (a_other.transform.tag == ("Bumper"))
        {
            //if it's not colliding with front bumper
            if(a_other.gameObject.GetComponent<BumperScript>().isFrontBumper == false)
            {
                //set other's bumper not alive (bumper destryed)
                a_other.gameObject.GetComponent<BumperScript>().isAlive = false;
                a_other.gameObject.GetComponent<BumperScript>().gameObject.SetActive(false);
            }
            
            //if it's colliding with the bumper which is not entity itself's front bumper
            if(isFrontBumper == false)
            {
                //set self's bumper not alive (bumper destryed)
                isAlive = false;
                gameObject.SetActive(false);
            }  
        }

        //If we can collide
        if(canCollide)
        {

            //if we are colliding with AI cars
            if (a_other.transform.tag == ("AI"))
            {
                //We have hit the car
                currAI = a_other.gameObject.GetComponent<AI>();
                hitCooldown = 0.0f;
                hasHit = true;
                collidedPlayer = false;
                addHit = true;

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
                //Have hit the car
                currPlayer = a_other.gameObject.GetComponent<PlayerManager>();
                hitCooldown = 0.0f;
                hasHit = true;
                collidedPlayer = true;
                addHit = true;

                //at the same time destroy own bumper if its not a front bumper
                if (isFrontBumper == false)
                {
                    //Destroy the bumper
                    isAlive = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    //To make sure that they do not constantly hit each other
    private void OnCollisionStay(Collision a_other)
    {
        if(a_other.transform.tag == ("Player") || a_other.transform.tag == ("Player2") || a_other.transform.tag == ("AI"))
        {
            stillConnected = true;
        }
    }

    private void OnCollisionExit(Collision a_other)
    {
        if (a_other.transform.tag == ("Player") || a_other.transform.tag == ("Player2") || a_other.transform.tag == ("AI"))
        {
            stillConnected = false;
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

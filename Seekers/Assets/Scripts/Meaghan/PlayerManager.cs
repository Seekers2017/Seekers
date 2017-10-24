using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Entity {

    [Header("Player Exclusives")]
    [Range(0.1f, 1f)]
    [Tooltip("Speed at which the car flashes red.")]
    [SerializeField]
    private float flashSpeed;
    

    //Variables 
    private IngameUIScript uiScript;
    private Color defaultColour;
    private GameObject carBody;
    private Renderer renderer;
    private WheelDrive playerMove;
    private Rigidbody rb;

    // Use this for initialization
    void Awake()
    {
        uiScript = FindObjectOfType<IngameUIScript>();
        defaultColour = gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color;
        renderer = gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>();
        playerMove = gameObject.GetComponent<WheelDrive>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        bumperSelect = 0;
        hasLowHealth = false;
        leftBumper.SetActive(false);
        rightBumper.SetActive(false);
        rearBumper.SetActive(false);
    }

    void Update()
    {
        //Respawn check
        if(hits >= maxHits)
        {
            //Set all values back and respawn
            playerMove.AbilityToDrive = false;
            rb.drag = 3.0f;
            hasLowHealth = false;
            Respawn();
        }
        else
        {
            //Playe can drive and store position to respawn at
            playerMove.AbilityToDrive = true;
            PositionTimer();
        }

        Items();

        //Boost
        if (isBoosting)
        {
            UpdateSpeedBoost();
        }

        //Low health iteration start
        if(hits == maxHits - 1)
        {
            hasLowHealth = true;
        }

        if(hasLowHealth)
        {
            //Start the function
            LowHealth();
        }
        else
        {
            //Set it back to the original colour
            renderer.material.color = defaultColour;
        }
    }

  


    IEnumerator Flasher()
    {
        //While the player has low health
        while (hasLowHealth)
        {
            //Cycles through the two colours and alters them while in the loop
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(flashSpeed);
            renderer.material.color = defaultColour;
            yield return new WaitForSeconds(flashSpeed);
        }
    }



    void LowHealth()
    {
        //Start flashing
        StartCoroutine(Flasher());
    }

    void Items()
    {
        if (hasItem == true)
        {
            if (hasBumper == true)
            {
                if (Input.GetButtonDown("LeftBumper"))
                {
                    //Traverse through item selection
                    if (bumperSelect >= 3)
                    {
                        bumperSelect = 0;
                    }
                    else
                    {
                        bumperSelect++;
                    }
                }

                if (Input.GetButton("Fire3"))
                {
                    //Input the selection to the car
                    if (bumperSelect == 0)
                    {
                        Bumper(leftBumper);
                        hasItem = false;
                    }
                    else if (bumperSelect == 1)
                    {
                        Bumper(rightBumper);
                        hasItem = false;
                    }
                    else if (bumperSelect == 2)
                    {
                        Bumper(rearBumper);
                        hasItem = false;
                    }
                }
            }
            else //speed boost
            {
                if (Input.GetButton("Fire3"))
                {
                    //Go fast
                    SpeedBoost();
                    hasItem = false;
                }
            }
        }
        else
        {
            bumperSelect = 0;
        }
    }






    protected override void OnCollectItem(SpawnerScript item)
    {
        //Calls Entity's
        base.OnCollectItem(item);

        //Update the UI
        uiScript.SetCollectedItem(item.CurrItem);
    }
}

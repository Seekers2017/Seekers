using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Entity {


    IngameUIScript uiScript;


    // Use this for initialization
    void Start()
    {
        uiScript = FindObjectOfType<IngameUIScript>();

        leftBumper.SetActive(false);
        rightBumper.SetActive(false);
        rearBumper.SetActive(false);
    }

    void Update()
    {
        Items();
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
            else
            {
                if (Input.GetButton("Fire3"))
                {
                    //Go fast
                    SpeedBoost();
                    hasItem = false;
                }
            }
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

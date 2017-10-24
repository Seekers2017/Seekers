using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Entity {


    IngameUIScript uiScript;
    Color defaultColour;
    GameObject carBody;
    Renderer renderer;

    // Use this for initialization
    void Awake()
    {
        uiScript = FindObjectOfType<IngameUIScript>();
        defaultColour = gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color;
        renderer = gameObject.GetComponent<Renderer>();
    }

    void Start()
    {
        leftBumper.SetActive(false);
        rightBumper.SetActive(false);
        rearBumper.SetActive(false);
    }

    void Update()
    {
        Items();

        if (isBoosting)
        {
            UpdateSpeedBoost();
        }

    }

    void LowHealth()
    {

    }


    IEnumerator Flasher()
    {
        for (int i = 0; i < 5; i++)
        {
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(.1f);
            renderer.material.color = defaultColour;
            yield return new WaitForSeconds(.1f);
        }
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
    }

    protected override void OnCollectItem(SpawnerScript item)
    {
        //Calls Entity's
        base.OnCollectItem(item);

        //Update the UI
        uiScript.SetCollectedItem(item.CurrItem);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


public class SpawnerScript : MonoBehaviour
{
    ///////////////////////////////////////
    ///Will do the spawnRate stuff later //
    //public float healthKitRate;        //
    //public float bumperRate;           //
    //public float speedBoostRate;       //
    ///////////////////////////////////////

    public float minRespawnTime;
    public float maxRespawnTime;

    private float timeUntilRespawn;
    private bool isSpawned;

    private ItemNum currActivatedItemNum;

    private int itemPickedUpTimes;

    [SerializeField]
    private GameObject bumper;
    [SerializeField]
    private GameObject healthKit;
    [SerializeField]
    private GameObject speedBoost;

    // Use this for initialization
    void Awake ()
	{
        //////////////////////////////////////
        ///Will do the spawnRate stuff later//
        //healthKitRate = 0;                //
        //bumperRate = 0;                   //
        //speedBoostRate = 0;               //
        //////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////
        ///This block is only for referencing                                         //
        //for (int i = 0; i < gameObject.transform.childCount; i++)                   //
        //{                                                                           //
        //    GameObject itemTypeList = gameObject.transform.GetChild(i).gameObject;  //
        //    itemTypeList.SetActive(false);                                          //
        //}                                                                           //
        ////////////////////////////////////////////////////////////////////////////////

        SpawnRandItem();
        minRespawnTime = 1.0f;
        maxRespawnTime = 3.0f;
        itemPickedUpTimes = 0;
    }
	
	// Update is called once per frame
	void Update ()
	{
        //if timeUntilRespawn count down to 0 && the no item is spawned, spawn an item
        if (timeUntilRespawn <= 0 && isSpawned == false)
        {
            SpawnRandItem();
        }
        //else keep count down
        else
        {
            timeUntilRespawn -= Time.deltaTime;
        }
    }

    //To spawn a random item base on ENUM class
    private void SpawnRandItem()
    {
        //Create an array to store Enum class's values
        Array itemTypeList = Enum.GetValues( typeof (ItemNum) );
        //Select a random value from that array and cast as an ItemNum
        currActivatedItemNum = (ItemNum)itemTypeList.GetValue(Random.Range(0, itemTypeList.Length));
        //Create an empty GameObject and assign instansiated item to it depending on the switch
        GameObject newSpawnedItem = null;
        //Set up a switch to decide which item to instantiate
        switch (currActivatedItemNum)
        {
            //creates a gameobject of (bumper) with it's parent being "this", at the same time zero out the axis
            case ItemNum.Bumper:
                newSpawnedItem = Instantiate(bumper, gameObject.transform.position, Quaternion.identity);
                break;
            //creates a gameobject of (healthKit) with it's parent being "this", at the same time zero out the axis
            case ItemNum.HeathKit: 
                newSpawnedItem = Instantiate(healthKit, gameObject.transform.position, Quaternion.identity);
                break;
            //creates a gameobject of (speedBoost) with it's parent being "this", at the same time zero out the axis
            case ItemNum.SpeedBoost:
                newSpawnedItem = Instantiate(speedBoost, gameObject.transform.position, Quaternion.identity);
                break;
            default: Debug.Log("Random Spawner Invalid ItemNum");
                break;
        }

        //Set the axis of the newly spawned item to the parent's as well as the item object itself
        newSpawnedItem.transform.SetParent(gameObject.transform);
        //Set item is spawned
        isSpawned = true;

        ///////////////////////////////////////////////////////////////////////////////////
        ///This block is only for referencing                                            //
        //int rand = Random.Range(0, this.gameObject.transform.childCount);              //
        //GameObject itemTypeList = this.gameObject.transform.GetChild(rand).gameObject; //
        //itemTypeList.SetActive(true);                                                  //
        ///////////////////////////////////////////////////////////////////////////////////
    }

    //As long as player object stays in the spawner's collision range, triggers this function
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && isSpawned == true)
        {
            isSpawned = false;
            timeUntilRespawn = Random.Range(minRespawnTime, maxRespawnTime);
            Destroy(gameObject.transform.GetChild(0).gameObject);

            itemPickedUpTimes++;

            ////////////////////////////////////////////////////////////////////////////////////
            //other.gameObject.GetComponent<PlayerScript>.giveItemEffects (currActivatedItem) //
            ///we will do above line when the player class is done                             //
            ////////////////////////////////////////////////////////////////////////////////////
        }
    }
}

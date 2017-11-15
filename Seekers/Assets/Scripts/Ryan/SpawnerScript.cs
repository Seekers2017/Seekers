using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

///////////////////////////////////////////////////////
// DON'T TOUCH THE CODE WITHOUT DISCUSSING WITH RYAN //
///////////////////////////////////////////////////////

public class SpawnerScript : MonoBehaviour
{
    //Create a list to store spawning rate for each item
    private List<uint> itemSpawnRateList = new List<uint>();
    
    //Spawning rates that can be entered by designers
    //note: the rates represnt by RATIO ex: (1, 2 ,7) == (10%, 20%, 70%) 
    public uint healthKitRate;
    public uint bumperRate;
    public uint speedBoostRate;

    //Max and Min respawn time;
    public float minRespawnTime;
    public float maxRespawnTime;

    //Set up spawning time range
    private float timeUntilRespawn;
    private bool isSpawned;
    private bool isPickedUp;

    //Fetch items' Enum number from Enum class
    public ItemNum CurrItem
    {
        get { return currActivatedItemNum; }
    }

    private ItemNum currActivatedItemNum;

    //Monitor Pick Up Times for all items
    private int totalPickedUpTimes;
    private int HealthKitPickedUpTimes;
    private int BumperPickedUpTimes;
    private int SpeedBoostPickedUpTimes;

    //Serialize Field for designers to assign prefabs(3D models) to items
    [SerializeField]
    private GameObject bumper;
    [SerializeField]
    private GameObject healthKit;
    [SerializeField]
    private GameObject speedBoost;

    private IngameUIMultiScript inGameUIMultiScript;
    private IngameUIScript inGameUIScript;

    void Start()
    {
        itemSpawnRateList.Add(healthKitRate);    //index 0
        itemSpawnRateList.Add(bumperRate);       //index 1
        itemSpawnRateList.Add(speedBoostRate);   //index 2

        SpawnRandItem();

        totalPickedUpTimes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        //if timeUntilRespawn count down to 0 && the no item is spawned, spawn an item
        if (timeUntilRespawn <= 0 && isSpawned == false)
        {
            SpawnRandItem();

            Debug.Log("index 0: " + itemSpawnRateList[0]);
            Debug.Log("index 1: " + itemSpawnRateList[1]);
            Debug.Log("index 2: " + itemSpawnRateList[2]);
        }
        else //else keep counting down
        {
            timeUntilRespawn -= Time.deltaTime;
        }
    }

    //Spawn an item base from ENUM class. The rate is given by ItemRateGenerator()
    private void SpawnRandItem()
    {
        //Create an array to store Enum class's values
        Array itemTypeList = Enum.GetValues(typeof(ItemNum));
        //Select a value from that array and cast as an ItemNum, based on ItemRateGenerator()
        currActivatedItemNum = (ItemNum)itemTypeList.GetValue( ItemRateGenerator(itemSpawnRateList, 1000) );
        //Create an empty GameObject and assign instansiated item to it depending on the switch
        GameObject newSpawnedItem = null;
        //Set up a switch to decide which item to instantiate
        switch (currActivatedItemNum)
        {
            //creates a gameobject of (healthKit) with it's parent being "this", at the same time zero out the axis
            case ItemNum.HeathKit:
                newSpawnedItem = Instantiate(healthKit, gameObject.transform.position, Quaternion.identity);
                HealthKitPickedUpTimes++;
                break;

            //creates a gameobject of (bumper) with it's parent being "this", at the same time zero out the axis
            case ItemNum.Bumper:
                newSpawnedItem = Instantiate(bumper, gameObject.transform.position, Quaternion.identity);
                BumperPickedUpTimes++;
                break;

            //creates a gameobject of (speedBoost) with it's parent being "this", at the same time zero out the axis
            case ItemNum.SpeedBoost:
                newSpawnedItem = Instantiate(speedBoost, gameObject.transform.position, Quaternion.identity);
                SpeedBoostPickedUpTimes++;
                break;

            default:
                Debug.Log("Random Spawner Invalid ItemNum");
                break;
        }

        //Set the axis of the newly spawned item to the parent's as well as the item object itself
        newSpawnedItem.transform.SetParent(gameObject.transform);
        //Set item is spawned
        isSpawned = true;
        //Set item is picked up
        isPickedUp = false;
    }

    //Call this when the player collects the item
    //destroy the item, and reset the spawner
    public void Collect(int a_playerIndex = 0)
    {
        isSpawned = false;
        isPickedUp = true;
        timeUntilRespawn = Random.Range(minRespawnTime, maxRespawnTime);

        Destroy(gameObject.transform.GetChild(0).gameObject);

        //loading InGameUIScript here just in case it hasn't been loaded
        if(inGameUIScript == null)
            inGameUIScript = FindObjectOfType<IngameUIScript>();

        inGameUIMultiScript = FindObjectOfType<IngameUIMultiScript>();

        //tell the ui that you've collected this item type (MOVE THIS LINE INTO WHERE THE PLAYER COLLECTS IT)
        //check if we are in Multi or Single mode by checking if the script is active
        GameStateID currState = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>().currState;

        //if we are playing single play mode
        if (currState == GameStateID.InGame)
        {
            //set single play mode's IngameUI to change the item icon
            inGameUIScript.SetCollectedItem(currActivatedItemNum);
        }

        //if we are playing multi play mode
        if (currState == GameStateID.InGameMuilti)
        {
            //set multi play mode's IngameUI to change the item icon
            inGameUIMultiScript.SetCollectedItem(currActivatedItemNum, a_playerIndex);
        }

        totalPickedUpTimes++;
    }

    //Generates a number based on the spawning rate
    private int ItemRateGenerator(List<uint> a_List, uint maxVal) //maxVal recommended to be big ex: 1000 up
    {
        uint sum = 0;
        uint factor = 0;
        uint randNumPicker = 0;

        //sum up all values in the list
        for (int i = 0; i < a_List.Count; i++)
        {
            sum += a_List[i];
        }

        factor = maxVal / sum; //setup the factor

        //blow up the figures in the list by factor
        for (int i = 0; i < a_List.Count; i++)
        {
            a_List[i] = a_List[i] * factor;
        }

        sum = sum * factor; //replace sum with normalized sum

        randNumPicker = (uint)Random.Range(1, sum); //pick up a randomized number from 1 to the new normalized sum

        if (a_List.Count <= 1) //if there is only 1 item rate or no item rate in the list
        {
            return 0;
        }
        else //if there are more than 1 item rate in the list
        {
            uint iTotal = 0; //Set up an icreament total uint

            for (int i = 0; i < a_List.Count; i++) //check which range randItemPicker's number falls into
            {
                //if the random number is less then curr index's content number plus previous total number(a_List[i] + iTotal) and greater than previous total number (iTotal)
                if (randNumPicker <= a_List[i] + iTotal && randNumPicker > iTotal) 
                {
                    return i;
                }

                iTotal += a_List[i]; //adds up iTotal for the next check
            }
        }

        return 99999; //if the for loop is not working properly, will return this crazy number so there will ba an error in console
    }
}


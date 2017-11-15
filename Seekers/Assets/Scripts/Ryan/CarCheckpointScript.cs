using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckpointScript : MonoBehaviour
{
    //var to store entity's current lap
    public int currLap;
    //var to store entity's current check point count
    public int currCheckpointCount;
    //var to store entity's start position
    public Vector3 startPos;
    
    //list to store every checkpoint's transform
    private List<Transform> checkpointList;
    //checkpoint set (parent object)
    private GameObject checkpointSet;
    //get rank script from game manager
    private RankScript rankScript;

    // Use this for initialization
    void Start ()
    {
        //get rank script from game manager
        rankScript = GameObject.Find("GameManager").GetComponent<RankScript>();
        //list to store every checkpoint's transform
        checkpointList = rankScript.CheckpointList;
        //Find the CheckpointSet object (parent group of all checkpoint objects)
        checkpointSet  = GameObject.FindGameObjectWithTag("CheckpointSet");
        //initialize current lap to 1
        currLap = 1;
        //initialize the checkpoints the entity has traversed to 0
        currCheckpointCount = 0;
        //use entity's current position as start position
        startPos = gameObject.transform.position;
    }



    //On trigger enter when player goes through a checkpoint 
    private void OnTriggerEnter(Collider other)
    {
        //check if other's tag is "Checkpoint"
        if (other.CompareTag("Checkpoint"))
        {
            //if the checkpoint player has gone through the last checkpoint in the List && is going for the first one
            //which means player is just about to finish the lap
            if (currCheckpointCount == checkpointSet.transform.childCount && other.transform == checkpointList[0])
            {
                //current lap count +1 and set current check point back to 1
                currLap++;
                currCheckpointCount = 1;
            }

            //if the checkpoint player is about to go through is the next in the list
            if (other.transform == checkpointList[currCheckpointCount])
            {
                //add player's checkpoint count when it's less than the child count in th list
                if (currCheckpointCount < checkpointSet.transform.childCount)
                {
                    currCheckpointCount++;
                }
            }
        }
    }
}

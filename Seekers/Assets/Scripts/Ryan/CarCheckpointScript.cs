using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckpointScript : MonoBehaviour
{
    public int currLap;
    public int currCheckpointCount;

    public Vector3 startPos;

    private List<Transform> checkpointList;
    private GameObject checkpointSet;
    private RankScript rankScript;

    // Use this for initialization
    void Start ()
    {
        rankScript = GameObject.Find("GameManager").GetComponent<RankScript>();
        checkpointList = rankScript.CheckpointList;
        checkpointSet  = GameObject.FindGameObjectWithTag("CheckpointSet");
        currLap = 1;
        currCheckpointCount = 0;
        startPos = gameObject.transform.position;
    }



    //On trigger enter when player goes through a checkpoint 
    private void OnTriggerEnter(Collider other)
    {
        //check if the tag is "Checkpoint"
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
                //on add player's checkpoint count when it's less than the child count in th list
                if (currCheckpointCount < checkpointSet.transform.childCount)
                {
                    currCheckpointCount++;
                }
            }
        }
    }
}

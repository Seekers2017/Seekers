using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RankScript : MonoBehaviour
{
    //create a list to store car's checkpointScript
    private  List<CarCheckpointScript> rankList;
    //get the checkpointSet
    private GameObject checkpointSet;
    //create a checkpointList to store the checkpoints
    private List<Transform> checkpointList;


    // Use this for initialization
    void Awake()
    {
        //get the checkpointSet by tag
        checkpointSet = GameObject.FindGameObjectWithTag("CheckpointSet");
        //create a checkpointList to store the checkpoints
        checkpointList = new List<Transform>();
        //create a list to store cars' (entities') checkpointScript
        rankList = new List<CarCheckpointScript>();

        //add all checkpoints' transform into checkpointList
        AddCheckpoints();
        //add all car entities into rankList
        AddEntities();
    }

    // Update is called once per frame
    void Update()
    {
        //sort all cars in rankList
        SortAllCars(rankList);
    }

    //Sort the rank with C# sorting
    private void SortAllCars(List<CarCheckpointScript> a_List)
    {
        ///Method 1. Selection Sorting (Manual)

        ///Method 2. C# Sort (Automatic)
        a_List.Sort(CompareRank);

    }

    //implement CompareRank function by using C# sorting
    //Should only compare 2 cars (1, 0, -1)
    //Because C# sorting is minimum start => [1 is smallest] [0 is tie] [-1 is biggest]
    private int CompareRank(CarCheckpointScript car1, CarCheckpointScript car2)
    {
        //Set Current Lap count as the first check condition
        if (car1.currLap > car2.currLap)
        {
            return -1;
        }

        else if (car1.currLap < car2.currLap)
        {
            return 1;
        }
        else //if both cars in on same lap
        {
            //compare current check point
            if (car1.currCheckpointCount > car2.currCheckpointCount)
            {
                return -1;
            }
            else if (car1.currCheckpointCount < car2.currCheckpointCount)
            {
                return 1;
            }
            else // if both having same checkpoint
            {
                //var to define which two checkpoints are two car's next checkpoint transform
                //(use this to check if it's the last check point or not)
                Transform car1NextCheckpoint;
                Transform car2NextCheckpoint;

                //if car1 has NOT traversed the last checkpoint (the one before finish/start line)
                if (car1.currCheckpointCount < checkpointList.Count - 1)
                {
                    //car1's next check point is the the current one + 1 of the checkpointList
                    car1NextCheckpoint = checkpointList[(car1.currCheckpointCount) + 1];
                }
                else //if car1 HAS traversed the last checkpoint (the one before finish line)
                {
                    //car1's next check point should be the first one in the checkpointList, which is the finish/start line
                    car1NextCheckpoint = checkpointList[0];
                }

                //if car2 has NOT traversed the last checkpoint (the one before finish/start line)
                if (car2.currCheckpointCount < checkpointList.Count - 1)
                {
                    //car2's next check point is the the current one + 1 of the checkpointList
                    car2NextCheckpoint = checkpointList[(car2.currCheckpointCount) + 1];
                }
                else //if car2 HAS traversed the last checkpoint (the one before finish line)
                {
                    //car2's next check point should be the first one in the checkpointList, which is the finish/start line
                    car2NextCheckpoint = checkpointList[0];
                }

                //With both cars' next checkpoints well defined, start to compare the distance to the next checkpoint they are going
                if ((car1.transform.position - car1NextCheckpoint.position).sqrMagnitude
                    < (car2.transform.position - car2NextCheckpoint.position).sqrMagnitude)
                {
                    return -1;
                }

                if ((car1.transform.position - car1NextCheckpoint.position).sqrMagnitude
                    > (car2.transform.position - car2NextCheckpoint.position).sqrMagnitude)
                {
                    return 1;
                }
            }
        }
        // if all conditions are the same, they are tie
        return 0;
    }

    //Add all the child checkpoints' transform into checkpointList
    private void AddCheckpoints()
    {
        //check how many children objects under checkpointSet and Add
        for (int i = 0; i < checkpointSet.transform.childCount; i++)
        {
            checkpointList.Add(checkpointSet.transform.GetChild(i).transform);
        }
    }

    private void AddEntities()
    {
        //Find all object with AI and Player and Player2 Tag, save them in two arrays separately
        GameObject[] aiTaggedObj = GameObject.FindGameObjectsWithTag("AI");
        GameObject[] playerTaggedObj = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] player2TaggedObj = GameObject.FindGameObjectsWithTag("Player2");

        //Squeeze them in to rankList
        foreach (GameObject gameObject in aiTaggedObj)
        {
            rankList.Add(gameObject.GetComponent<CarCheckpointScript>());
        }

        foreach (GameObject gameObject in playerTaggedObj)
        {
            rankList.Add(gameObject.GetComponent<CarCheckpointScript>());
        }

        foreach (GameObject gameObject in player2TaggedObj)
        {
            rankList.Add(gameObject.GetComponent<CarCheckpointScript>());
        }
    }

    //Getter function for other object to call and get the checkpointList
    public List<Transform> CheckpointList
    {
        get { return checkpointList; }
    }

    //Getter function for other object to call and get a car's current rank
    public int GetRank(CarCheckpointScript a_car)
    {
        int rank;

        for (int i = 0; i < rankList.Count; i++)
        {
            if (a_car == rankList[i])
            {
                rank = i + 1;
                return rank;
            }
        }

        return 0;
    }

    //Getter function for other object to call and get the entire rank list
    public List<CarCheckpointScript> GetRankList()
    {
        return rankList;
    }
}

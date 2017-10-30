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

    //Sort the rank
    private void SortAllCars(List<CarCheckpointScript> a_List)
    {
        //create a temp list for sorting
        //List<CarCheckpointScript> tempList = new List<CarCheckpointScript>();

        ///Method 1. Selection Sorting (Manual)

        ///Method 2. C# Sort (Automatic)
        a_List.Sort(CompareRank);

    }

    //implement CompareRank function
    //use C# sort instead
    //Should only compare 2 cars (1, 0, -1)
    //Because C# sort is minimum start => [1 is smallest] [0 is tie] [-1 is biggest]
    private int CompareRank(CarCheckpointScript car1, CarCheckpointScript car2)
    {
        //Get the Transform List from car's checkpointList
        //(Will have to move the checkpointList to Rank Script later)
        //List<Transform> chkpntTransList = car1.checkpointList;

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
                //compare the distance to the next checkpoint they are going
                if ((car1.transform.position - checkpointList[(car1.currCheckpointCount) + 1].position).sqrMagnitude
                    < (car2.transform.position - checkpointList[(car2.currCheckpointCount) + 1].position).sqrMagnitude)
                {
                    return -1;
                }
                if ((car1.transform.position - checkpointList[(car1.currCheckpointCount) + 1].position).sqrMagnitude
                    > (car2.transform.position - checkpointList[(car2.currCheckpointCount) + 1].position).sqrMagnitude)
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
        //Find all object with AI and Player Tag, save them in two arrays separately
        GameObject[] aiTaggedObj = GameObject.FindGameObjectsWithTag("AI");
        GameObject[] playerTaggedObj = GameObject.FindGameObjectsWithTag("Player");

        //Squeeze them in to rankList
        foreach (GameObject gameObject in aiTaggedObj)
        {
            rankList.Add(gameObject.GetComponent<CarCheckpointScript>());
        }
        foreach (GameObject gameObject in playerTaggedObj)
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

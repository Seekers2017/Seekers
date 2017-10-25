using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RankScript : MonoBehaviour
{
    private static List<CarCheckpointScript> rankList = new List<CarCheckpointScript>();

    // Use this for initialization
    void Awake ()
    {
        GameObject[] aiTaggedObj = GameObject.FindGameObjectsWithTag("AI");
        GameObject[] playerTaggedObj = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject gameObject in aiTaggedObj)
        {
            rankList.Add(gameObject.GetComponent<CarCheckpointScript>());
        }
        foreach (GameObject gameObject in playerTaggedObj)
        {
            rankList.Add(gameObject.GetComponent<CarCheckpointScript>());
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        SortAllCars(rankList);
    }


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

    public List<CarCheckpointScript> GetRankList()
    {
        return rankList; 
    }

    private void SortAllCars(List<CarCheckpointScript> a_List)
    {
        //create a temp list for sorting
        List<CarCheckpointScript> tempList = new List<CarCheckpointScript>();

        //Method 1. Bubble Sorting (Manual)
        bool sorted = false;

        while (sorted == false)
        {
            sorted = true;

            for (int i = 0; i < a_List.Count; i++)
            {
                for (int j = 0; j < a_List.Count; j++)
                {
                    //car i was ahead
                    if( CompareRank(a_List[i], a_List[j]) == 1 )
                    {
                        tempList.Add(a_List[i]);
                        sorted = false;
                    }

                    if (CompareRank(a_List[i], a_List[j]) == -1)
                    {
                        tempList.Add(a_List[j]);
                        sorted = false;
                    }

                }
            }
        }

        ///Method 2. C# Sort (Automatic)
        //tempList.AddRange(a_List);
        //tempList.Sort(CompareRank);


        a_List.RemoveRange(0, a_List.Count);
        a_List.AddRange(tempList);
    }

    //implement CompareRank function
    //use C# sort instead
    //Should only compare 2 cars (1, 0, -1)
    private int CompareRank(CarCheckpointScript car1, CarCheckpointScript car2)
    {
        //Get the Transform List from car's checkpointList
        //(Will have to move the checkpointList to Rank Script later)
        List<Transform> chkpntTransList = car1.checkpointList;

        //Set Current Lap count as the first check condition
        if (car1.currLap > car2.currLap)
        {
            return 1;
        }

        else if (car1.currLap < car2.currLap)
        {
            return -1;
        }
        else //if both cars in on same lap
        {
            //compare current check point
            if (car1.currCheckpointCount > car2.currCheckpointCount)
            {
                return 1;
            }
            else if (car1.currCheckpointCount < car2.currCheckpointCount)
            {
                return -1;
            }
            else // if both having same checkpoint
            {
                //compare the distance to the next checkpoint they are going
                if ( (car1.transform.position - chkpntTransList[(car1.currCheckpointCount) + 1].position).sqrMagnitude 
                    < (car2.transform.position - chkpntTransList[(car2.currCheckpointCount) + 1].position).sqrMagnitude )
                {
                    return 1;
                }
                if ( (car1.transform.position - chkpntTransList[(car1.currCheckpointCount) + 1].position).sqrMagnitude
                    > (car2.transform.position - chkpntTransList[(car2.currCheckpointCount) + 1].position).sqrMagnitude )
                {
                    return -1;
                }
            }
        }
        // if all conditions are the same, they are tie
        return 0;
    }
}

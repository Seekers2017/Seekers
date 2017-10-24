using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RankScript : MonoBehaviour
{
    private static List<CarCheckpointScript> rankList = new List<CarCheckpointScript>();

    // Use this for initialization
    void Start ()
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

    private void SortByLap(List<CarCheckpointScript> a_List)
    {

        List<CarCheckpointScript> tempList = new List<CarCheckpointScript>();

        //add all cars from rankList into tempList
        

        //sort tempList
        //1.
        bool sorted = false;
        

        while (sorted)
        {
            for (int i = 0; i < a_List.Count; i++)
            {
                for (int j = 0; j < a_List.Count; j++)
                {
                    //car i was ahead
                    if( CompareRank(a_List[i], a_List[j]) == 1)
                    {
                        //put a_List[i] in list
                    }

                    
                }
            }
        }

        //OR 2. 
        tempList.Sort(CompareRank);


        //rankList = tempList
    }

    //implement CompareRank function
    //use C# sort instead
    //Should only compare 2 cars (1, 0, -1)
    private int CompareRank(CarCheckpointScript car1, CarCheckpointScript car2)
    {
        //Get the Transform List from car's checkpointList
        //(Will have to move the checkpointList to Rank Script later)
        List<Transform> chkpntTransList = car1.checkpointList;

        //
        if (car1.currLap > car2.currLap)
        {
            return 1;
        }

        else if (car1.currLap < car2.currLap)
        {
            return -1;
        }
        else
        {
            if (car1.currCheckpointCount > car2.currCheckpointCount)
            {
                return 1;
            }
            else if (car1.currCheckpointCount < car2.currCheckpointCount)
            {
                return -1;
            }
            else
            {
                if ( (car1.transform.position - chkpntTransList[(car1.currCheckpointCount) + 1].position).sqrMagnitude 
                    > (car2.transform.position - chkpntTransList[(car2.currCheckpointCount) + 1].position).sqrMagnitude )
                {
                    return 1;
                }
                else if ( (car1.transform.position - chkpntTransList[(car1.currCheckpointCount) + 1].position).sqrMagnitude
                    > (car2.transform.position - chkpntTransList[(car2.currCheckpointCount) + 1].position).sqrMagnitude )
                {
                    return -1;
                }
            }
        }

        return 0;


        //if car1 lap is greater than car2's current lap
        //return 1

        //if car2's lap is greater than car1's lap
        //return -1




        //down here - they're on the same lap, compare check points.

        //if car1 checkpoint > car2 checkpoint
        //return 1

        //if car2 checkpoint > car1 checkpoint
        //return -1


        //here: same lap, same checkpoint, compare distances (car1.transform.position - checkpoint.position).sqrmagnitude < (car2 - checkpoint).sqrmagnitude
    }
}

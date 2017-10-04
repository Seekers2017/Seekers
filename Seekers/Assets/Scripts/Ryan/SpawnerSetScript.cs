using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerSetScript : MonoBehaviour
{
    public uint spawnerCount;

    [SerializeField]
    private GameObject spawner;
    private List<Vector3> spawnerPos;
    private List<GameObject> spawnerObj;
    private Vector3 rightPos;
    private Vector3 leftPos;
    private float disToRight;

	// Use this for initialization
	void Awake ()
    {
        //new a Dyanmic Array List to store our spawner positions
        spawnerPos = new List<Vector3>();

        //new a Dyanmic Array List to store our actual spawner objects
        spawnerObj = new List<GameObject>();

        //get the distance from the centre axis to the rightmost
        disToRight = transform.localScale.x / 2.0f;

        //Vec3 axises all the way to the rightmost
        rightPos = gameObject.transform.right * disToRight;

        //Vec3 axises all the way to the left most
        //NOTE: to the left is negative number, therefore multiply by negative disToRight
        leftPos = gameObject.transform.right * -disToRight;

        SpawnerSetGenerator();
    }
	
	// Update is called once per frame
	void Update ()
    {
      
    }

    //Generate spawners based on the stored in the List<Vector3> spawnerPos
    private void SpawnerSetGenerator()
    {
        //Activate SpawnerPosGenerator() to generate positions
        SpawnerPosGenerator(spawnerPos);

        //reset THIS box's scale, otherwise the sale of the meshes will be stretched along with THIS box
        transform.localScale = Vector3.one;

        //create an empty null Game Object waiting to be assigned
        GameObject newSpawner = null;

        for (int i = 0; i < spawnerCount; i++)
        {
            //Instantiate a spawner in the world's centre
            newSpawner = Instantiate(spawner, Vector3.zero, Quaternion.identity);
            //Set the sapwner's parent to THIS box 
            newSpawner.transform.SetParent(transform, false);
            //Now we can assign the positions in the spawnerPos List
            newSpawner.transform.localPosition = spawnerPos[i];
            //Finally, Add it to the spawnerObj List
            spawnerObj.Add(newSpawner);
        }
    }


    //Set up the List to store all spawners' positions | arg: (List<Vector3> a_List)
    private void SpawnerPosGenerator(List<Vector3> a_List)
    {
        if (spawnerCount % 2 == 0) //if spawnerCount is an EVEN number
        {
            if (spawnerCount < 2) //if we only have less than 2 spawners in the list
            {
                //directly put the spawner in the middle of the set
                //in this case, the default number is 0 and [0 / 0 = 0] so will still give 1 spawner to the designer 
                a_List.Add(Vector3.zero); 
            }
            else //if we have more than 2 spawners
            {
                //get the gap length for each item
                float gapLength = transform.localScale.x / spawnerCount;
                // we need to know where our first gap is as a start point
                Vector3 currGapPos = Vector3.zero;

                //Add left pos + half length of the gap to the list
                //with even number of spawner, we need to place it in the mid of the grid -> [ x ][ x ]
                //Therefore, the first position has to be half length
                a_List.Add(leftPos + gameObject.transform.right * (gapLength / 2));
                //store the current gap position
                currGapPos = leftPos + gameObject.transform.right * (gapLength / 2);

                //for loop and plant the rest of the positions by full length
                //Don't forget to start with 1 because we have planted the first position above
                for (int i = 1; i < spawnerCount; i++)
                {
                    a_List.Add(currGapPos + gameObject.transform.right * gapLength * i);
                }
            }
        }
        else //if spawnerCount is an ODD number
        {
            if (spawnerCount <= 1) //if we only have 1 or none spawner in the list
            {
                a_List.Add(Vector3.zero); //directly put the spawner in the middle of the set
            }
            else //if we have more than one spawner in th e list
            {
                //First stuff a zero in the list
                a_List.Add(Vector3.zero);

                //Set up a factor for the vec to the right/left to add/subtract
                //In this case, factor should be the distance from the centre to right divided by (spawnerCount / 2)
                float factor = disToRight / (spawnerCount / 2);

        
                //Insert the Vec3 positions into the list, start from index [1] because index [0] has been taken by zero
                for (int i = 0; i <= spawnerCount / 2; i++)
                {
                    //add the rightmost vec3 than use that vec3 subtracts factor by i (how many times) to find all the axis on the right side
                    //then stuff them into the list
                    a_List.Add( rightPos - (i * gameObject.transform.right * factor) );
                    //same logic to the left side, but add the vec3 because the left side is negative number
                    a_List.Add( leftPos + (i * gameObject.transform.right * factor) );
                }
            }
        }
    }
}

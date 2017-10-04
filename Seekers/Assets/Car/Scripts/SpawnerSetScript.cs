using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerSetScript : MonoBehaviour
{
    public uint spawnerCount;
    private List<Vector3> spawnerPos;
    private BoxCollider boxCollider;
    private Vector3 rightPos;
    private Vector3 leftPos;
    private float disToRight;

	// Use this for initialization
	void Awake ()
    {
        boxCollider = gameObject.transform.GetComponent<BoxCollider>();
        spawnerPos = new List<Vector3>();
        disToRight = boxCollider.size.x / 2;
        rightPos = gameObject.transform.right * disToRight;
        leftPos = gameObject.transform.right * -disToRight;
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void SpawnerPosGenerator(List<Vector3> a_List)
    {
        if (spawnerCount % 2 == 0) //if spawnerCount is an EVEN number
        {
            float gapLength = boxCollider.size.x / spawnerCount;
            Vector3 currGapPos = Vector3.zero;

            a_List.Add( leftPos + gameObject.transform.right * (gapLength / 2) );
            currGapPos = leftPos + gameObject.transform.right * (gapLength / 2);

            for (int i = 0; i < spawnerCount -1; i++)
            {
                a_List.Add(currGapPos + gameObject.transform.right * gapLength * i);
            }

        }
        else //if spawnerCount is an ODD number
        {
            if (spawnerCount <= 1) //if we only have 1 or none spawner in the Array
            {
                a_List.Add(Vector3.zero); //directly put the spawner in the middle of the set
            }
            else //if we have more than one spawner in th e Array
            {
                //First stuff a zero in the Array
                a_List.Add(Vector3.zero);

                //Set up a factor for the vec to the right/left to add/subtract
                //In this case, factor should be the distance from the centre to right divided by (spawnerCount / 2)
                float factor = disToRight / (spawnerCount / 2);

                //Insert the Vec3 positions into the Array, start from index [1] because index [0] has been taken by zero
                for (int i = 0; i == spawnerCount / 2; i++)
                {
                    //add the rightmost vec3 than use that vec3 subtracts factor by i (how many times) to find all the axis on the right side
                    //then stuff them into the array
                    a_List.Add( rightPos - (i * gameObject.transform.right * factor) );
                    //same logic to the left side, but add the vec3 because the left side is negative number
                    a_List.Add( leftPos + (i * gameObject.transform.right * factor) );
                }
            }
        }

    }
}

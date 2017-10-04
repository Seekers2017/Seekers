using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerSetScript : MonoBehaviour
{
    public uint spawnerCount;
    private Vector3[] spawnerPos;
    private Transform spawnerSetPos;
    private BoxCollider boxCollider;
    private Vector3 rightPos;
    private Vector3 leftPos;
    private float disToRight;

	// Use this for initialization
	void Awake ()
    {
        boxCollider = gameObject.transform.GetComponent<BoxCollider>();
        spawnerPos = new Vector3[spawnerCount];
        disToRight = boxCollider.size.x / 2;
        rightPos = gameObject.transform.right * disToRight;
        leftPos = gameObject.transform.right * -disToRight;
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void SpawnerPosGenerator(Vector3[] a_array)
    {
        if (spawnerCount % 2 == 0) //if spawnerCount is an EVEN number
        {
            //for (int i = 0; i <= spawnerCount / 2; i++)
            //{
            //    a_array[i] = rightPos / i;
            //}
        }
        else //if spawnerCount is an ODD number
        {
            if (spawnerCount <= 1) //if we only have 1 or none spawner in the Array
            {
                a_array[0] = Vector3.zero; //directly put the spawner in the middle of the set
            }
            else //if we have more than one spawner in th e Array
            {
                //First stuff a zero in the Array
                a_array[0] = Vector3.zero;

                //Set up a factor for the vec to the right/left to add/subtract
                //In this case, factor should be the distance from the centre to right divided by spawnerCount
                float factor = disToRight / spawnerCount;

                //Insert the Vec3 positions into the Array, start from index [1] because index [0] has been taken by zero
                for (int i = 0; i == spawnerCount / 2; i++)
                {
                    //
                    a_array[i+1] = rightPos - (i * gameObject.transform.right * factor);
                    a_array[i + 1 + (spawnerCount / 2)] = leftPos + (i * gameObject.transform.right * factor);
                }
            }
        }

    }
}

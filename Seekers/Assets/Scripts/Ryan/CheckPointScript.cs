using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private Transform playerTransform;
    private CarCheckpointScript carCheckpointScript;
    private GameObject checkpointSet;
    private bool isTraversed;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        carCheckpointScript = playerTransform.GetComponent<CarCheckpointScript>();
        checkpointSet = GameObject.FindGameObjectWithTag("CheckpointSet");
        isTraversed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (transform == carCheckpointScript.checkpointList[carCheckpointScript.currCheckpointCount])
        {
            if (carCheckpointScript.currCheckpointCount + 1 < checkpointSet.transform.childCount)
            {
                if (carCheckpointScript.currCheckpointCount == 0)
                {
                    carCheckpointScript.currLap++;
                }

                carCheckpointScript.currCheckpointCount++;
                isTraversed = true;
            }
        }
        else
        {
            carCheckpointScript.currCheckpointCount = 0;
            IsTraversedRefresher();
        }
    }

    void IsTraversedRefresher()
    {
        for (int i = 0; i < checkpointSet.transform.childCount; i++)
        {
            checkpointSet.transform.GetChild(i).GetComponent<CheckpointScript>().isTraversed = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBlocks : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("FallingBlocks");

            for (int i = 0; i < objects.Length; i++)
            {
                Destroy(objects[i]);
            }

            //GameObject.Destroy(GameObject.FindGameObjectsWithTag("FallingRocks"));
        }
    }
}

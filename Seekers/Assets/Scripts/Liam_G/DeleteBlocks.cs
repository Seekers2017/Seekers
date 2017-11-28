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
        //cheacks if an object taged with player or an objecct taged with player2 hits
        //if so the blocks are deleted
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            //finds all the objects taged with "FallingBlocks"
            GameObject[] objects = GameObject.FindGameObjectsWithTag("FallingBlocks");

            //loops though all the existing blocks and destroys them all
            for (int i = 0; i < objects.Length; i++)
            {
                Destroy(objects[i]);
            }
        }
    }
}

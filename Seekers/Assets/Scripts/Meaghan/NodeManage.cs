using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManage : MonoBehaviour {

    //Variables
    public List<Node> nodeList = new List<Node>();

	// Use this for initialization
	void Start ()
    {
        //Use FindObjectsWithTag node to populate the nodeList
        //Get a GameObject array of all nodes

        //loop over that array
        for(int i = 0; i < transform.childCount; i++)
        {
            //Add each Node component on each GameObject to the nodeList
            nodeList.Add( transform.GetChild(i).GetComponent<Node>() );
        }

        //Loop over nodeList (be careful of edges, i-1 will give error if you start at zero)
        for (int c = 0; c < nodeList.Count; c++)
        {
            //if we're not looking at the end node
            if (c != nodeList.Count - 1)
            {
                //Set nodeList[i].next to nodeList[i+1]
                nodeList[c].next = nodeList[c + 1];
            }

            //If we aren't looking at the start node
            if (c != 0)
            {
                //Set nodeList[i].prev to nodeList[i-1]
                nodeList[c].prev = nodeList[c - 1];
            }

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}


}

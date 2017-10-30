using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Selected!");
        }
	}
}


//create a simple finite state machine that runs each of your game states (put it on your GameManager).
//to test, based on key press, swap between each state.


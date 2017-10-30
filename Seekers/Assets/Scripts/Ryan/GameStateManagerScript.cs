using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManagerScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("1"))
        {

        }
	}
}

public class MainMenuState : FSMState
{
    private void ShowLog()
    {
        Debug.Log("This is Main Menu");
    }
}

public class TutoriulState : FSMState
{
    private void ShowLog()
    {
        Debug.Log("This is Tutoriul");
    }
}

public class InGameState : FSMState
{
    private void ShowLog()
    {
        Debug.Log("This is Main Game");
    }
}

public class PauseState : FSMState
{
    private void ShowLog()
    {
        Debug.Log("This is Pause Menu");
    }
}

public class GameOverState : FSMState
{
    private void ShowLog()
    {
        Debug.Log("This is Pause Menu");
    }
}
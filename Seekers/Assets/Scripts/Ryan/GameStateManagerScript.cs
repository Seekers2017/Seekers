using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManagerScript : MonoBehaviour
{
    private BaseState currGameState;
    private List<BaseState> gameStates;

	// Use this for initialization
	void Start ()
    {
        //create all states 
        gameStates = new List<BaseState>();
        gameStates.Add( new MainMenuState() );
        gameStates.Add( new TutoriulState() );
        gameStates.Add( new InGameState() );
        gameStates.Add( new PauseState() );
        gameStates.Add( new GameOverState() );

        SwitchGameState(GameStateID.MainMenu);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currGameState != null)
        {
            currGameState.Update();
        }

        if (Input.GetKeyDown("0"))
        {
            SwitchGameState(GameStateID.MainMenu);
            currGameState.Update();
        }

        if (Input.GetKeyDown("1"))
        {
            SwitchGameState(GameStateID.Tutoriul);
            currGameState.Update();
        }

        if (Input.GetKeyDown("2"))
        {
            SwitchGameState(GameStateID.InGame);
            currGameState.Update();
        }

        if (Input.GetKeyDown("3"))
        {
            SwitchGameState(GameStateID.Pause);
            currGameState.Update();
        }

        if (Input.GetKeyDown("4"))
        {
            SwitchGameState(GameStateID.GameOver);
            currGameState.Update();
        }
    }

    private void SwitchGameState(GameStateID StateID)
    {
        if (currGameState != null)
        {

            currGameState.Shutdown();

            //shut down current state
        }

        for (int i = 0; i < gameStates.Count; i++)
        {
            if (StateID == gameStates[i].StateID)
            {
                currGameState = gameStates[i];
                break;
            }
        }

        currGameState.Start();
    }
}

public class MainMenuState : BaseState
{
    private void ShowLog()
    {
        Debug.Log("This is Main Menu");
    }

    public override void Update()
    {
        ShowLog();
    }
}

public class TutoriulState : BaseState
{
    private void ShowLog()
    {
        Debug.Log("This is Tutoriul");
    }
}

public class InGameState : BaseState
{
    private void ShowLog()
    {
        Debug.Log("This is Main Game");
    }
}

public class PauseState : BaseState
{
    private void ShowLog()
    {
        Debug.Log("This is Pause Menu");
    }

    public override void Start()
    {
        //pause game here
        //display the pause ui canvas
        Time.timeScale = 0.0f;
    }

    public override void Update()
    {
        //write code in here that needs to run ONLY while the game is paused.
    }

    public override void Shutdown()
    {
        //hide the pause ui canvas
        //reset time scale back to regular 100% real time.
        Time.timeScale = 1.0f;
    }
}

public class GameOverState : BaseState
{
    private void ShowLog()
    {
        Debug.Log("This is Pause Menu");
    }

    public override void Start()
    {
        //load the game over scene
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }
}
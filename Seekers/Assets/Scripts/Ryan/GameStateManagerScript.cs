using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManagerScript : MonoBehaviour
{
    private BaseState currGameState;
    private List<BaseState> gameStates;

    public GameStateID currState
    {
        get { return currGameState.StateID; }
    }

	// Use this for initialization
	void Awake ()
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

        if (Input.GetKeyDown(KeyCode.F1))
        {
            SwitchGameState(GameStateID.MainMenu);
            currGameState.Update();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SwitchGameState(GameStateID.Tutoriul);
            currGameState.Update();
        }

		if (Input.GetKeyDown(KeyCode.F3))
        {
            SwitchGameState(GameStateID.InGame);
            currGameState.Update();
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            SwitchGameState(GameStateID.Pause);
            currGameState.Update();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            SwitchGameState(GameStateID.GameOver);
            currGameState.Update();
        }
    }

    public void SwitchGameState(GameStateID StateID)
    {
        if (currGameState != null)
        {
            currGameState.Shutdown();   
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
    MainMenuScript mainMenuScript;

    public MainMenuState()
    {
        stateID = GameStateID.MainMenu;
    }

    public override void Start()
    {
        Time.timeScale = 0.0f;
        mainMenuScript = GameObject.Find("MainMenuUI").GetComponent<MainMenuScript>();
        mainMenuScript.gameObject.SetActive(true);
    }

    public override void Update()
    {
        ShowLog();
    }

    public override void Shutdown()
    {
        mainMenuScript.gameObject.SetActive(false);
    }

    private void ShowLog()
    {
        Debug.Log("This is Main Menu");
    }
}

public class TutoriulState : BaseState
{
    public TutoriulState()
    {
        stateID = GameStateID.Tutoriul;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        ShowLog();
    }

    private void ShowLog()
    {
        Debug.Log("This is Tutoriul");
    }
}

public class InGameState : BaseState
{
    IngameUIScript ingameUIScript;

    public InGameState()
    {
        stateID = GameStateID.InGame;
    }

    public override void Start()
    {
        Time.timeScale = 1.0f;
        ingameUIScript = GameObject.Find("IngameUI").GetComponent<IngameUIScript>();
        ingameUIScript.gameObject.SetActive(true);
    }

    public override void Update()
    {
        ShowLog();
    }

    public override void Shutdown()
    {
        ingameUIScript.gameObject.SetActive(false);
    }

        private void ShowLog()
    {
        Debug.Log("This is Main Game");
    }
}

public class PauseState : BaseState
{
    PauseMenuScript pauseScript;

    public PauseState()
    {
        stateID = GameStateID.Pause;
    }

    public override void Start()
    {
        Time.timeScale = 0.0f;
        pauseScript = GameObject.Find("PauseUI").GetComponent<PauseMenuScript>();
        pauseScript.gameObject.SetActive(true);
    }

    public override void Update()
    {
        ShowLog();
    }

    public override void Shutdown()
    {
        pauseScript.gameObject.SetActive(false);
    }

    private void ShowLog()
    {
        Debug.Log("This is Pause Menu");
    }
}

public class GameOverState : BaseState
{
    public GameOverState()
    {
        stateID = GameStateID.GameOver;
    }

    public override void Start()
    {
        //load the game over scene
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }

    public override void Update()
    {
        //write code in here that needs to run ONLY while the game is paused.
        ShowLog();
    }

    private void ShowLog()
    {
        Debug.Log("This is Pause Menu");
    }
}
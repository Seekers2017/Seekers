using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManagerScript : MonoBehaviour
{
    private BaseState currGameState;
    private List<BaseState> gameStates;

    private GameObject mainMenuUI;
    private GameObject tutorialUI;
    private GameObject ingameUI;
    private GameObject pauseUI;
    private GameObject winUI;

    public List<GameObject> uiObjList;

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
        gameStates.Add(new VictoryState());

        uiObjList = new List<GameObject>();

        mainMenuUI = GameObject.Find("MainMenuUI");
        tutorialUI = GameObject.Find("TutoriulUI");
        ingameUI = GameObject.Find("IngameUI");
        pauseUI = GameObject.Find("PauseUI");
        winUI = GameObject.Find("VictoryUI");

        uiObjList.Add(mainMenuUI);
        uiObjList.Add(tutorialUI);
        uiObjList.Add(ingameUI);
        uiObjList.Add(pauseUI);
        uiObjList.Add(winUI);

        foreach (GameObject gameObject in uiObjList)
        {
            gameObject.SetActive(false);
        }

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

    ///////////////////////////////
    ///ADDING UI OBJECTS TO LIST///
    ///////////////////////////////
    //private void AddUIObjs()
    //{
    //    GameObject[] uiTaggedObj = GameObject.FindGameObjectsWithTag("UI");

    //    //Squeeze them in to rankList
    //    foreach (GameObject gameObject in uiTaggedObj)
    //    {
    //        uiObjList.Add(gameObject);
    //    }

    //    foreach (GameObject gameObject in uiObjList)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}

    ///////////////////////////////
    ///FINDING INVISIBLE OBJECTS///
    ///////////////////////////////
    //public List<GameObject> FindInactiveGameObjects()
    //{
    //    GameObject[] all = GameObject.FindObjectsOfType<GameObject>();//Get all of them in the scene
    //    List<GameObject> objs = new List<GameObject>();
    //    foreach (GameObject obj in all) //Create a list 
    //    {
    //        objs.Add(obj);
    //    }
    //    Predicate<GameObject> inactiveFinder = new Predicate<GameObject>((GameObject go) => { return !go.activeInHierarchy; });//Create the Finder
    //    List<GameObject> results = objs.FindAll(inactiveFinder);//And find inactive ones
    //    return results;
    //}
}

public class MainMenuState : BaseState
{
    GameStateManagerScript gm;


    public MainMenuState()
    {
        stateID = GameStateID.MainMenu;
    }

    public override void Start()
    {
        Time.timeScale = 0.0f;
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        gm.uiObjList[0].SetActive(true);
    }

    public override void Update()
    {
        ShowLog();
    }

    public override void Shutdown()
    {
        gm.uiObjList[0].SetActive(false);
    }

    private void ShowLog()
    {
        Debug.Log("This is Main Menu");
    }
}

public class TutoriulState : BaseState
{
    GameStateManagerScript gm;

    public TutoriulState()
    {
        stateID = GameStateID.Tutoriul;
    }

    public override void Start()
    {
        Time.timeScale = 0.0f;
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        gm.uiObjList[1].SetActive(true);
    }

    public override void Update()
    {
        ShowLog();
    }

    public override void Shutdown()
    {
        gm.uiObjList[1].SetActive(false);
    }

    private void ShowLog()
    {
        Debug.Log("This is Tutoriul");
    }
}

public class InGameState : BaseState
{
    GameStateManagerScript gm;

    public InGameState()
    {
        stateID = GameStateID.InGame;
    }

    public override void Start()
    {
        Time.timeScale = 1.0f;
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        gm.uiObjList[2].SetActive(true);
    }

    public override void Update()
    {
        ShowLog();
    }

    public override void Shutdown()
    {
        gm.uiObjList[2].SetActive(false);
    }

    private void ShowLog()
    {
        Debug.Log("This is Main Game");
    }
}

public class PauseState : BaseState
{
    GameStateManagerScript gm;

    public PauseState()
    {
        stateID = GameStateID.Pause;
    }

    public override void Start()
    {
        Time.timeScale = 0.0f;
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        gm.uiObjList[3].SetActive(true);

    }

    public override void Update()
    {
        ShowLog();
    }

    public override void Shutdown()
    {
        gm.uiObjList[3].SetActive(false);
    }

    private void ShowLog()
    {
        Debug.Log("This is Pause Menu");
    }
}

public class VictoryState : BaseState
{
    GameStateManagerScript gm;

    public VictoryState()
    {
        stateID = GameStateID.Victory;
    }

    public override void Start()
    {
        Time.timeScale = 0.0f;
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        gm.uiObjList[4].SetActive(true);

    }

    public override void Update()
    {
        ShowLog();
    }

    public override void Shutdown()
    {
        gm.uiObjList[4].SetActive(false);
    }

    private void ShowLog()
    {
        Debug.Log("The race is over.");
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
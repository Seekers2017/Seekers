using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//////////////////////////////////////////////////////////////////////////////////////////////
// Note: For changing game states, in this project, it's only turning on/off the UI object. //
//       Game Manager will load the whole scene in the first place due to the scale of the  //
//       project is not too big.                                                            //
//////////////////////////////////////////////////////////////////////////////////////////////

public class GameStateManagerScript : MonoBehaviour
{
    //inherit from base class of the FSMSystem to store current game state
    private BaseState currGameState;

    //list to store all game state
    private List<BaseState> gameStates;

    //get all UI objects
    private GameObject mainMenuUI;
    private GameObject tutorialUI;
    private GameObject ingameUI;
    private GameObject ingameMultiUI;
    private GameObject pauseUI;
    private GameObject winUI;
    private GameObject winUIP2;

    //list to store all UI objects
    public List<GameObject> uiObjList;

    //getter for other classes to get current game state
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
        gameStates.Add(new InGameMultiState());
        gameStates.Add( new PauseState() );
        gameStates.Add(new VictoryState());
        gameStates.Add(new VictoryStateP2());

        //create the list that stores all ui obects
        uiObjList = new List<GameObject>();

        //find all UI objects
        mainMenuUI = GameObject.Find("MainMenuUI");
        tutorialUI = GameObject.Find("TutoriulUI");
        ingameUI = GameObject.Find("IngameUI");
        ingameMultiUI = GameObject.Find("IngameMuiltUI");
        pauseUI = GameObject.Find("PauseUI");
        winUI = GameObject.Find("VictoryUI");
        winUIP2 = GameObject.Find("VictoryUIP2");

        //add all UI objects into uiObjList
        uiObjList.Add(mainMenuUI);
        uiObjList.Add(tutorialUI);
        uiObjList.Add(ingameUI);
        uiObjList.Add(ingameMultiUI);
        uiObjList.Add(pauseUI);
        uiObjList.Add(winUI);
        uiObjList.Add(winUIP2);

        //set all UI object in the list to inactive right after we add them into the list
        foreach (GameObject gameObject in uiObjList)
        {
            gameObject.SetActive(false);
        }

        //initialize the current game state to start menu
        //so each time we start the game will only start at Main Menu
        SwitchGameState(GameStateID.MainMenu);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if current game state is not null
        if (currGameState != null)
        {
            //run current game state's update
            currGameState.Update();
        }

        ///////////////////////////////////////////////////////////////////////////////
        //This section is for setting up the shortcut keys for developers            //
        //so the developer can enter each state easily without travers the gameloop  //
                                                                                     //
        if (Input.GetKeyDown(KeyCode.F1)) //Press F1 to enter Main Menu              //
        {                                                                            //
            SwitchGameState(GameStateID.MainMenu);                                   //
            currGameState.Update();                                                  //
        }                                                                            //
                                                                                     //
        if (Input.GetKeyDown(KeyCode.F2)) //Press F2 to enter Tutoriul               //
        {                                                                            //
            SwitchGameState(GameStateID.Tutoriul);                                   //
            currGameState.Update();                                                  //
        }                                                                            //
                                                                                     //
		if (Input.GetKeyDown(KeyCode.F3)) //Press F3 to enter Singel play mode       //
        {                                                                            //
            SwitchGameState(GameStateID.InGame);                                     //
            currGameState.Update();                                                  //
        }                                                                            //
                                                                                     //
        if (Input.GetKeyDown(KeyCode.F4)) //Press F4 to enter Multi Play Mode        //
        {                                                                            //
            SwitchGameState(GameStateID.InGameMuilti);                               //
            currGameState.Update();                                                  //
        }                                                                            //
                                                                                     //
        if (Input.GetKeyDown(KeyCode.F5)) //Press F5 to enter Pause Menu             //
        {                                                                            //
            SwitchGameState(GameStateID.Pause);                                      //
            currGameState.Update();                                                  //
        }                                                                            //
                                                                                     //
        if (Input.GetKeyDown(KeyCode.F6)) //Press F6 to enter Victory Screen         //
        {                                                                            //
            SwitchGameState(GameStateID.Victory);                                    //
            currGameState.Update();                                                  //
        }                                                                            //
                                                                                     //
        ///////////////////////////////////////////////////////////////////////////////
    }

    //function that can switch game state
    public void SwitchGameState(GameStateID StateID)
    {
        //shut down current state before activate the other one
        if (currGameState != null)
        {
            currGameState.Shutdown();   
        }

        //find each state in the list to see if it matches the one player entered
        for (int i = 0; i < gameStates.Count; i++)
        {
            //if matches, switch current to that state and break the for loop
            if (StateID == gameStates[i].StateID) 
            {
                currGameState = gameStates[i];
                break;
            }
        }

        //Start current state
        currGameState.Start();
    }
}

/////////////////////////////////////////////////////////////////////
//Section below is the Game State classes                          //
//We are basically build a layer above Unity's core to control FSM //
//Therefore we need a constructor                                  //
/////////////////////////////////////////////////////////////////////

//Main Manu State class : inherit from FSM base class 
public class MainMenuState : BaseState
{
    //get game manager
    GameStateManagerScript gm;

    //constructor of the class (The reaseon we need a constructor, read head comment)
    public MainMenuState()
    {
        stateID = GameStateID.MainMenu;
    }

    //Start Game State by overriding Base State
    public override void Start()
    {
        //set world time scale to 0
        Time.timeScale = 0.0f;
        //find Game Manager
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        //set the index[0] object (Main Menu) in UI list to active
        gm.uiObjList[0].SetActive(true);
    }

    public override void Update()
    {
        //Display current sate in console
        ShowLog();
    }

    public override void Shutdown()
    {
        //set current inactive
        gm.uiObjList[0].SetActive(false);
    }

    //Display current sate in console
    private void ShowLog()
    {
        Debug.Log("This is Main Menu");
    }
}

//Tutoriul State class : inherit from FSM base class 
public class TutoriulState : BaseState
{
    //get game manager
    GameStateManagerScript gm;

    //constructor of the class (The reaseon we need a constructor, read head comment)
    public TutoriulState()
    {
        stateID = GameStateID.Tutoriul;
    }

    //Start Game State by overriding Base State
    public override void Start()
    {
        //set world time scale to 0
        Time.timeScale = 0.0f;
        //find Game Manager
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        //set the index[1] object (Tutoriul) in UI list to active
        gm.uiObjList[1].SetActive(true);
    }

    public override void Update()
    {
        //Display current sate in console
        ShowLog();
    }

    public override void Shutdown()
    {
        //set current inactive when shutdown
        gm.uiObjList[1].SetActive(false);
    }

    //Display current sate in console
    private void ShowLog()
    {
        Debug.Log("This is Tutoriul");
    }
}

//Ingame State (single player) class : inherit from FSM base class 
public class InGameState : BaseState
{
    //get game manager
    GameStateManagerScript gm;

    //constructor of the class (The reaseon we need a constructor, read head comment)
    public InGameState()
    {
        stateID = GameStateID.InGame;
    }

    //Start Game State by overriding Base State
    public override void Start()
    {
        //set world time scale to 1 so the player can start to play
        Time.timeScale = 1.0f;
        //find Game Manager
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        //set the index[2] object (Ingame single play) in UI list to active
        gm.uiObjList[2].SetActive(true);
    }

    public override void Update()
    {
        //Display current sate in console
        ShowLog();
    }

    public override void Shutdown()
    {
        //set current inactive when shutdown
        gm.uiObjList[2].SetActive(false);
    }

    //Display current sate in console
    private void ShowLog()
    {
        Debug.Log("This is Main Game");
    }
}

//Ingame State (multi player) class : inherit from FSM base class 
public class InGameMultiState : BaseState
{
    //get game manager
    GameStateManagerScript gm;

    //constructor of the class (The reaseon we need a constructor, read head comment)
    public InGameMultiState()
    {
        stateID = GameStateID.InGameMuilti;
    }

    //Start Game State by overriding Base State
    public override void Start()
    {
        //set world time scale to 1 so the player can start to play
        Time.timeScale = 1.0f;
        //find Game Manager
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        //set the index[3] object (Ingame multi play) in UI list to active
        gm.uiObjList[3].SetActive(true);
    }

    public override void Update()
    {
        //Display current sate in console
        ShowLog();
    }

    public override void Shutdown()
    {
        //set current inactive when shutdown
        gm.uiObjList[3].SetActive(false);
    }

    //Display current sate in console
    private void ShowLog()
    {
        Debug.Log("This is Main Game");
    }
}

//Pause State class : inherit from FSM base class 
public class PauseState : BaseState
{
    //get game manager
    GameStateManagerScript gm;

    //constructor of the class (The reaseon we need a constructor, read head comment)
    public PauseState()
    {
        stateID = GameStateID.Pause;
    }

    //Start Game State by overriding Base State
    public override void Start()
    {
        //set world time scale to 0
        Time.timeScale = 0.0f;
        //get game manager
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        //set the index[4] object (Pause) in UI list to active
        gm.uiObjList[4].SetActive(true);

    }

    public override void Update()
    {
        //Display current sate in console
        ShowLog();
    }

    public override void Shutdown()
    {
        //set current inactive when shutdown
        gm.uiObjList[4].SetActive(false);
    }

    //Display current sate in console
    private void ShowLog()
    {
        Debug.Log("This is Pause Menu");
    }
}

//Victory State class : inherit from FSM base class 
public class VictoryState : BaseState
{
    //get game manager
    GameStateManagerScript gm;

    //constructor of the class (The reaseon we need a constructor, read head comment)
    public VictoryState()
    {
        stateID = GameStateID.Victory;
    }

    //Start Game State by overriding Base State
    public override void Start()
    {
        //set world time scale to 0
        Time.timeScale = 0.0f;
        //find Game Manager
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        //set the index[4] object (Pause) in UI list to active
        gm.uiObjList[5].SetActive(true);

    }

    public override void Update()
    {
        //Display current sate in console
        ShowLog();
    }

    public override void Shutdown()
    {
        //set current inactive when shutdown
        gm.uiObjList[5].SetActive(false);
    }
    //Display current sate in console
    private void ShowLog()
    {
        Debug.Log("The race is over.");
    }
}

public class VictoryStateP2 : BaseState
{
    GameStateManagerScript gm;

    public VictoryStateP2()
    {
        stateID = GameStateID.Victory;
    }

    public override void Start()
    {
        Time.timeScale = 0.0f;
        gm = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        gm.uiObjList[5].SetActive(true);

    }

    public override void Update()
    {
        ShowLog();
    }

    public override void Shutdown()
    {
        gm.uiObjList[6].SetActive(false);
    }

    private void ShowLog()
    {
        Debug.Log("The race is over.");
    }
}
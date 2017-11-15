using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

//Enum class for indicating current option
public enum currOption_MainMenu
{
    START,
    QUIT
}

//Main Menu UI script
public class MainMenuScript : MonoBehaviour
{
    //get game manager
    private GameStateManagerScript gameManager;

    //assign start sprite
    private Image startSprite;
    //assign quit sprite
    private Image quitSprite;

    //Arrays for storing Resources sprites
    private Sprite[] startSpriteList;
    private Sprite[] quitSpriteList;

    //get current pointing
    private currOption_MainMenu currPointing;

    //get Xbox controller
    public XboxController controller;

    // Use this for initialization
    void Start()
    {
        //get game manager
        gameManager = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();

        //Arrays for storing Resources sprites
        startSpriteList = Resources.LoadAll<Sprite>("MainStartSprites");
        quitSpriteList = Resources.LoadAll<Sprite>("MainQuitSprites");

        //assign start sprite
        startSprite = gameObject.transform.Find("start").GetComponent<Image>();
        //assign quit sprite
        quitSprite = gameObject.transform.Find("quit").GetComponent<Image>();

        //initialize which sprite to assign to each option (1 = highlighted sprite, 0 = not highlighted sprite)
        startSprite.sprite = startSpriteList[1];
        quitSprite.sprite = quitSpriteList[0];

        //set current pointing to START
        currPointing = currOption_MainMenu.START;
    }

    // Update is called once per frame
    void Update()
    {
        //if currently pointing to START
        if (currPointing == currOption_MainMenu.START)
        {
            //if press up or down
            if (XCI.GetButtonDown(XboxButton.DPadUp, controller) || XCI.GetButtonDown(XboxButton.DPadDown, controller))
            {
                //change currPointing to QUIT
                currPointing = currOption_MainMenu.QUIT;
                //Unhighlight START sprite
                startSprite.sprite = startSpriteList[0];
                //highlight QUIT sprite
                quitSprite.sprite = quitSpriteList[1];
                //Display current status in console
                Debug.Log("Moving away from Start");
            }

            //if press A, Load the tutorial
            if (XCI.GetButtonDown(XboxButton.A, controller))
            {
                gameManager.SwitchGameState(GameStateID.Tutoriul);
            }
        }

        //else if pointing at QUIT
        else if (currPointing == currOption_MainMenu.QUIT)
        {
            //if press up or down
            if (XCI.GetButtonDown(XboxButton.DPadUp, controller) || XCI.GetButtonDown(XboxButton.DPadDown, controller))
            {
                //change currPointing to START
                currPointing = currOption_MainMenu.START;
                //highlight START sprite
                startSprite.sprite = startSpriteList[1];
                //Unhighlight QUIT sprite
                quitSprite.sprite = quitSpriteList[0];
                //Display current status in console
                Debug.Log("Moving away from Quit");
            }

            //if press A, quit the game
            if (XCI.GetButtonDown(XboxButton.A, controller))
            {
                Application.Quit();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public enum currOption_MainMenu
{
    START,
    QUIT
}

public class MainMenuScript : MonoBehaviour
{
    private GameStateManagerScript gameManager;

    private Image startSprite;
    private Image quitSprite;

    private Sprite[] startSpriteList;
    private Sprite[] quitSpriteList;

    private currOption_MainMenu currPointing;
    private int playCount;

    public XboxController controller;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();

        startSpriteList = Resources.LoadAll<Sprite>("MainStartSprites");
        quitSpriteList = Resources.LoadAll<Sprite>("MainQuitSprites");

        startSprite = gameObject.transform.Find("start").GetComponent<Image>();
        quitSprite = gameObject.transform.Find("quit").GetComponent<Image>();

        startSprite.sprite = startSpriteList[1];
        quitSprite.sprite = quitSpriteList[0];

        currPointing = 0;
        playCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currPointing == currOption_MainMenu.START)
        {
            if (XCI.GetButtonDown(XboxButton.DPadUp, controller) || XCI.GetButtonDown(XboxButton.DPadDown, controller))
            {
                currPointing = currOption_MainMenu.QUIT;
                startSprite.sprite = startSpriteList[0];
                quitSprite.sprite = quitSpriteList[1];
                Debug.Log("Moving away from Start");
            }

            //If this is the first time we have played the game
            if(playCount < 1)
            {
                //Load the tutorial
                if (XCI.GetButtonDown(XboxButton.A))
                {
                    playCount++;
                    gameManager.SwitchGameState(GameStateID.Tutoriul);
                }
            }
            else
            {
                gameManager.SwitchGameState(GameStateID.InGame);
            }
            
        }

        else if (currPointing == currOption_MainMenu.QUIT)
        {
            if (XCI.GetButtonDown(XboxButton.DPadUp, controller) || XCI.GetButtonDown(XboxButton.DPadDown, controller))
            {
                currPointing = currOption_MainMenu.START;
                startSprite.sprite = startSpriteList[1];
                quitSprite.sprite = quitSpriteList[0];
                Debug.Log("Moving away from Quit");
            }

            if (XCI.GetButtonDown(XboxButton.A))
            {
                Application.Quit();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (currPointing == currOption_MainMenu.START)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                currPointing = currOption_MainMenu.QUIT;
                startSprite.sprite = startSpriteList[0];
                quitSprite.sprite = quitSpriteList[1];
                Debug.Log("Moving away from Start");
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                gameManager.SwitchGameState(GameStateID.Tutoriul);
            }
        }

        else if (currPointing == currOption_MainMenu.QUIT)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                currPointing = currOption_MainMenu.START;
                startSprite.sprite = startSpriteList[1];
                quitSprite.sprite = quitSpriteList[0];
                Debug.Log("Moving away from Quit");
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Application.Quit();
            }
        }
    }
}

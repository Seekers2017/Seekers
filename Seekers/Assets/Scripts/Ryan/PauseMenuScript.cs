using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum currOption_Pause
{
    RESUME,
    MAINMENU
}

public class PauseMenuScript : MonoBehaviour
{
    private GameStateManagerScript gameManager;

    private Image resumeSprite;
    private Image mainMenuSprite;

    private Sprite[] resumeSpriteList;
    private Sprite[] mainMenuSpriteList;

    private currOption_Pause currPointing;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();

        resumeSpriteList = Resources.LoadAll<Sprite>("PauseResumeSprites");
        mainMenuSpriteList = Resources.LoadAll<Sprite>("PauseMainMenuSprites");

        resumeSprite = gameObject.transform.Find("resume").GetComponent<Image>();
        mainMenuSprite = gameObject.transform.Find("mainMenu").GetComponent<Image>();

        resumeSprite.sprite = resumeSpriteList[1];
        mainMenuSprite.sprite = mainMenuSpriteList[0];

        currPointing = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0.0f;

        if (currPointing == currOption_Pause.RESUME)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                currPointing = currOption_Pause.MAINMENU;
                resumeSprite.sprite = resumeSpriteList[0];
                mainMenuSprite.sprite = mainMenuSpriteList[1];
                Debug.Log("Moving away from Resume");
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Time.timeScale = 1.0f;
                gameManager.SwitchGameState(GameStateID.InGame);
            }
        }

        else if (currPointing == currOption_Pause.MAINMENU)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                currPointing = currOption_Pause.RESUME;
                resumeSprite.sprite = resumeSpriteList[1];
                mainMenuSprite.sprite = mainMenuSpriteList[0];
                Debug.Log("Moving away from Main Menu");
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                gameManager.SwitchGameState(GameStateID.MainMenu);
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

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

    public XboxController controller;

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

        if (currPointing == currOption_Pause.RESUME)
        {
            if (XCI.GetButtonDown(XboxButton.DPadUp, controller) || XCI.GetButtonDown(XboxButton.DPadDown, controller))
            {
                currPointing = currOption_Pause.MAINMENU;
                resumeSprite.sprite = resumeSpriteList[0];
                mainMenuSprite.sprite = mainMenuSpriteList[1];
                Debug.Log("Moving away from Resume");
            }

            if (XCI.GetButtonDown(XboxButton.A))
            {
                gameManager.SwitchGameState(GameStateID.InGame);
            }
        }

        else if (currPointing == currOption_Pause.MAINMENU)
        {
            if (XCI.GetButtonDown(XboxButton.DPadUp, controller) || XCI.GetButtonDown(XboxButton.DPadDown, controller))
            {
                currPointing = currOption_Pause.RESUME;
                resumeSprite.sprite = resumeSpriteList[1];
                mainMenuSprite.sprite = mainMenuSpriteList[0];
                Debug.Log("Moving away from Main Menu");
            }

            if (XCI.GetButtonDown(XboxButton.A))
            {
                SceneManager.LoadScene("CrusHour");
            }
        }
    }
}


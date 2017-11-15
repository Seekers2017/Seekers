using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

//Enum class for indicating current option
public enum currOption_Pause
{
    RESUME,
    MAINMENU
}

//Pause UI script
public class PauseMenuScript : MonoBehaviour
{
    //get game manager
    private GameStateManagerScript gameManager;

    //assign Resume sprite
    private Image resumeSprite;
    //assign Main Menu sprite
    private Image mainMenuSprite;

    //Arrays for storing Resources sprites
    private Sprite[] resumeSpriteList;
    private Sprite[] mainMenuSpriteList;

    //get current pointing
    private currOption_Pause currPointing;

    //get Xbox controller
    public XboxController controller;

    // Use this for initialization
    void Start()
    {
        //get game manager
        gameManager = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();

        //Arrays for storing Resources sprites
        resumeSpriteList = Resources.LoadAll<Sprite>("PauseResumeSprites");
        mainMenuSpriteList = Resources.LoadAll<Sprite>("PauseMainMenuSprites");

        //assign Resume sprite
        resumeSprite = gameObject.transform.Find("resume").GetComponent<Image>();
        //assign Main Menu sprite
        mainMenuSprite = gameObject.transform.Find("mainMenu").GetComponent<Image>();

        //initialize which sprite to assign to each option (1 = highlighted sprite, 0 = not highlighted sprite)
        resumeSprite.sprite = resumeSpriteList[1];
        mainMenuSprite.sprite = mainMenuSpriteList[0];

        //set current pointing to RESUME
        currPointing = currOption_Pause.RESUME;
    }

    // Update is called once per frame
    void Update()
    {
        //if currently pointing to RESUME
        if (currPointing == currOption_Pause.RESUME)
        {
            //if press up or down
            if (XCI.GetButtonDown(XboxButton.DPadUp, controller) || XCI.GetButtonDown(XboxButton.DPadDown, controller))
            {
                //change currPointing to MAIN MENU
                currPointing = currOption_Pause.MAINMENU;
                //Unhighlight RESUME sprite
                resumeSprite.sprite = resumeSpriteList[0];
                //highlight QUIT MAIN MENU
                mainMenuSprite.sprite = mainMenuSpriteList[1];
                //Display current status in console
                Debug.Log("Moving away from Resume");
            }

            //if press A, resume the game
            if (XCI.GetButtonDown(XboxButton.A))
            {
                gameManager.SwitchGameState(GameStateID.InGameMuilti);
            }
        }

        //if currently pointing to MAIN MENU
        else if (currPointing == currOption_Pause.MAINMENU)
        {
            //if press up or down
            if (XCI.GetButtonDown(XboxButton.DPadUp, controller) || XCI.GetButtonDown(XboxButton.DPadDown, controller))
            {
                //change currPointing to RESUME
                currPointing = currOption_Pause.RESUME;
                //highlight RESUME sprite
                resumeSprite.sprite = resumeSpriteList[1];
                //Unhighlight QUIT MAIN MENU
                mainMenuSprite.sprite = mainMenuSpriteList[0];
                //Display current status in console
                Debug.Log("Moving away from Main Menu");
            }

            //if press A, go back to Main Manu (reload the entire scene)
            if (XCI.GetButtonDown(XboxButton.A))
            {
                SceneManager.LoadScene("CrusHour_Multiplayer");
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class StateVictoryP2 : MonoBehaviour {

    //Variables
    private GameStateManagerScript gameManager;
    private PlayerManager playerManager;
    private WheelDrive wheelDrive;

    [SerializeField]
    private Sprite winSprite;
    [SerializeField]
    private Sprite loseSprite;

    private Image set;

    public XboxController controller;



    // Use this for initialization
    void Awake()
    {
        //Obtain the script properties
        gameManager = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        playerManager = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerManager>();
        wheelDrive = GameObject.FindGameObjectWithTag("Player2").GetComponent<WheelDrive>();
        set = gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        //If you have won
        if (playerManager.Win == true)
        {
            //Set the winning sprite
            set.sprite = winSprite;

            //Stop driving
            wheelDrive.AbilityToDrive = false;
        }
        else
        {
            //Set the losing sprite
            set.sprite = loseSprite;

            //Stop driving
            wheelDrive.AbilityToDrive = false;
        }

        if (XCI.GetButtonDown(XboxButton.A))
        {
            //Quit the game
        }
    }
}

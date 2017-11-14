using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class StateVictory : MonoBehaviour {

    //Variables
    private GameStateManagerScript gameManager;
    private PlayerManager playerManager;

    [SerializeField]
    private Sprite winSprite;
    [SerializeField]
    private Sprite loseSprite;

    private Image set;

    public XboxController controller;

  

    // Use this for initialization
    void Awake ()
    {
        //Obtain the script properties
        gameManager = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        set = gameObject.transform.GetChild(1).GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		//Switch to the main menu

        //If you have won
        if(playerManager.Win == true)
        {
            //Set the winning sprite
            set.sprite = winSprite;
        }
        else
        {
            //Set the losing sprite
            set.sprite = loseSprite;
        }

        if (XCI.GetButtonDown(XboxButton.A))
        {
            //Quit the game
        }


    }
}

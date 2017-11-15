using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class TutorialUIScript : MonoBehaviour
{
    private GameStateManagerScript gameManager;

    public XboxController controller;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
    }


    // Update is called once per frame
    void Update ()
    {
        //press A to skip tutorial and get into multi player mode
        if (XCI.GetButtonDown(XboxButton.A, controller))
        {
            gameManager.SwitchGameState(GameStateID.InGameMuilti);
        }
    }
}

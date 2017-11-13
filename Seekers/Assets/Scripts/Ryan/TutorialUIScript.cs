using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class TutorialUIScript : MonoBehaviour
{

    // Use this for initialization
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
        if (XCI.GetButton(XboxButton.A, controller))
        {
            gameManager.SwitchGameState(GameStateID.InGame);
        }
    }
}

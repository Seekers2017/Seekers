using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIScript : MonoBehaviour
{

    // Use this for initialization
    private GameStateManagerScript gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
    }


    // Update is called once per frame
    void Update ()
    {
        if (Input.anyKeyDown)
        {
            gameManager.SwitchGameState(GameStateID.InGame);
        }
    }
}

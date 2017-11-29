using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryP1 : MonoBehaviour {

    //Variables
    private GameStateManagerScript gameManager;
    private CarCheckpointScript carCheckpoint;
    private RankScript rank;
    private WheelDrive drive;
    private VictoryP2 check;

    private int currRank;
    private int currLap;
    private bool hasFinished;
    private float mainMenuTimer;

    [SerializeField]
    private GameObject displayRank;

    [SerializeField]
    private int maxLaps;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite first;

    [SerializeField]
    private Sprite second;

    [SerializeField]
    private Sprite third;

    [SerializeField]
    private Sprite fourth;

    [SerializeField]
    private Sprite fifth;

    [SerializeField]
    private Sprite sixth;

    // Use this for initialization
    void Awake ()
    {
        //Obtain the script properties
        gameManager = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();
        rank = GameObject.Find("GameManager").GetComponent<RankScript>();
        carCheckpoint = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CarCheckpointScript>();
        drive = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<WheelDrive>();
        spriteRenderer = GameObject.FindGameObjectWithTag("Player").transform.GetChild(16).transform.GetComponent<SpriteRenderer>();
        check = GameObject.FindGameObjectWithTag("Player2").transform.GetComponent<VictoryP2>();

        //Don't render the game object
        displayRank.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Obtain the checkpoint
        currLap = carCheckpoint.currLap;

        if(hasFinished == false)
        {
            //Obtain the rank
            currRank = rank.GetRank(carCheckpoint);
        }
        

        //If we have exceeded the amount of laps
        if(currLap > maxLaps)
        {
            //Stop driving
            drive.AbilityToDrive = false;
            hasFinished = true;

            if(currRank == 1)
            {
                //Set the image of the sprite renderer
                spriteRenderer.sprite = first;

                //Display the rank
                displayRank.SetActive(true);
            }
            else if (currRank == 2)
            {
                //Set the image of the sprite renderer
                spriteRenderer.sprite = second;

                //Display the rank
                displayRank.SetActive(true);
            }
            else if (currRank == 3)
            {
                //Set the image of the sprite renderer
                spriteRenderer.sprite = third;

                //Display the rank
                displayRank.SetActive(true);
            }
            else if (currRank == 4)
            {
                //Set the image of the sprite renderer
                spriteRenderer.sprite = fourth;

                //Display the rank
                displayRank.SetActive(true);
            }
            else if (currRank == 5)
            {
                //Set the image of the sprite renderer
                spriteRenderer.sprite = fifth;

                //Display the rank
                displayRank.SetActive(true);
            }
            else if (currRank == 6)
            {
                //Set the image of the sprite renderer
                spriteRenderer.sprite = sixth;

                //Display the rank
                displayRank.SetActive(true);
            }
        }

        //Check if both players have finished the race
        if(hasFinished == true && check.HasFinished == true)
        {
            //Start the timer
            mainMenuTimer += Time.deltaTime;

            //If the timer exceeds 3 seconds
            if(mainMenuTimer > 3.0f)
            {
                //Switch states
                gameManager.SwitchGameState(GameStateID.MainMenu);
            }

        }
    }
}

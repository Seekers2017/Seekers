using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;

public class IngameUIScript : MonoBehaviour
{
    //get game manager
    private GameStateManagerScript gameManager;

    //get entity
    private PlayerManager entity;
    private RankScript rankScript;

    //create slots for UI sprites
    private Image lapCountSprite;
    private Image rankSprite;
    private Image itemSprite;

    //create arrays for sprites that called from Resource folder
    private Sprite[] lapSpriteList;
    private Sprite[] rankSpriteList;
    private Sprite[] itemSpriteList;

    public XboxController controller;

    //get entity's check point script
    private CarCheckpointScript checkpointScript;

    //For designers
    [SerializeField]
    private int maxLaps = 3;

    //Lap and rank values
    private int currLap;
    private int rank;

    //Obtain the player manager script
    private PlayerManager playerManager;

    //Getters and setters
    public int Rank
    {
        get { return rank; }
    }

    public int CurrLap
    {
        get { return currLap; }
    }

    // Use this for initialization
    void Start ()
    {

        //get game manager
        gameManager = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();

        //Obtain the player manager
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();

        //get entity
        entity = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerManager>();

        //get rankScript
        rankScript = GameObject.Find("GameManager").GetComponent<RankScript>();

        //get check point script from entities
        checkpointScript = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CarCheckpointScript>();

        //load sprites for sprites into arrays we created above from Resource folder
        lapSpriteList = Resources.LoadAll<Sprite>("LapSprites");
        rankSpriteList = Resources.LoadAll<Sprite>("RankSprites");
        itemSpriteList = Resources.LoadAll<Sprite>("ItemIconSprites");

        //get image slots and link to the variables we set up
        lapCountSprite = gameObject.transform.Find("lapCount").GetComponent<Image>();
        rankSprite = gameObject.transform.Find("rank").GetComponent<Image>();
        itemSprite = gameObject.transform.Find("itemIcon").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //get currLap from check point script of entity's
        currLap = checkpointScript.currLap;
        //get rank from the rankScrip's getter GetRank
        rank = rankScript.GetRank(checkpointScript);
        //according to the currlap, decide which sprite to render
        //it has to be (currlap - 1), you know why
        lapCountSprite.sprite = lapSpriteList[currLap-1];

        //according to the rank, decide which sprite to render
        //it has to be (currlap - 1), you know why
        rankSprite.sprite = rankSpriteList[rank - 1];

        //if entity doesn't possess items
        if (entity.HasItem == false)
        {
            //no matter what icon inside it's currently in the frame,
            //make it transparent (alpha = 0)
            itemSprite.color = new Color(1, 1, 1, 0);
            //this method is a faking method, it doesn't actually destroy the icon
            //only hiding it. Will have to improve if possible ( use Destroy(); and Instantiate(); )
        }

        //press start button to pause
        if (XCI.GetButtonDown(XboxButton.Start))
        {
            gameManager.SwitchGameState(GameStateID.Pause);
        }

        //Check if the race is complete 
        if (currLap > maxLaps)
        {
            //If you win
            if (rank == 1)
            {
                //You win the race
                playerManager.Win = true;
                gameManager.SwitchGameState(GameStateID.Victory);
            }
            else
            {
                //You lose the race
                playerManager.Win = false;
                gameManager.SwitchGameState(GameStateID.Victory);
            }
        }
    }

    //Set item sprite display in the frame
    public void SetCollectedItem(ItemNum collectedItemType)
    {
        //if ItemNum = HealthKit
        if (collectedItemType == ItemNum.HeathKit)
        {
            //set sprite alpha to 1
            itemSprite.color = new Color(1, 1, 1, 1);
            //replace the current sprite to the first on of the list
            itemSprite.sprite = itemSpriteList[0];
        }

        if (collectedItemType == ItemNum.Bumper)
        {
            //set sprite alpha to 1
            itemSprite.color = new Color(1, 1, 1, 1);
            //replace the current sprite to the second on of the list
            itemSprite.sprite = itemSpriteList[1];
        }

        if (collectedItemType == ItemNum.SpeedBoost)
        {
            //set sprite alpha to 1
            itemSprite.color = new Color(1, 1, 1, 1);
            //replace the current sprite to the third on of the list
            itemSprite.sprite = itemSpriteList[2];
        }
    }
}

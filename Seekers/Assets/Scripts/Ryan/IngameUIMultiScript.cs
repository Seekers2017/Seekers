using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class IngameUIMultiScript : MonoBehaviour
{
    //get game manager
    private GameStateManagerScript gameManager;

    //get entity
    private PlayerManager player1;
    private PlayerManager player2;
    private RankScript rankScript;

    //create slots for UI sprites
    private Image lapCountP1Sprite;
    private Image rankP1Sprite;
    private Image itemP1Sprite;

    private Image lapCountP2Sprite;
    private Image rankP2Sprite;
    private Image itemP2Sprite;

    //create arrays for sprites that called from Resource folder
    private Sprite[] lapSpriteList;
    private Sprite[] rankSpriteList;
    private Sprite[] itemSpriteList;

    //get entity's check point script
    private CarCheckpointScript checkpointP1Script;
    private CarCheckpointScript checkpointP2Script;

    //For designers
    [SerializeField]
    private int maxLaps = 3;

    //Rank and lap values
    private int currLapP1;
    private int currLapP2;
    private int rankP1;
    private int rankP2;

    //Obtain the player manager script
    private PlayerManager playerManager;
    private PlayerManager playerManagerP2;

    public XboxController controller;

    // Use this for initialization
    void Start()
    {
        //get game manager
        gameManager = GameObject.Find("GameManager").GetComponent<GameStateManagerScript>();

        //Obtain the players
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        playerManagerP2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerManager>();

        //get entity
        player1 = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerManager>(); 
        player2 = GameObject.FindGameObjectWithTag("Player2").transform.GetComponent<PlayerManager>();

        //get rankScript
        rankScript = GameObject.Find("GameManager").GetComponent<RankScript>();

        //get check point script from entities
        checkpointP1Script = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CarCheckpointScript>();
        checkpointP2Script = GameObject.FindGameObjectWithTag("Player2").transform.GetComponent<CarCheckpointScript>();

        //load sprites for sprites into arrays we created above from Resource folder
        lapSpriteList = Resources.LoadAll<Sprite>("LapSprites");
        rankSpriteList = Resources.LoadAll<Sprite>("RankSprites");
        itemSpriteList = Resources.LoadAll<Sprite>("ItemIconSprites");

        //get image slots and link to the variables we set up
        lapCountP1Sprite = gameObject.transform.Find("lapCountP1").GetComponent<Image>();
        rankP1Sprite = gameObject.transform.Find("rankP1").GetComponent<Image>();
        itemP1Sprite = gameObject.transform.Find("itemIconP1").GetComponent<Image>();

        lapCountP2Sprite = gameObject.transform.Find("lapCountP2").GetComponent<Image>();
        rankP2Sprite = gameObject.transform.Find("rankP2").GetComponent<Image>();
        itemP2Sprite = gameObject.transform.Find("itemIconP2").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //get currLap from check point script of entity's
        currLapP1 = checkpointP1Script.currLap;
        currLapP2 = checkpointP2Script.currLap;

        //get rank from the rankScrip's getter GetRank
        rankP1 = rankScript.GetRank(checkpointP1Script);
        rankP2 = rankScript.GetRank(checkpointP2Script);

        //according to the currlap, decide which sprite to render
        //it has to be (currlap - 1), you know why
        lapCountP1Sprite.sprite = lapSpriteList[currLapP1 - 1];
        lapCountP2Sprite.sprite = lapSpriteList[currLapP2 - 1];

        //according to the rank, decide which sprite to render
        //it has to be (currlap - 1), you know why
        rankP1Sprite.sprite = rankSpriteList[rankP1 - 1];
        rankP2Sprite.sprite = rankSpriteList[rankP2 - 1];

        //if player1 doesn't possess items
        if (player1.HasItem == false)
        {
            //no matter what icon inside it's currently in the frame,
            //make it transparent (alpha = 0)
            itemP1Sprite.color = new Color(1, 1, 1, 0);
            //this method is a faking method, it doesn't actually destroy the icon
            //only hiding it. Will have to improve if possible ( use Destroy(); and Instantiate(); )
        }

        //if player1 doesn't possess items
        if (player2.HasItem == false)
        {
            //no matter what icon inside it's currently in the frame,
            //make it transparent (alpha = 0)
            itemP2Sprite.color = new Color(1, 1, 1, 0);
            //this method is a faking method, it doesn't actually destroy the icon
            //only hiding it. Will have to improve if possible ( use Destroy(); and Instantiate(); )
        }

        //Check if the race is complete 
        if (currLapP1 > maxLaps)
        {
            //If you win
            if (rankP1 == 1)
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

        //Check if the race is complete 
        if (currLapP2 > maxLaps)
        {
            //If you win
            if (rankP2 == 1)
            {
                //You win the race
                playerManagerP2.Win = true;
                gameManager.SwitchGameState(GameStateID.VictoryP2);
            }
            else
            {
                //You lose the race
                playerManagerP2.Win = false;
                gameManager.SwitchGameState(GameStateID.VictoryP2);
            }
        }

        //press start button to pause the game
        if ( XCI.GetButtonDown(XboxButton.Start, controller) )
        {
            //Switch game states to pause
            gameManager.SwitchGameState(GameStateID.Pause);
        }

    }

    //Set item sprite display in the frame
    public void SetCollectedItem(ItemNum collectedItemType, int a_playerIndex) //player index 0,1
    {
        //set a null ref to put in which player to change sprite
        Image itemSprite = null;
        //check playerIndex to decide which UI need to change
        if (a_playerIndex == 0)
        {
            itemSprite = itemP1Sprite;
        }
        else
        {
            itemSprite = itemP2Sprite;
        }

        //if ItemNum = HealthKit
        if (collectedItemType == ItemNum.HeathKit)
        {
            //set sprite alpha to 1
            itemSprite.color = new Color(1, 1, 1, 1);
            //replace the current sprite to the first on of the list
            itemSprite.sprite = itemSpriteList[0];
        }

        //if ItemNum = Bumper
        if (collectedItemType == ItemNum.Bumper)
        {
            //set sprite alpha to 1
            itemSprite.color = new Color(1, 1, 1, 1);
            //replace the current sprite to the second on of the list
            itemSprite.sprite = itemSpriteList[1];
        }

        //if ItemNum = SpeedBoost
        if (collectedItemType == ItemNum.SpeedBoost)
        {
            //set sprite alpha to 1
            itemSprite.color = new Color(1, 1, 1, 1);
            //replace the current sprite to the third on of the list
            itemSprite.sprite = itemSpriteList[2];
        }
    }
}

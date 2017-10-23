using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIScript : MonoBehaviour
{
    private int currLap;
    private PlayerManager entity;

    private Image lapCountSprite;
    private Image rankSprite;
    private Image itemSprite;
    private Sprite[] lapSpriteList;
    private Sprite[] rankSpriteList;
    private Sprite[] itemSpriteList;

    private CarCheckpointScript checkpointScript;
	// Use this for initialization
	void Start ()
    {
        entity = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerManager>();

        checkpointScript = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CarCheckpointScript>();

        lapSpriteList = Resources.LoadAll<Sprite>("LapSprites");
        rankSpriteList = Resources.LoadAll<Sprite>("RankSprites");
        itemSpriteList = Resources.LoadAll<Sprite>("ItemIconSprites");

        lapCountSprite = gameObject.transform.Find("lapCount").GetComponent<Image>();
        rankSprite = gameObject.transform.Find("rank").GetComponent<Image>();
        itemSprite = gameObject.transform.Find("itemIcon").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        currLap = checkpointScript.currLap;

        lapCountSprite.sprite = lapSpriteList[currLap-1];

        if (entity.HasItem == false)
        {
            itemSprite.color = new Color(1, 1, 1, 0);
        }
    }

    public void SetCollectedItem(ItemNum collectedItemType)
    {
        if (collectedItemType == ItemNum.HeathKit)
        {
            itemSprite.color = new Color(1, 1, 1, 1);
            itemSprite.sprite = itemSpriteList[0];
        }

        if (collectedItemType == ItemNum.Bumper)
        {
            itemSprite.color = new Color(1, 1, 1, 1);
            itemSprite.sprite = itemSpriteList[1];
        }

        if (collectedItemType == ItemNum.SpeedBoost)
        {
            itemSprite.color = new Color(1, 1, 1, 1);
            itemSprite.sprite = itemSpriteList[2];
        }
    }
}

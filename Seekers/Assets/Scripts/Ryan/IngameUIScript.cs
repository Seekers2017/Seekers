using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUIScript : MonoBehaviour
{
    private int currLap;
    private GameObject playerObject;

    private Sprite lapCountSprite;
    private Sprite rankSprite;
    private Sprite itemSprite;
    private Sprite[] lapSpriteList;
    private Sprite[] rankSpriteList;
    private Sprite[] itemSpriteList;

	// Use this for initialization
	void Start ()
    {
        currLap = GameObject.FindGameObjectWithTag("Player").GetComponent<CarCheckpointScript>().currLap;

        lapCountSprite = gameObject.transform.Find("lapCount").GetComponent<Sprite>();
        rankSprite = gameObject.transform.Find("rank").GetComponent<Sprite>();
        itemSprite = gameObject.transform.Find("itemIcon").GetComponent<Sprite>();

        lapSpriteList = Resources.LoadAll<Sprite>("LapSprites");
        rankSpriteList = Resources.LoadAll<Sprite>("RankSprites");
        itemSpriteList = Resources.LoadAll<Sprite>("ItemIconSprites");
    }
	
	// Update is called once per frame
	void Update ()
    {
        lapCountSprite = lapSpriteList[currLap - 1];
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void CheckItemUsed()
    {
        //if
    }

    public void SetCollectedItem(ItemNum collectedItemType)
    {
        if (collectedItemType == ItemNum.Bumper)
        {
            itemSprite = itemSpriteList[1];
        }

        if (collectedItemType == ItemNum.SpeedBoost)
        {
            itemSprite = itemSpriteList[2];
        }
    }
}

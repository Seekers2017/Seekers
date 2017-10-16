using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUIScript : MonoBehaviour
{
    private int currLap;

    private Sprite lapCountSprite;
    private Sprite rankSprite;
    private Sprite[] lapSpriteList;
    private Sprite[] rankSpriteList;

	// Use this for initialization
	void Start ()
    {
        currLap = GameObject.FindGameObjectWithTag("Player").GetComponent<CarCheckpointScript>().currLap;

        lapCountSprite = gameObject.transform.Find("lapCount").GetComponent<Sprite>();
        rankSprite = gameObject.transform.Find("rank").GetComponent<Sprite>();

        lapSpriteList = Resources.LoadAll<Sprite>("LapSprites");
        rankSpriteList = Resources.LoadAll<Sprite>("RankSprites");
    }
	
	// Update is called once per frame
	void Update ()
    {
        lapCountSprite = lapSpriteList[currLap - 1];
    }

    //private CheckItem
}

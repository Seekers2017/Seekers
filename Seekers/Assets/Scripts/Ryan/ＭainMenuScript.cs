using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ＭainMenuScript : MonoBehaviour
{
    private Image startSprite;
    private Image quitSprite;

    private Sprite[] startSpriteList;
    private Sprite[] quitSpriteList;

    // Use this for initialization
    void Start ()
    {
        startSpriteList = Resources.LoadAll<Sprite>("MainStartSprites");
        quitSpriteList = Resources.LoadAll<Sprite>("MainQuitSprites");

        startSprite = gameObject.transform.Find("start").GetComponent<Image>();
        startSprite = gameObject.transform.Find("quit").GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

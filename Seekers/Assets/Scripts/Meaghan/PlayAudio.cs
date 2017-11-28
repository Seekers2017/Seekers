using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {

    //Varaibles
    public AudioClip musicClip;

    public AudioSource musicSource;

    private GameStateManagerScript currState;

	// Use this for initialization
	void Awake ()
    {
        musicSource.clip = musicClip;
        currState = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateManagerScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Play the song
        musicSource.Play();



        ////If we are in game
        //if (currState.currGameState.StateID == GameStateID.InGame)
        //{
        //    //Play the song
        //    musicSource.Play();
        //}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip Impact;
    AudioSource audioSource;
    //bool IsAttached;  
	// Use this for initialization
	void Start ()
    {
        //gets the audio source component
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision other)
    {
        // cheacks to see if the audio source exists and is attached to the object
        if (audioSource.GetComponent<AudioSource>() != null)
        {
            // plays the sound assigened to it in the inspector
            audioSource.PlayOneShot(Impact, 0.7f);
        }

        //stops the sound from player is currently not used but is here incase
        //audioSource.Stop();
    }
}

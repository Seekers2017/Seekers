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
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (audioSource.GetComponent<AudioSource>() != null)
        {
            audioSource.PlayOneShot(Impact, 0.7f);
        }

        //audioSource.Stop();
    }
}

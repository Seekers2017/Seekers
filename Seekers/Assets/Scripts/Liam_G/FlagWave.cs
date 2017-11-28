using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagWave : MonoBehaviour
{
    public Renderer rend;
    public float scroll;
    public float waveSpeed;
    //public float Scroll1;
    //public float Scroll2;
	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
        scroll = 0.9f;
        //Scroll1 = 0.1f;
        //Scroll2 = 0.9f;

	}
	
	// Update is called once per frame
	void Update ()
    {
        // set's the scroll back to 1 when it hits 0
        if  (scroll <= 0.1f)
        {
            scroll = 1.0f;
        }

        // scrolls through the offset form the flag matirial and then applyes it
        scroll -= waveSpeed;
        rend.material.SetFloat("_Flagoffset", scroll);


	}

}

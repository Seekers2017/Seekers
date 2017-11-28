using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    //prefabs and spawning locations
    private Rigidbody RB;
    public GameObject Block;
    //public GameObject FallingObjectSpawner;

    // all the variables used
    public Vector3 BlockSpawn = new Vector3(0, 0, 0);
    public Transform FallSpawn;
    public Transform FallSpawn1;
    public Transform FallSpawn2;
    public Transform FallSpawn3;
    public float Spawnlimite;
    public float spawnTimer;
    public float Timer;
    public float MaxY = 0;
    public float MinY = 0;

    // Use this for initialization
    void Start ()
    {
        //gets the riged body on the rigidbody component
        RB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Timer = 0;
        Timer++;
	}

    void OnTriggerEnter(Collider other)
    {
        //checks to see if an object taged with player or player2 has hit the trgger and spawns the falling blocks 
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            //makes sure theres a small delay before the blocks spawn
            if(Time.time > spawnTimer)
            { 
                //spawnTimer = Time.time + Spawnlimite;
                //set a vector3 veriable
                Vector3 rndPosWithin;

                // iterates through all the blocks that will spawn which were set in the inspector
                for (int i = 0; i < Spawnlimite; ++i)
                {
                    //creates random positions using the vector3 variable set previusly and spawns the blocks there 
                    rndPosWithin = new Vector3(Random.Range(FallSpawn.position.x, FallSpawn1.position.x), Random.Range(MinY, MaxY), Random.Range(FallSpawn2.position.z, FallSpawn3.position.z));
                    GameObject FallingObjectSpawner = Instantiate(Block, rndPosWithin, Quaternion.identity);
                }
            }
        }
    }
}

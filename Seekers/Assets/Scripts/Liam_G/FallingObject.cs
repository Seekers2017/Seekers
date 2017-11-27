using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    //prefabs and spawning locations
    private Rigidbody RB;
    public GameObject Block;
    //public GameObject FallingObjectSpawner;

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
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            if(Time.time > spawnTimer)
            {
                spawnTimer = Time.time + Spawnlimite;

                Vector3 rndPosWithin;
                //rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);

                for (int i = 0; i < Spawnlimite; ++i)
                {
                    rndPosWithin = new Vector3(Random.Range(FallSpawn.position.x, FallSpawn1.position.x), Random.Range(MinY, MaxY), Random.Range(FallSpawn2.position.z, FallSpawn3.position.z));
                    GameObject FallingObjectSpawner = Instantiate(Block, rndPosWithin, Quaternion.identity);
                    //Debug.Log("Instantiat Has been Reached");
                }
            }
        }
    }
}

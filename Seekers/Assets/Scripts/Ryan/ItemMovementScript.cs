using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovementScript : MonoBehaviour
{
    //Designers can change the rotation spped of the object
    public float rotationSpeed;

    // Use this for initialization
    void Awake()
    {
        //preset at 80
        rotationSpeed = 80f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed, 0));
    }
}

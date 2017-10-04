using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPlayer : MonoBehaviour {



    public WheelCollider frontWheelLeft;
    public WheelCollider frontWheelRight;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //constant movement forward when hitting key/mouse/button
        frontWheelLeft.motorTorque = 10;
        frontWheelRight.motorTorque = 10;
    
        //if holding left
        frontWheelLeft.steerAngle = -10;
        frontWheelRight.steerAngle = -10;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCarPart : MonoBehaviour {

    public Vector3 animationDirection;
    public AnimationCurve bounceCurve;
    public float bounceDuration = 4.0f;
    public float maxBounceHeight = 15.0f;
    private float bounceTimer = 0.0f;

    private bool isAnimating = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

        if(isAnimating)
        {
            bounceTimer += Time.deltaTime;

            float valueBetween0and1 = bounceTimer / bounceDuration;
            float curveValue = bounceCurve.Evaluate(valueBetween0and1);

            Vector3 eulerAngles = transform.localEulerAngles;

            
            eulerAngles = animationDirection * curveValue * maxBounceHeight;
          
            //set the rotation
            transform.localRotation = Quaternion.Euler(eulerAngles);

            if(valueBetween0and1 >= 1.0f)
            {
                //reset the animation back to start
                bounceTimer = 0.0f;
               // bounceDuration = Random.Range(1.0f, 2.0f);
                maxBounceHeight = Random.Range(30.0f, 60.0f);
            }
        }

	}

    /// <summary>
    /// Call this when the car gets damaged.
    /// </summary>
    public void BeginAnimation()
    {
        isAnimating = true;
    }


}

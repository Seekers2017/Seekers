using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    //Variables
    [Header("General")]
    public float speed;
    public float rotation;
    public int maxHits;
    public float boostSpeed = 500.0f;

    [Header("DO NOT TOUCH!!!")]
    public GameObject frontBumper;
    public GameObject leftBumper;
    public GameObject rearBumper;
    public GameObject rightBumper;

    //He attac, but he also protec (the variables)
    protected int hits;
    protected bool hasBumper;
    protected bool hasItem;
    protected int bumperSelect = 0;
    protected float boostTimer;
    public float maxBoostTimer = 1.0f;

    //Getters and setters
    public int Hits
    {
        get { return hits; }
        set { hits = value; }
    }

    public bool HasItem
    {
        get { return hasItem; }
        set { hasItem = value; }
    }
}

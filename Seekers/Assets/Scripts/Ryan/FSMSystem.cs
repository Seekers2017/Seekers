using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum class of to specify game states 
public enum GameStateID
{
    MainMenu,
    Tutoriul,
    InGame,
    InGameMuilti,
    Pause,
    Victory
}

//abstract class for children classes to inherit
public abstract class BaseState
{
    protected GameStateID stateID;

    // getter for the game state Enum class
    public GameStateID StateID
    {
        get { return stateID; }
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Shutdown()
    {

    }
}

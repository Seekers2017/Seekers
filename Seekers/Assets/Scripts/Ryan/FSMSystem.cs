using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateID
{
    MainMenu,
    Tutoriul,
    InGame,
    Pause,
    GameOver
}

public abstract class BaseState
{
    protected GameStateID stateID;

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

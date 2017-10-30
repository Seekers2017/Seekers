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

public abstract class FSMState
{
    protected GameStateID stateID;

    public GameStateID StateID
    {
        get { return stateID; }
    }
}

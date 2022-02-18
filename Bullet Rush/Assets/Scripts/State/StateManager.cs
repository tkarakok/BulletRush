using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State
{
    MainMenu,
    InGame,
    EndGame,
    GameOver
}

public class StateManager : Singleton<StateManager>
{
    [HideInInspector]
    public State state;


    private void Start()
    {
        state = State.MainMenu;
    }

}

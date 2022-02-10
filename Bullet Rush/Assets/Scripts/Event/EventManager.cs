using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void StateActions();
    public StateActions MainMenu;
    public StateActions InGame;
}

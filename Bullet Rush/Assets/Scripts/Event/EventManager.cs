using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void StateActions();
    public StateActions MainMenu;
    public StateActions InGame;

    public delegate void Collect();
    public Collect CollectBullet;


    private void Awake()
    {
        MainMenu += SubscribeAllEvent;
        MainMenu();
    }

    void SubscribeAllEvent()
    {
        CollectBullet += BulletMovementController.Instance.StartWave;
    }

}

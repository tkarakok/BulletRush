using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void StateActions();
    public StateActions MainMenu;
    public StateActions InGame;
    public StateActions GameOver;
    public StateActions EndGame;

    public delegate void Collect();
    public Collect CollectBullet;


    private void Awake()
    {
        MainMenu += SubscribeAllEvent;
        MainMenu += UIManager.Instance.MainMenuUIUpdate;
        MainMenu();
    }

    void SubscribeAllEvent()
    {
        // collect event
        CollectBullet += BulletMovementController.Instance.StartWave;
        // ýn game
        InGame += () => StateManager.Instance.state = State.InGame;
        InGame += UIManager.Instance.InGameCoinUpdate;
        InGame += UIManager.Instance.InGameLevelUpdate;
        InGame += () => AudioManager.Instance.gameMusicAudioSource.enabled = true;

        // gameover 
        GameOver += () => StateManager.Instance.state = State.GameOver;

        // end game
        EndGame += () => StateManager.Instance.state = State.EndGame;
        EndGame += () => AudioManager.Instance.PlaySound(AudioManager.Instance.finishClip);
        EndGame += UIManager.Instance.EndGameTotalCoinTextUpdate;
        EndGame += UIManager.Instance.EndGameMagazineTextUpdate;
        EndGame += UIManager.Instance.EndGameMultiplierTextUpdate;
        EndGame += UIManager.Instance.EndGameTotalEarnedTextUpdate;



    }

}

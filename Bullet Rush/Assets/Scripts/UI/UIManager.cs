using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject inGamePanel;
    public GameObject gameOverPanel;
    public GameObject endGamePanel;
    public GameObject rateUsPanel;
    [Header("Settings")]
    public Scrollbar volume;
    public GameObject volumeHandle;
    [Header("Rate Us")]
    private string _packageName = "com.avenpigame.BulletRunning";
    [Header("Main Menu")]
    public Text mainMenuTotalCoinText;
    public Text mainMenuLevelText;
    public Animator animator;
    [Header("In Game")]
    public Text inGameCoinText;
    public Text inGameCurrentLevelText;
    public Slider levelProgressBar;
    [Header("End Game")]
    public Text endGameTotalCoinText;
    public Text endGameMagazineText;
    public Text endGameMultiplierText;
    public Text endGameTotalEarnedText;
    public GameObject endGameClaimButton;
    public GameObject endGameNextLevelButton;
    public Transform[] claimCoinSprites;
    public Transform targetSprite;
    private int totalEarnedCoin;


    private GameObject _currentPanel;



    private void Start()
    {
        _currentPanel = mainMenuPanel;
        if (LevelManager.Instance.CurrentLevel %3 == 0)
        {
            if (PlayerPrefs.GetInt("Rate") == 0)
            {
                rateUsPanel.SetActive(true);
            }
        }
    }

    public void GameOver()
    {
        PanelChange(gameOverPanel);
        EventManager.Instance.GameOver();
        AdManager.Instance.InterstitialAdShow();
        AudioManager.Instance.PlaySound(AudioManager.Instance.gameOverClip);
    }
    public void EndGame()
    {
        PanelChange(endGamePanel);
        EventManager.Instance.EndGame();
    }

    #region Panel
    public void PanelChange(GameObject openPanel)
    {
        _currentPanel.SetActive(false);
        openPanel.SetActive(true);
        _currentPanel = openPanel;
    }
    #endregion

    #region UI UPDATE
    // main menu UI
    public void MainMenuUIUpdate()
    {
        mainMenuTotalCoinText.text = PlayerPrefs.GetInt("Coin").ToString();
        mainMenuLevelText.text = "LEVEL " + (LevelManager.Instance.CurrentLevel).ToString();
    }
    // In Game
    public void InGameCoinUpdate()
    {
        inGameCoinText.text = GameManager.Instance.bulletsInMagazine.ToString();
    }
    public void InGameLevelUpdate()
    {
        inGameCurrentLevelText.text = LevelManager.Instance.CurrentLevel.ToString();
    }
    // end game
    public void EndGameTotalCoinTextUpdate()
    {
        endGameTotalCoinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    public void EndGameMagazineTextUpdate()
    {
        endGameMagazineText.text = GameManager.Instance.bulletsInMagazine.ToString();
    }
    public void EndGameMultiplierTextUpdate()
    {
        endGameMultiplierText.text = GameManager.Instance.bonusMultiplier.ToString();
    }
    public void EndGameTotalEarnedTextUpdate()
    {
        totalEarnedCoin = GameManager.Instance.bulletsInMagazine * GameManager.Instance.bonusMultiplier;
        endGameTotalEarnedText.text = totalEarnedCoin.ToString();
        
    }
    
    #endregion

    #region Buttons
    public void StartButton()
    {
        EventManager.Instance.InGame();
        PanelChange(inGamePanel);
        AudioManager.Instance.PlaySound(AudioManager.Instance.uiClickClip);
    }
    public void SettingsPanelVolume()
    {
        if (volume.value <= .5f)
        {
            AudioManager.Instance.AudioController();
            volumeHandle.GetComponent<Image>().color = Color.red;
        }
        else
        {
            AudioManager.Instance.AudioController();
            volumeHandle.GetComponent<Image>().color = Color.green;
        }
    }
    public void SettingsButton()
    {
        settingsPanel.SetActive(true);
        animator.SetBool("Settings",true);
        AudioManager.Instance.PlaySound(AudioManager.Instance.uiClickClip);
    }
    public void BackToMenu()
    {
        settingsPanel.SetActive(false);
        animator.SetBool("Settings", false);
    }
    public void RestartButton()
    {
        LevelManager.Instance.ChangeLevel("LEVEL " + LevelManager.Instance.CurrentLevel);
        AudioManager.Instance.PlaySound(AudioManager.Instance.uiClickClip);
    }
    public void NextLevelButton()
    {
        LevelManager.Instance.ChangeLevel("LEVEL " + LevelManager.Instance.GetLevelName());
        AudioManager.Instance.PlaySound(AudioManager.Instance.uiClickClip);

    }
    public void EndGameClaimButton()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        coin += totalEarnedCoin;
        PlayerPrefs.SetInt("Coin",coin);
        PlayerPrefs.SetInt("Level",LevelManager.Instance.CurrentLevel);
        StartCoroutine(ClaimCoinAnimation());
        endGameClaimButton.SetActive(false);
        endGameNextLevelButton.SetActive(true);
    }
    #endregion

    #region ClaimButtonSpriteAnim
    IEnumerator ClaimCoinAnimation()
    {
        for (int i = 0; i < claimCoinSprites.Length; i++)
        {
            claimCoinSprites[i].gameObject.SetActive(true);
            claimCoinSprites[i].DOMove(targetSprite.position,.2f).OnComplete(()=> claimCoinSprites[i].gameObject.SetActive(false));
            yield return new WaitForSeconds(.1f);
            EndGameTotalCoinTextUpdate();
        }
    }
    #endregion

    #region Rate Us
    public void YesButton()
    {
        Application.OpenURL("market://details?id=" + _packageName);
        PlayerPrefs.SetInt("Rate", 1);
    }
    public void NoButton()
    {
        PlayerPrefs.SetInt("Rate", 1);
        rateUsPanel.SetActive(false);
    }
    public void LaterButton()
    {
        rateUsPanel.SetActive(false);
    }
    #endregion
}

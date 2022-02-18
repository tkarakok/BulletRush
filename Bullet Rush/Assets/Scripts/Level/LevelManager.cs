using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    private Scene _lastLoadedScene;
    private int _currentLevel;

    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }

    private void Start()
    {
        
        ChangeLevel("LEVEL " + GetLevelName());
    }
   
    public int GetLevelName()
    {
        CurrentLevel = (PlayerPrefs.GetInt("Level") + 1);
        return CurrentLevel;
    }

    public void ChangeLevel(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
    }

    IEnumerator ChangeScene(string sceneName)
    {
        if (_lastLoadedScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(_lastLoadedScene);
            bool sceneUnloaded = false;
            while (!sceneUnloaded)
            {
                sceneUnloaded = !_lastLoadedScene.IsValid();
                yield return new WaitForEndOfFrame();
            }
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        bool sceneLoaded = false;
        while (!sceneLoaded)
        {
            _lastLoadedScene = SceneManager.GetSceneByName(sceneName);
            sceneLoaded = _lastLoadedScene != null && _lastLoadedScene.isLoaded;
            yield return new WaitForEndOfFrame();
        }
    }
}
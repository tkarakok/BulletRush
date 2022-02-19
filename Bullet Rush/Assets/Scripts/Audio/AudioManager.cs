using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    
    public AudioSource sfxAudioSource,gameMusicAudioSource;
    public AudioClip inGameMusic, collectClip, finishClip, uiClickClip, confettiClip;
    

    bool _muted = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
        }
        else
        {
            LoadSettings();
        }
        
        AudioListener.pause = _muted;
    }

    public void PlaySound(AudioClip audioClip)
    {
        sfxAudioSource.PlayOneShot(audioClip);
    }

    public void AudioController()
    {
        if (UIManager.Instance.volume.value <= .5f)
        {
            _muted = true;
            AudioListener.pause = true;
        }
        else
        {
            _muted = false;
            AudioListener.pause = false;
        }
        Debug.Log(_muted);
        SaveSettings();
    }

    private void LoadSettings()
    {
        _muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void SaveSettings()
    {
        PlayerPrefs.SetInt("muted", _muted ? 1 : 0);
    }
}
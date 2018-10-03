using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider SFXSlider;
    public Text musicText;
    public Text SFXText;

    public Slider autoRetryToggle;
    public Slider restartBttnToggle;
    public Slider gamePRogressToggle;

    private void Start()
    {
        musicSlider.value = GameSettings.Instance.musicVolume;
        SFXSlider.value = GameSettings.Instance.SFXVolume;
        musicText.text = (int)(musicSlider.value * 100) + "%";
        SFXText.text = (int)(SFXSlider.value * 100) + "%";
        autoRetryToggle.value = GameSettings.Instance.autoRetry ? 1 : 0;
        restartBttnToggle.value = GameSettings.Instance.showRestartButton ? 1 : 0;
        gamePRogressToggle.value = GameSettings.Instance.showGameProgress ? 1 : 0;
    }

    public void MusicVolumeChanged(float volume)
    {
        GameSettings.MusicVolumeChanged(volume);
        musicText.text = (int)(musicSlider.value * 100) + "%";
    }

    public void SFXVolumeChanged(float volume)
    {
        GameSettings.SFXVolumeChanged(volume);
        SFXText.text = (int)(SFXSlider.value * 100) + "%";
    }

    public void AutoRetryChanged(float value)
    {
        GameSettings.AutoRetryChanged(value==0?false:true);
    }

    public void RestartBttnChanged(float value)
    {
        GameSettings.RestartBttnChanged(value == 0 ? false : true);
    }

    public void GameProgressChanged(float value)
    {
        GameSettings.GameProgressChanged(value == 0 ? false : true);
    }

    public void LoadMenu()
    {
        SceneLoader.LoadLevel("Menu");
    }

    internal void Disable()
    {
        this.gameObject.SetActive(false);
    }
}

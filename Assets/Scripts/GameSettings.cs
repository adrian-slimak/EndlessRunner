using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    public float musicVolume;
    public float SFXVolume;

    public bool autoRetry;
    public bool showRestartButton;
    public bool showGameProgress;

    public List<SkinInfo> skinInfo;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this);
    }

    void Start ()
    {
        musicVolume = PlayerPrefsManager.GetMusicVolume();
        SFXVolume = PlayerPrefsManager.GetSFXVolume();
        autoRetry = PlayerPrefsManager.GetAutoRetry() == 0 ? false : true;
        showRestartButton = PlayerPrefsManager.GetRestartButton() == 0 ? false : true;
        showGameProgress = PlayerPrefsManager.GetGameProgress() == 0 ? false : true;

        GetSkinInfo();
	}

    public static void MusicVolumeChanged(float volume)
    {
        Instance.musicVolume = volume;
        PlayerPrefsManager.SetMusicVolume(volume);
    }

    public static void SFXVolumeChanged(float volume)
    {
        Instance.SFXVolume = volume;
        PlayerPrefsManager.SetSFXVolume(volume);
    }

    public static void AutoRetryChanged(bool value)
    {
        Instance.autoRetry = value;
        PlayerPrefsManager.SetAutoRetry(value ? 1 : 0);
    }

    public static void RestartBttnChanged(bool value)
    {
        Instance.showRestartButton = value;
        PlayerPrefsManager.SetRestartButton(value ? 1 : 0);
    }

    public static void GameProgressChanged(bool value)
    {
        Instance.showGameProgress = value;
        PlayerPrefsManager.SetGameProgress(value ? 1 : 0);
    }

    private static void GetSkinInfo()
    {
        Instance.skinInfo = SQLDataBase.LoadSkins();
    }

    public static void SkinChanged(int mechanicID)
    {
        SQLDataBase.SaveSkinInfo(Instance.skinInfo[mechanicID]);
    }
}

public class SkinInfo
{
    public int MechanicID;
    public int skinSelected;
    public Color firstColor;
    public int firstColorIndex;
    public Color secondColor;
    public int secondColorIndex;
    public string spriteName;

    public SkinInfo(int mechanicID)
    {
        this.MechanicID = mechanicID;
    }
}

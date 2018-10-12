using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{
	const string MUSIC_VOLUME_KEY = "master_volume";
	const string SFX_VOLUME_KEY = "sfx_volume";
	const string AUTO_RETRY_KEY = "auto_retry";
	const string RESTART_BUTTON_KEY = "restart_button";
	const string GAME_PROGRESS_SLIDER_KEY = "game_progress_slider";

    const string FIRST_SKIN_COLOR = "first_skin_color";
    const string SECOND_SKIN_COLOR = "second_skin_color";
    const string FIRST_SKIN_COLOR_INDEX = "first_skin_color_index";
	const string SECOND_SKIN_COLOR_INDEX = "second_skin_color_index";
    const string PLAYER_POINTS = "player_points";


    public static void SetPlayerPoints(int value)
    {
        PlayerPrefs.SetInt(PLAYER_POINTS, value);
    }

    public static int GetPlayerPoints()
    {
        return PlayerPrefs.GetInt(PLAYER_POINTS, 0);
    }

    public static void SetMusicVolume(float volume)
	{
		if (volume >= 0f && volume <= 1f)
			PlayerPrefs.SetFloat (MUSIC_VOLUME_KEY, volume);
		else
			Debug.LogWarning ("Muusic Volume Out Of Range");
	}

	public static float GetMusicVolume()
	{
		return PlayerPrefs.GetFloat (MUSIC_VOLUME_KEY, 0.5f);
	}

    public static void SetSFXVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
        else
            Debug.LogWarning("SFX Volume Out Of Range");
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.5f);
    }

    public static void SetAutoRetry(int value)
    {
        if (value != 0 && value != 1) Debug.Log("PlayerPrefs bad value");
        PlayerPrefs.SetInt(AUTO_RETRY_KEY, value);
    }

    public static int GetAutoRetry()
    {
        return PlayerPrefs.GetInt(AUTO_RETRY_KEY);
    }

    public static void SetRestartButton(int value)
    {
        if (value != 0 && value != 1) Debug.Log("PlayerPrefs bad value");
        PlayerPrefs.SetInt(RESTART_BUTTON_KEY, value);
    }

    public static int GetRestartButton()
    {
        return PlayerPrefs.GetInt(RESTART_BUTTON_KEY);
    }

    public static void SetGameProgress(int value)
    {
        if (value != 0 && value != 1) Debug.Log("PlayerPrefs bad value");
        PlayerPrefs.SetInt(GAME_PROGRESS_SLIDER_KEY, value);
    }

    public static int GetGameProgress()
    {
        return PlayerPrefs.GetInt(GAME_PROGRESS_SLIDER_KEY);
    }
}

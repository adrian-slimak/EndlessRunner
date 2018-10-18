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

    const string TOTAL_PLAYER_ATTEMPTS = "total_player_attempts";
    const string TOTAL_PLAYER_JUMPS = "total_player_jumps";
    const string TOTAL_TIME_REWINDED = "total_time_rewinded";
    const string TOTAL_HIDDEN_SHARDS_COLLECTED = "total_hidden_shards_collected";
    const string TOTAL_STARS_COLLECTED = "total_stars_collected";
    const string TOTAL_COLORS_UNLOCKED = "total_colors_unlocked";
    const string PLAYER_POINTS = "player_points";


    public static void SetPlayerPoints(int value)
    {
        PlayerPrefs.SetInt(PLAYER_POINTS, value);
    }

    public static int GetPlayerPoints()
    {
        return PlayerPrefs.GetInt(PLAYER_POINTS, 0);
    }

    public static void SetTotalPlayerAttempts(int value)
    {
        PlayerPrefs.SetInt(TOTAL_PLAYER_ATTEMPTS, value);
    }

    public static int GetTotalPlayerAttempts()
    {
        return PlayerPrefs.GetInt(TOTAL_PLAYER_ATTEMPTS, 0);
    }

    public static void SetTotalPlayerJumps(int value)
    {
        PlayerPrefs.SetInt(TOTAL_PLAYER_JUMPS, value);
    }

    public static int GetTotalPlayerJumps()
    {
        return PlayerPrefs.GetInt(TOTAL_PLAYER_JUMPS, 0);
    }

    public static void SetTotalTimeRewinded(float value)
    {
        PlayerPrefs.SetFloat(TOTAL_TIME_REWINDED, value);
    }

    public static float GetTotalTimeRwinded()
    {
        return PlayerPrefs.GetFloat(TOTAL_TIME_REWINDED, 0);
    }

    public static void SetTotalHiddenCollected(int value)
    {
        PlayerPrefs.SetInt(TOTAL_HIDDEN_SHARDS_COLLECTED, value);
    }

    public static int GetTotalHiddenCollected()
    {
        return PlayerPrefs.GetInt(TOTAL_HIDDEN_SHARDS_COLLECTED, 0);
    }

    public static void SetTotalStarsCollected(int value)
    {
        PlayerPrefs.SetInt(TOTAL_STARS_COLLECTED, value);
    }

    public static int GetTotalStarsCollected()
    {
        return PlayerPrefs.GetInt(TOTAL_STARS_COLLECTED, 0);
    }

    public static void SetTotalColorsUnlocked(int value)
    {
        PlayerPrefs.SetInt(TOTAL_COLORS_UNLOCKED, value);
    }

    public static int GetTotalColorsUnlocked()
    {
        return PlayerPrefs.GetInt(TOTAL_COLORS_UNLOCKED, 6);
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

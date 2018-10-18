using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsistics : MonoBehaviour
{
    public static PlayerStatsistics Instance;

    public int totalPlayerAttempts;
    public int totalPlayerJumps;
    public float totalTimeRewinded;
    public int totalHiddenShardsCollected;
    public int totalStarsCollected;
    public int totalColorsCollected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this);
    }

    private void Start()
    {
        totalPlayerJumps = PlayerPrefsManager.GetTotalPlayerAttempts();
        totalPlayerJumps = PlayerPrefsManager.GetTotalPlayerJumps();
        totalTimeRewinded = PlayerPrefsManager.GetTotalTimeRwinded();
        totalHiddenShardsCollected = PlayerPrefsManager.GetTotalHiddenCollected();
        totalStarsCollected = PlayerPrefsManager.GetTotalStarsCollected();
        totalColorsCollected = PlayerPrefsManager.GetTotalColorsUnlocked();
    }

    public static void NewColorUnlocked()
    {
        Instance.totalColorsCollected++;
        PlayerPrefsManager.SetTotalColorsUnlocked(Instance.totalColorsCollected);
        AchievementManager.CheckColorsAchiev();
    }

    public static void SaveStats()
    {
        PlayerPrefsManager.SetTotalPlayerAttempts(Instance.totalPlayerAttempts);
        PlayerPrefsManager.SetTotalPlayerJumps(Instance.totalPlayerJumps);
        PlayerPrefsManager.SetTotalTimeRewinded(Instance.totalTimeRewinded);
        PlayerPrefsManager.SetTotalHiddenCollected(Instance.totalHiddenShardsCollected);
        PlayerPrefsManager.SetTotalStarsCollected(Instance.totalStarsCollected);
    }
}

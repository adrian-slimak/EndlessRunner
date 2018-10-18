using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    internal LevelInfo levelInfo;
    public GameplayUI gameplayUI;

    public int levelID = -1;
    public int lvlCompleteAchievID;
    public int lvlCompletePracticeAchievID;
    public int allHiddenShardsAchievID;
    public int levelLength;
    public float DeathRestartDelay = 3f;

    public bool practiceMode = false;

    public RunnerController Player;
    [HideInInspector]
    public bool gamePaused = false;

    [HideInInspector]
    public int totalAttempts = 0;
    [HideInInspector]
    public int totalJumps = 0;
    [HideInInspector]
    public Vector3 shardsCollected;

    private int levelProgress;

    void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            levelInfo = LevelManager.GetLevelInfo(levelID);
        }
        else Destroy(this);

        Instance.totalAttempts++;
        if(!practiceMode) Instance.levelInfo.totalAttempts++;
        PlayerStatsistics.Instance.totalPlayerAttempts++;
        AchievementManager.CheckAttemptsAchiev();
        shardsCollected = Vector3.zero;
        Instance.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<RunnerController>();
        Instance.gameplayUI = FindObjectOfType<GameplayUI>();
    }

    public static void PlayerJumped()
    {
        Instance.levelInfo.totalJumps++;
        Instance.totalJumps++;
        PlayerStatsistics.Instance.totalPlayerJumps++;
        AchievementManager.CheckJumpsAchiev();
    }

    public static void PlayerDied() // BUG jak ktoś zresetuje level kiedy player zginie
    {
        if (Instance.practiceMode)
        { TimeController.PlayerDied(); return; }

        Instance.SaveLevel();

        if (GameSettings.Instance.autoRetry)
            Instance.Invoke("RestartLevel", Instance.DeathRestartDelay);
        else
            Instance.Invoke("ShowGameOverPanel", Instance.DeathRestartDelay/2);
    }

    private void RestartLevel()
    {
        SceneLoader.RestartCurrentScene();
    }

    private void ShowGameOverPanel()
    {    Instance.gameplayUI.ShowGameOverPanel(); }

    public static void LevelFinished()
    {
        if (Instance.practiceMode)
        {
            Instance.levelInfo.levelFinishedPractice = true;
            AchievementManager.AchievementCollected(Instance.lvlCompletePracticeAchievID);
        }
        else
        {
            if (Instance.levelInfo.shardsCollected.x == 0 && Instance.shardsCollected.x == 1)
            { Instance.levelInfo.shardsCollected.x = 1; PlayerStatsistics.Instance.totalHiddenShardsCollected++; }
            if (Instance.levelInfo.shardsCollected.y == 0 && Instance.shardsCollected.y == 1)
            { Instance.levelInfo.shardsCollected.y = 1; PlayerStatsistics.Instance.totalHiddenShardsCollected++; }
            if (Instance.levelInfo.shardsCollected.z == 0 && Instance.shardsCollected.z == 1)
            { Instance.levelInfo.shardsCollected.z = 1; PlayerStatsistics.Instance.totalHiddenShardsCollected++; }

            if (Instance.shardsCollected == Vector3.one)
                AchievementManager.AchievementCollected(Instance.allHiddenShardsAchievID);

            AchievementManager.CheckHiddenShardsAchiev();

            if (!Instance.levelInfo.levelFinished)
            {
                PlayerStatsistics.Instance.totalStarsCollected += Instance.levelInfo.maxStars;
                Instance.levelInfo.levelFinished = true;
                AchievementManager.AchievementCollected(Instance.lvlCompleteAchievID);
            }

            AchievementManager.CheckStarsCollectedAchiev();
        }

        Instance.SaveLevel();
        Instance.gameplayUI.ShowLevelCompletePanel();
        PauseGame();
        Instance.totalAttempts = 0;
        Instance.totalJumps = 0;
        Instance.shardsCollected = Vector3.zero;
    }

    public static void RestartLevelStatic()
    {
        Instance.Player.gameObject.SetActive(false);
        SceneLoader.LoadLevel("Level" + Instance.levelID, animSpeed:2);
        ResumeGame();
    }

    public static void ExitLevel()
    {
        Instance.SaveLevel();

        SceneLoader.LoadLevel("LevelPreview");
        ResumeGame();
        Instance.Player.gameObject.SetActive(false);
        Destroy(Instance.gameObject);
    }

    private void SaveLevel()
    {
        int levelProgress = (int)(CameraController.Instance.transform.position.x * 100 / Instance.levelLength);
        if (practiceMode)
        {
            if (levelProgress > levelInfo.practiceModeProgress)
                levelInfo.practiceModeProgress = levelProgress;
        }
        else if(levelProgress > levelInfo.normalModeProgress)
        {

            levelInfo.normalModeProgress = levelProgress;
            int points = (int)((levelProgress / 100f) * levelInfo.maxPoints) - levelInfo.pointsCollected;
            PlayerPrefsManager.SetPlayerPoints(points + PlayerPrefsManager.GetPlayerPoints());
            levelInfo.pointsCollected += points;
        }

        PlayerStatsistics.SaveStats();
        LevelManager.SaveLevelToDataBase(levelID);
    }

    public static void PauseGame()
    {
        Instance.gamePaused = true;
        Time.timeScale = 0;
    }

    public static void ResumeGame()
    {
        Instance.gamePaused = false;
        Time.timeScale = 1;
    }

    public static void HiddenShardCollected(int id)
    {
        if (id == 1)
            Instance.shardsCollected.x = 1;

        if (id == 2)
            Instance.shardsCollected.y = 1;

        if (id == 3)
            Instance.shardsCollected.z = 1;

    }

    private void OnApplicationPause(bool pause)
    {
        SaveLevel();
    }
}

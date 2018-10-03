using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    internal LevelInfo levelInfo;
    new Transform camera;
    GameplayUI gameplayUI;

    public int levelID = -1;
    public int lvlCompleteAchievID;
    public int lvlCompletePracticeAchievID;
    public int allHiddenShardsAchievID;
    public int levelLength;
    public float DeathRestartDelay = 3f;



    [HideInInspector]
    public bool practiceMode = false;

    [HideInInspector]
    public RunnerController Player;

    [HideInInspector]
    public int totalAttempts = 1;
    [HideInInspector]
    public int totalJumps = 0;
    [HideInInspector]
    public Vector3 hiddenShardsCollected;

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

        hiddenShardsCollected = Vector3.zero;
        Instance.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<RunnerController>();
        Instance.gameplayUI = FindObjectOfType<GameplayUI>();
    }

    public static void PlayerJumped()
    {
        Instance.levelInfo.totalJumps++;
        Instance.totalJumps++;
    }

    public static void PlayerDied() // BUG jak ktoś zresetuje level kiedy player zginie
    {
        if (GameSettings.Instance.autoRetry)
            Instance.Invoke("RestartLevel", Instance.DeathRestartDelay);
        else
            Instance.Invoke("ShowGameOverPanel", Instance.DeathRestartDelay/2);
    }

    private void RestartLevel()
    {
        Instance.totalAttempts++;
        Instance.levelInfo.totalAttempts++;

        SceneLoader.RestartCurrentScene();
    }

    private void ShowGameOverPanel()
    {    Instance.gameplayUI.ShowGameOverPanel();   }

    public static void LevelFinished()
    {
        if (Instance.practiceMode)
        {
            Instance.levelInfo.levelFinishedPractice = true;
            //AchievementManager.AchievementCollected(Instance.lvlCompletePracticeAchievID);
        }
        else
        {
            Instance.levelInfo.levelFinished = true;
            //AchievementManager.AchievementCollected(Instance.lvlCompleteAchievID);
        }

        Instance.SaveLevel();

        Instance.gameplayUI.ShowLevelCompletePanel(Instance.totalJumps, Instance.totalAttempts, Instance.hiddenShardsCollected);
        Instance.totalAttempts = 0;
        Instance.totalJumps = 0;
        Instance.hiddenShardsCollected = Vector3.zero;
    }

    public static void RestartLevelStatic()
    {
        Instance.totalAttempts++;
        Instance.levelInfo.totalAttempts++;
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
            levelInfo.pointsCollected = (levelProgress / 100) * levelInfo.maxPoints;
        }

        LevelManager.SaveLevelToDataBase(levelID);
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public static void HiddenShardCollected(int id)
    {
        //if (Instance.hiddenCoinsCollected == Vector3.one)
            //AchievementManager.AchievementCollected(Instance.allHiddenShardsAchievID);

        if (id == 1)
        {
            Instance.hiddenShardsCollected.x = 1;
            Instance.levelInfo.hiddenCoinsCollected.x = 1;
        }

        if (id == 2)
        {
            Instance.hiddenShardsCollected.y = 1;
            Instance.levelInfo.hiddenCoinsCollected.y = 1;
        }

        if (id == 3)
        {
            Instance.hiddenShardsCollected.z = 1;
            Instance.levelInfo.hiddenCoinsCollected.z = 1;
        }
    }
}

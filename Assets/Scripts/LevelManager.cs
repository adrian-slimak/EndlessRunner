using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private Dictionary<int, LevelInfo> gameLevelsInfo;

    void Awake()
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
        if (gameLevelsInfo == null)
            gameLevelsInfo = SQLDataBase.LoadLevels();
    }

    public static void SaveLevelToDataBase(int LevelID)
    {
        SQLDataBase.SaveLevel(Instance.gameLevelsInfo[LevelID]);
    }

    public static LevelInfo GetLevelInfo(int levelID)
    {
        return Instance.gameLevelsInfo[levelID];
    }

    public static List<LevelInfo> GetLevels()
    {
        return new List<LevelInfo>(Instance.gameLevelsInfo.Values);
    }

    public static int GetLevelCount()
    {
        return Instance.gameLevelsInfo.Count;
    }

}

public class LevelInfo
{
    public readonly int LevelID;
    public readonly string LevelName;
    public readonly int maxStars;
    public readonly int maxPoints;

    public LevelInfo(int LevelID, string LevelName, int maxStars, int maxPoints)
    {
        this.LevelID = LevelID;
        this.LevelName = LevelName;
        this.maxStars = maxStars;
        this.maxPoints = maxPoints;
    }

    public int totalAttempts = 0;
    public int totalJumps = 0;

    public bool levelFinished = false;
    public bool levelFinishedPractice = false;

    public int normalModeProgress = -1;
    public int practiceModeProgress = -1;

    public int pointsCollected = -1;
    public Vector3 shardsCollected = new Vector3(-1, -1, -1);

}

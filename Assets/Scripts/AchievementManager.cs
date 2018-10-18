using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;
    public enum Category { ALL = 0, LEVELS = 1, STATICTICS = 2, TROPHY = 3};

    public Animator animator;
    public GameObject achievPanel;
    public float achievShowTime = 5f;

    List<Achievement> achievements;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else Destroy(this);
    }

    void Start()
    {
        achievements = SQLDataBase.LoadAchievements();
    }

    private void Update()
    {
        if (Input.touchCount > 2)
        {
            Debug.Log("achievementCompleted");
            AchievementCollected(1);
        }
    }

    public static List<Achievement> GetAchievements(Category cat)
    {
        if (cat == Category.ALL)
            return Instance.achievements;
        else
            return Instance.achievements.FindAll(a => a.category == (int) cat);
    }

    public static Achievement GetAchievement(int id)
    {
        return Instance.achievements[id];
    }

    #region CheckAchievements
    internal static void CheckJumpsAchiev() // POPRAWIĆ ID OSIĄGNIĘĆ
    {
        int jumps = PlayerStatsistics.Instance.totalPlayerJumps;

        if (jumps == 250)
            AchievementCollected(105);
        else
        if (jumps == 500)
            AchievementCollected(106);
        else
        if (jumps == 1000)
            AchievementCollected(107);
        else
        if (jumps == 5000)
            AchievementCollected(108);
        else
        if (jumps == 10000)
            AchievementCollected(109);
    }

    internal static void CheckAttemptsAchiev() // POPRAWIĆ ID OSIĄGNIĘĆ
    {
        int attempts = PlayerStatsistics.Instance.totalPlayerAttempts;

        if (attempts == 100)
            AchievementCollected(113);
        else
        if (attempts == 500)
            AchievementCollected(114);
        else
        if (attempts == 1000)
            AchievementCollected(115);
        else
        if (attempts == 5000)
            AchievementCollected(116);
        else
        if (attempts == 10000)
            AchievementCollected(117);
    }

    internal static void CheckTimeRewindedAchiev() // POPRAWIĆ ID OSIĄGNIĘĆ
    {
        float time = PlayerStatsistics.Instance.totalTimeRewinded;

        if (time > 60)
            AchievementCollected(110);
        else
        if (time == 300)
            AchievementCollected(111);
        else
        if (time == 600)
            AchievementCollected(112);
    }

    internal static void CheckHiddenShardsAchiev() // POPRAWIĆ ID OSIĄGNIĘĆ
    {
        int shards = PlayerStatsistics.Instance.totalHiddenShardsCollected;

        if (shards == 3)
            AchievementCollected(101);
        else
        if (shards == 9)
            AchievementCollected(102);
        else
        if (shards == 15)
            AchievementCollected(103);
        else
        if (shards == 15)
            AchievementCollected(104);
    }

    internal static void CheckStarsCollectedAchiev() // POPRAWIĆ ID OSIĄGNIĘĆ
    {
        int stars = PlayerStatsistics.Instance.totalStarsCollected;

        if (stars == 5)
            AchievementCollected(118);
        else
        if (stars == 10)
            AchievementCollected(119);
        else
        if (stars == 25)
            AchievementCollected(120);
    }

    internal static void CheckColorsAchiev() // POPRAWIĆ ID OSIĄGNIĘĆ
    {
        int stars = PlayerStatsistics.Instance.totalColorsCollected;

        if (stars == 10)
            AchievementCollected(121);
        else
        if (stars == 25)
            AchievementCollected(122);
        else
        if (stars == 48)
            AchievementCollected(123);
    }

    #endregion

    public static void AchievementCollected(int achievID)
    {
        if (Instance.achievements[achievID - 1].collected) return;

        Instance.achievPanel.SetActive(true);
        Text[] texts = Instance.achievPanel.GetComponentsInChildren<Text>();
        texts[0].text = Instance.achievements[achievID - 1].name;
        texts[1].text = Instance.achievements[achievID - 1].description;

        Instance.animator.SetTrigger("ShowAchiev");
        Instance.Invoke("HideAchievementPanel", Instance.achievShowTime);

        SQLDataBase.SaveAchievement(achievID);
    }

    private void HideAchievementPanel()
    {
        Instance.animator.SetTrigger("HideAchiev");
        Instance.Invoke("DisableAchievementPanel", 1);
    }

    private void DisableAchievementPanel()
    {
        Instance.achievPanel.SetActive(false);
    }
}

public class Achievement
{
    public int AchievementID;
    public string name;
    public string description;
    public bool collected;
    public int category;
    public string date;
    public string image;

    public Achievement(int AchievementID, string name)
    {
        this.AchievementID = AchievementID;
        this.name = name;
    }
}

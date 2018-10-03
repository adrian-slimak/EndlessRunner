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
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("achievementCompleted");
            AchievementCollected(4);
        }
    }

    public static List<Achievement> GetAchievements(Category cat)
    {
        if (cat == Category.ALL)
            return Instance.achievements;
        else
            return Instance.achievements.FindAll(a => a.category == (int) cat);
    }

    public static void AchievementCollected(int achievID)
    {
        //if (Instance.achievements[achievID - 1].collected) return;

        Instance.achievPanel.SetActive(true);
        Text[] texts = Instance.achievPanel.GetComponentsInChildren<Text>();
        texts[0].text = Instance.achievements[achievID].name;
        texts[1].text = Instance.achievements[achievID].description;

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

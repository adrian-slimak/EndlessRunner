using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject achievementsPanel;

    public void LoadLevelPreview()
    {
        SceneLoader.LoadLevel("LevelPreview");
    }

    public void LoadShop()
    {
        SceneLoader.LoadLevel("Shop");
    }

    public void LoadAchievements()
    {
        achievementsPanel.SetActive(true);
        achievementsPanel.GetComponent<Animator>().SetTrigger("Show");
    }

    public void LoadLeaderBoard()
    {
        SceneLoader.LoadLevel("Leaderboard");
    }

    public void LoadSettings()
    {
        settingsPanel.SetActive(true);
        settingsPanel.GetComponent<Animator>().SetTrigger("Show");
    }
}

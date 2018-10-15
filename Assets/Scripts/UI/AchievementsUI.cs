using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AchievementsUI : MonoBehaviour
{
    public GameObject allCat;
    public GameObject levelsCat;
    public GameObject staticticsCat;
    public GameObject trophyCat;

    public GameObject achievementPrefab;
    public Color unlockedColor;
    public Image currentBttn;

    private void Start()
    {
        GenerateAchievements(AchievementManager.Category.ALL, allCat.transform);
    }

    public void ShowAllCat()
    {
        allCat.SetActive(true);
        levelsCat.SetActive(false);
        staticticsCat.SetActive(false);
        trophyCat.SetActive(false);

        SetButton();

        if (allCat.transform.childCount == 0) GenerateAchievements(AchievementManager.Category.ALL, allCat.transform);
    }

    public void ShowLevelsCat()
    {
        allCat.SetActive(false);
        levelsCat.SetActive(true);
        staticticsCat.SetActive(false);
        trophyCat.SetActive(false);

        SetButton();

        if (levelsCat.transform.childCount == 0) GenerateAchievements(AchievementManager.Category.LEVELS, levelsCat.transform);
    }

    public void ShowStatisticsCat()
    {
        allCat.SetActive(false);
        levelsCat.SetActive(false);
        staticticsCat.SetActive(true);
        trophyCat.SetActive(false);

        SetButton();

        if (staticticsCat.transform.childCount == 0) GenerateAchievements(AchievementManager.Category.STATICTICS, staticticsCat.transform);
    }

    public void ShowTrophyCat()
    {
        allCat.SetActive(false);
        levelsCat.SetActive(false);
        staticticsCat.SetActive(false);
        trophyCat.SetActive(true);

        SetButton();

        if (trophyCat.transform.childCount == 0) GenerateAchievements(AchievementManager.Category.TROPHY, trophyCat.transform);
    }

    private void SetButton()
    {
        Image newBttn = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        Color color = currentBttn.color;
        currentBttn.color = newBttn.color;
        newBttn.color = color;
        currentBttn = newBttn;
    }

    private void GenerateAchievements(AchievementManager.Category category, Transform parent)
    {
        List<Achievement> achievements = AchievementManager.GetAchievements(category);

        foreach (Achievement achiev in achievements)
        {
            GameObject newAchievObj = Instantiate(achievementPrefab, parent) as GameObject;

            Text[] text = newAchievObj.GetComponentsInChildren<Text>();
            text[0].text = achiev.name;
            text[1].text = achiev.description;
            text[2].text = achiev.date;

            if (achiev.collected)
                newAchievObj.GetComponent<Image>().color = unlockedColor;
        }
    }

    public void LoadMenu()
    {
        SceneLoader.LoadLevel("Menu");
    }

    internal void Disable()
    {
        this.gameObject.SetActive(false);
    }
}

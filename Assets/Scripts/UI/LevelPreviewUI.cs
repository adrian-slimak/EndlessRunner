using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPreviewUI : MonoBehaviour
{
    List<GameObject> levels;
    private List<LevelInfo> levelsInfo;

    public GameObject levelHolder;
    public ScrollRectSnap scrollRect;
    public Color selectedColor;
    int levelSelected = 0;

    private void Start()
    {
        levels = new List<GameObject>();

        int children = levelHolder.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            levels.Add(levelHolder.transform.GetChild(i).gameObject);
            levels[i].GetComponentInChildren<Text>().text = LevelManager.GetLevelInfo(i+1).LevelName;
        }

        //foreach (LevelInfo level in LevelManager.GetLevels())
        //{
        //    GameObject newLevel = Instantiate(levelPrefab, levelHolder.transform);
        //    levels.Add(newLevel);
        //}
        levels[levelSelected].GetComponent<Image>().color = selectedColor;
    }

    public void LoadMenu()
    {
        SceneLoader.LoadLevel("Menu");
    }

    public void LevelClicked(int idx)
    {
        levels[levelSelected].GetComponent<Image>().color = levels[idx].GetComponent<Image>().color;
        levelSelected = idx;
        levels[levelSelected].GetComponent<Image>().color = selectedColor;
        scrollRect.SelectLevel(levelSelected);
    }
}

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
    public RectTransform selection;
    int levelSelected = 0;

    private void Start()
    {
        selection.SetParent(levelHolder.transform);

        levels = new List<GameObject>();

        int children = levelHolder.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            levels.Add(levelHolder.transform.GetChild(i).gameObject);
            if(levels[i].gameObject.activeSelf) // USUNĄĆ
            if(LevelManager.GetLevelInfo(i + 1) != null)
                levels[i].GetComponentInChildren<Text>().text = LevelManager.GetLevelInfo(i+1).LevelName;
        }

        Invoke("SetSelection", 0.1f);
        //foreach (LevelInfo level in LevelManager.GetLevels())
        //{
        //    GameObject newLevel = Instantiate(levelPrefab, levelHolder.transform);
        //    levels.Add(newLevel);
        //}
    }

    private void SetSelection()
    {
        selection.anchoredPosition = levels[levelSelected].GetComponent<RectTransform>().anchoredPosition;
    }

    public void LoadMenu()
    {
        SceneLoader.LoadLevel("Menu");
    }

    public void LevelClicked(int idx)
    {
        Color color = levels[idx].GetComponent<Image>().color;
        levels[idx].GetComponent<Image>().color = levels[levelSelected].GetComponent<Image>().color;
        levels[levelSelected].GetComponent<Image>().color = color;
        levelSelected = idx;
        scrollRect.SelectLevel(levelSelected);

        SetSelection();
    }
}

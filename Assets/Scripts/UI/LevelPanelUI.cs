using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanelUI : MonoBehaviour
{
    public int LevelID = -1;
    private LevelInfo levelInfo;

    public Text levelName;
    public Slider progressBar1;
    public Slider progressBar2;
    public Text progressText1;
    public Text progressText2;
    public Text pointsText;
    public Text jumpsText;
    public Text attemptsText;

    public GameObject stars;

    public Image coin1;
    public Image coin2;
    public Image coin3;

    public Sprite collectedShardSprite;
    public Color spriteColor;

    private void Start()
    {
        levelInfo = LevelManager.GetLevelInfo(LevelID);

        if (levelInfo.levelFinished)
        {
            stars.GetComponent<Image>().color = spriteColor;
            stars.GetComponentInChildren<Text>().text = ""+levelInfo.maxStars;
            stars.GetComponentInChildren<Text>().color = spriteColor;
        }

        levelName.text = levelInfo.LevelName;
        progressBar1.value = levelInfo.normalModeProgress;
        progressBar2.value = levelInfo.practiceModeProgress;
        progressText1.text = levelInfo.normalModeProgress + " %";
        progressText2.text = levelInfo.practiceModeProgress + " %";
        pointsText.text = levelInfo.pointsCollected + " / " + levelInfo.maxPoints;
        jumpsText.text = "Jumps: " + levelInfo.totalJumps;
        attemptsText.text = "Attempts: " + levelInfo.totalAttempts;
        //starsText.text = levelInfo.maxStars; // GWIAZDKI ZA UKOŃCZONY LEVEL

        if (levelInfo.shardsCollected.x == 1)
        { coin1.sprite = collectedShardSprite; coin1.color = spriteColor; coin1.SetNativeSize(); }
        if (levelInfo.shardsCollected.y == 1)
        { coin2.sprite = collectedShardSprite; coin2.color = spriteColor; coin2.SetNativeSize(); }
        if (levelInfo.shardsCollected.z == 1)
        { coin3.sprite = collectedShardSprite; coin3.color = spriteColor; coin3.SetNativeSize(); }
    }

    public void LoadLevel()
    {
        SceneLoader.LoadLevel("Level"+LevelID);
    }

    public void LoadLevelPractice()
    {
        SceneLoader.LoadLevel("Level" + LevelID + "_practice");
    }
}

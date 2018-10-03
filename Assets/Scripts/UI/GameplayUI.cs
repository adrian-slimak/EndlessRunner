using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public Text currentAttemptText;
    public GameObject gamePausePanel;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    public GameObject restartButton;

    public GameObject gameProgressBar;

	void Start ()
    {
        gameProgressBar.SetActive(GameSettings.Instance.showGameProgress);
        restartButton.SetActive(GameSettings.Instance.showRestartButton);
        currentAttemptText.text = "Attempt " + LevelController.Instance.totalAttempts;
    }

    public void ShowPausePanel()
    {
        LevelController.PauseGame();
        gamePausePanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        LevelController.PauseGame();
    }

    public void ShowLevelCompletePanel(int totalJumps, int totalAttempts, Vector3 hiddenCoinsCollected)
    {
        levelCompletePanel.SetActive(true);
        LevelController.PauseGame();
    }

    public void ExitLevel()
    {
        LevelController.ExitLevel();
    }

    public void RestartLevel()
    {
        LevelController.RestartLevelStatic();
    }
}

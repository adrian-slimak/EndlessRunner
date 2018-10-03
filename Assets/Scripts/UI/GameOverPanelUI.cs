using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelUI : MonoBehaviour
{
    public Slider progressBar;
    public Text text;
    public Text progressText;
    public Text attemptText;
    public Text jumpText;
    public Text timeText;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        text.text = LevelController.Instance.levelInfo.LevelName;

        float levelProgress = CameraController.Instance.transform.position.x * 100f / LevelController.Instance.levelLength;
        progressBar.value = levelProgress;
        progressText.text = (int)(levelProgress) + " %";

        attemptText.text = "Attempts: " + LevelController.Instance.totalAttempts;
        jumpText.text = "Jumps: " + LevelController.Instance.totalJumps;
        timeText.text = string.Format("Time: {0:00}:{1:00}",
                            (int)Time.timeSinceLevelLoad / 60,
                            (int)Time.timeSinceLevelLoad % 60);

        if (animator != null)
            animator.SetTrigger("Show");
    }

}

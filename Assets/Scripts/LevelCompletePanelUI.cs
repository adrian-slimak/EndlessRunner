using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanelUI : MonoBehaviour
{
    public Text attemptText;
    public Text jumpText;
    public Text timeText;
    public Image coin1;
    public Image coin2;
    public Image coin3;

    public Sprite collectedImageSprite;
    public Color spriteColor;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        attemptText.text = "Attempts: " + LevelController.Instance.totalAttempts;
        jumpText.text = "Jumps: " + LevelController.Instance.totalJumps;
        timeText.text = string.Format("Time: {0:00}:{1:00}",
                            (int)Time.timeSinceLevelLoad / 60,
                            (int)Time.timeSinceLevelLoad % 60);

        if (LevelController.Instance.hiddenShardsCollected.x == 1)
        { coin1.sprite = collectedImageSprite; coin1.color = spriteColor; coin1.SetNativeSize(); }
        if (LevelController.Instance.hiddenShardsCollected.y == 1)
        { coin2.sprite = collectedImageSprite; coin2.color = spriteColor; coin2.SetNativeSize(); }
        if (LevelController.Instance.hiddenShardsCollected.z == 1)
        { coin3.sprite = collectedImageSprite; coin3.color = spriteColor; coin3.SetNativeSize(); }

        if (animator != null)
            animator.SetTrigger("Show");
    }
}

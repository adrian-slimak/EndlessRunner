using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelUI : MonoBehaviour
{
    public Slider progressBar1;
    public Slider progressBar2;
    public Text text;
    public Text progressText1;
    public Text progressText2;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        text.text = LevelController.Instance.levelInfo.LevelName;

        progressBar1.value = LevelController.Instance.levelInfo.normalModeProgress;
        progressBar2.value = LevelController.Instance.levelInfo.practiceModeProgress;

        progressText1.text = LevelController.Instance.levelInfo.normalModeProgress + " %";
        progressText2.text = LevelController.Instance.levelInfo.practiceModeProgress + " %";

        if(animator!=null)
            animator.SetTrigger("Show");
    }

    internal void Disable()
    {
        LevelController.ResumeGame();
        this.gameObject.SetActive(false);
    }
}

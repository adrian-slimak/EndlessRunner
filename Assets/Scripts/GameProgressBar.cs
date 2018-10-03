using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressBar : MonoBehaviour
{
    public Text progressText;

    private Slider progressSlider;

    void Start()
    {
        progressSlider = GetComponent<Slider>();
    }

    void LateUpdate ()
    {
        float levelProgress = CameraController.Instance.transform.position.x * 100f / LevelController.Instance.levelLength;
        progressSlider.value = levelProgress;
        progressText.text = (int)(levelProgress) + " %";
	}
}

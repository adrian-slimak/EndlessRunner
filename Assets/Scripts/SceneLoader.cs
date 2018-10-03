using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    static SceneLoader Instance;

    private Animator animator;

    private string sceneToLoad;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public static void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public static void LoadLevel(string sceneName, bool withFade = true, float animSpeed = 1)
    {
        if (withFade)
        {
            Instance.animator.speed = animSpeed;
            Instance.sceneToLoad = sceneName;
            Instance.animator.SetTrigger("FadeOut");
        }
        else
            SceneManager.LoadScene(sceneName);
    }

    public void OnFadeComplete()
    {
        if (sceneToLoad.Equals("none")) return; 
        SceneManager.LoadScene(sceneToLoad);
        sceneToLoad = "none";
        animator.SetTrigger("FadeIn");
    }
}

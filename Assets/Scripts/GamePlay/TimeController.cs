using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;

    public Animator stopBttn;
    public Animator rewindBttn;

    [HideInInspector]
    public bool isRewinding;
    [HideInInspector]
    public bool isRecording = true;
    [HideInInspector]
    public bool timePaused = false;

    RunnerController player;
    Stack<Vector3> position;
    Stack<float> rotation;
    Stack<Vector3> velocity;
    Stack<float> angularVelocity;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = LevelController.Instance.Player;
        position = new Stack<Vector3>();
        rotation = new Stack<float>();
        velocity = new Stack<Vector3>();
        angularVelocity = new Stack<float>();
    }

    void FixedUpdate ()
    {
        if (isRewinding)
            RewindTime();
        else if(isRecording)
            RecordTime();
	}

    private void RecordTime()
    {
        position.Push(player._rb2d.position);
        rotation.Push(player._rb2d.rotation);
        velocity.Push(player._rb2d.velocity);
        angularVelocity.Push(player._rb2d.angularVelocity);
    }

    private void RewindTime()
    {
        if (!isRecording)
        { player.gameObject.SetActive(true); isRecording = true; }

        if (position.Count > 0)
        {
            PlayerStatsistics.Instance.totalTimeRewinded += Time.deltaTime;
            AchievementManager.CheckTimeRewindedAchiev();
            player._rb2d.position = position.Pop();
            player._rb2d.rotation = rotation.Pop();
            player._rb2d.velocity = velocity.Pop();
            player._rb2d.angularVelocity = angularVelocity.Pop();
        }
    }

    public static void PlayerChanged()
    {
        Instance.position = new Stack<Vector3>();
        Instance.rotation = new Stack<float>();
        Instance.velocity = new Stack<Vector3>();
        Instance.angularVelocity = new Stack<float>();

        Instance.player = LevelController.Instance.Player;
    }

    public static void PlayerDied()
    {
        Instance.isRecording = false;
        Instance.player.gameObject.SetActive(false);
    }

    public void RewindingTime()
    {
        rewindBttn.SetBool("Play", true);
        if (timePaused) Time.timeScale = 1;
        isRewinding = true;
        player._rb2d.isKinematic = true;
    }

    public void StopRewindingTime()
    {
        rewindBttn.SetBool("Play", false);
        if (timePaused) Time.timeScale = 0;
        isRewinding = false;
        player._rb2d.isKinematic = false;
    }

    public void StopResumeTime()
    {
        if (timePaused)
        {
            Time.timeScale = 1;
            timePaused = false;

            stopBttn.SetBool("Play", false);
        }
        else
        {
            Time.timeScale = 0;
            timePaused = true;

            stopBttn.SetBool("Play", true);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePortal : MonoBehaviour
{
    public enum Type { Cube, Ship, Circle, Bird, Arrow}

    public Type PortalType;

    public GameObject ShapeType;

    private RunnerController currentPlayer;
    private bool disabled = false;

    ParticleSystem particles;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void Update()
    {
        if (!particles.isPlaying && Mathf.Abs(transform.position.x - LevelController.Instance.Player.transform.position.x) < 20f)
            particles.Play();
    }

    private void TransformPlayer()
    {

        LevelController.Instance.Player = Instantiate(ShapeType, currentPlayer.transform.position, Quaternion.identity).GetComponent<RunnerController>();
        if (LevelController.Instance.practiceMode) TimeController.PlayerChanged();

        Destroy(currentPlayer.gameObject);
        particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (disabled) return;

        if (collision.tag == "Player")
        {
            disabled = true;
            currentPlayer = collision.gameObject.GetComponent<RunnerController>();
            TransformPlayer();
            CameraController.Instance.nextState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    public bool simpleTrigger = true;
    Quaternion rotation;
    Vector3 position;

    RunnerController RC;
    new ParticleSystem particleSystem;

    void Start()
    {
        rotation = transform.rotation;
        position = transform.parent.position - transform.position;
        RC = GetComponentInParent<RunnerController>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    void LateUpdate()
    {
        // FIX rotation and position
        transform.rotation = rotation;
        transform.position = transform.parent.position - position;

        if (simpleTrigger)
            if (RC.isGrounded)
            {
                if (!particleSystem.isPlaying)
                    particleSystem.Play();
            }
            else particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!particleSystem.isPlaying)
            particleSystem.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!particleSystem.isPlaying)
            particleSystem.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }
}

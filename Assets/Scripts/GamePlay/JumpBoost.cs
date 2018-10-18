using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    public float boostForce = 3f;

    ParticleSystem particles;

    private void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - LevelController.Instance.Player.transform.position.x) < 20f)
        {
            if (!particles.isPlaying)
                particles.Play();
        }
        else
        if (particles.isPlaying) particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Rigidbody2D rb2d = collision.GetComponent<Rigidbody2D>();
            rb2d.velocity = new Vector2(rb2d.velocity.x, boostForce);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hiddenShard : MonoBehaviour
{

    public int shardNumber = 1;

    ParticleSystem particles;

    void Start ()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
	

	void Update ()
    {
        if (!particles.isPlaying && Mathf.Abs(transform.position.x - LevelController.Instance.Player.transform.position.x) < 25f)
            particles.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LevelController.HiddenShardCollected(shardNumber);
            //particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            StartCoroutine(Disappear(0.8f));
        }
    }

    private IEnumerator Disappear(float time)
    {
        while (time > 0)
        {
            particles.transform.localScale = particles.transform.localScale + new Vector3(0.05f,0,0.05f);
            time -= 0.02f;
            yield return new WaitForSeconds(0.02f);
        }

        Destroy(gameObject);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}

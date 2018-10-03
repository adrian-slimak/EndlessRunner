using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour // POPRAWI OnClick i OnPressed
{
    protected Rigidbody2D _rb2d;

    public bool isGrounded = true;
    public float speedX = 10f;
    public GameObject burstParticle;
    
    protected void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.velocity = new Vector2(speedX, 0f);
    }

    protected void Update()
    {
        if (_rb2d.velocity.x < 0.1) gameOver();

        float speedY = _rb2d.velocity.y;
        _rb2d.velocity = new Vector2(speedX, speedY);

        if(Input.touchCount > 0)
            onClick();

        if (Input.GetKeyDown(KeyCode.Space))
            //if (Input.GetTouch(0).phase == TouchPhase.Began)
            onClick();

        if (Input.GetKeyDown(KeyCode.R)) LevelController.PlayerDied();
    }

    protected virtual void onClick() { LevelController.PlayerJumped(); }

    private void gameOver()
    {
        LevelController.PlayerDied();
        Instantiate(burstParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike") gameOver();
    }

    public Vector3 getVelocity()
    {
        return _rb2d.velocity;
    }

    public void setVelocity(Vector3 newVelocity)
    {
        _rb2d.velocity = newVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = false;

    }
}

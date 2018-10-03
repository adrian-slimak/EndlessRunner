using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMechanic : RunnerController
{
    public float maxSpeedY = 10f;
    public float accelerationY = 1f;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();

        float speedY = _rb2d.velocity.y + accelerationY;
        speedY = Mathf.Clamp(speedY, -maxSpeedY, maxSpeedY);
        _rb2d.velocity = new Vector2(speedX, speedY);

        transform.rotation = Quaternion.Euler(0, 0, speedY/maxSpeedY * 45 - 45);
    }

    override
    protected void onClick()
    {
        base.onClick();
        accelerationY *= -1;
    }
}

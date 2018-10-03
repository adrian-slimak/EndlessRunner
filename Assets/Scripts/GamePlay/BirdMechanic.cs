using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMechanic : RunnerController
{
    public float jumpForce = 25f;
    public float maxTilt = 13f;
    public float tiltFactor = -0.5f;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();

        float tilt = tiltFactor * _rb2d.velocity.y;
        tilt = Mathf.Clamp(tilt, -maxTilt, 0);
        transform.rotation = Quaternion.Euler(0, 0, tilt);

        if(isGrounded)
            transform.rotation = Quaternion.Euler(0, 0, tilt);
    }

    override
    protected void onClick()
    {
        base.onClick();
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpForce);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMechanic : RunnerController
{
    public float jumpForce = 5f;
    public float jumpRotationForce = 0f;

    new void Start ()
    {
        base.Start();
	}

    new void Update ()
    {
        base.Update();
	}

    override
    protected void onClick()
    {
        base.onClick();
        if (isGrounded)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpForce);
            _rb2d.angularVelocity = jumpRotationForce;
        }
    }
}

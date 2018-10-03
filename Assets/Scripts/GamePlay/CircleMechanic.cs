using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMechanic : RunnerController
{
    public float angularDragInAir = 150f;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();

        if (isGrounded)
            _rb2d.angularDrag = 0;
        else
            _rb2d.angularDrag = angularDragInAir;
    }

    override
    protected void onClick()
    {
        base.onClick();

        if (isGrounded)
        {
            _rb2d.gravityScale = -_rb2d.gravityScale;
        }
    }
}

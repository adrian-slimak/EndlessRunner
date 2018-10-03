using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMechanic : RunnerController
{
    public float maxTilt;
    public float flyAcceletarion;
    public float maxFlyVelocity;
    public float maxFallVelocity;
    public float tiltFactor;

    private bool Flying;

	// Use this for initialization
	new void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	new void Update ()
    {
        base.Update();

        float tilt = tiltFactor * (_rb2d.velocity.y - 1); // -1, because max fall velocity is less than max fly velocity
        tilt = Mathf.Clamp(tilt, -maxTilt, maxTilt);
        transform.rotation = Quaternion.Euler(0, 0, tilt);
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0)
        {
            _rb2d.AddForce(Vector2.up * flyAcceletarion);
        }

        float newVelocity = Mathf.Clamp(_rb2d.velocity.y, -maxFallVelocity, maxFlyVelocity);

        _rb2d.velocity = new Vector2(_rb2d.velocity.x, newVelocity);
    }
}

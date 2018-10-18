using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public float rotationSpeed = 0.1f;
    public Vector3 speed = new Vector3(0, 0.1f, 0);

	void Update ()
    {
        if (Mathf.Abs(transform.position.x - LevelController.Instance.Player.transform.position.x) > 25f) return;

        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        transform.Translate(speed * Time.deltaTime, Space.World);
    }
}

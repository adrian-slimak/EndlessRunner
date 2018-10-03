using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public List<Vector2> positions;
    GameObject player;
    int currentPosition = 0;

	void Start ()
    {
		player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void FixedUpdate ()
    {
        if(player == null) FindPlayer();
        if (player.transform.position.x >= positions[currentPosition + 1].x) currentPosition++;

        transform.position = new Vector2(
            transform.position.x,
            Mathf.Lerp(transform.position.y, positions[currentPosition].y, Time.deltaTime * 1.5f)
            );
	}

    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}

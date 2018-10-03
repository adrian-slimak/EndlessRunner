﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraMaxDistY = 4f;
    public float cameraOffsetX = 3f;
    public float cameraFollowYSpeed = 3f;
    public float cameraStopFollow = 900f;

    GameObject playerToFollow;
    float cameraPosY = 1f;

	void Start ()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        QualitySettings.antiAliasing = 1;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        cameraPosY = this.transform.position.y;
        playerToFollow = LevelController.Instance.Player.gameObject;
	}

	void LateUpdate ()
    {
        if (playerToFollow == null) playerToFollow = LevelController.Instance.Player.gameObject;
        if (playerToFollow.transform.position.x < -3 || playerToFollow.transform.position.x > cameraStopFollow) return;

        float distY = playerToFollow.gameObject.transform.position.y - cameraPosY;
        if (distY > cameraMaxDistY) cameraPosY++;
        if (distY < -cameraMaxDistY) cameraPosY--;
        transform.position = new Vector3(playerToFollow.transform.position.x + cameraOffsetX,
                                         transform.position.y+(cameraPosY - transform.position.y)*Time.deltaTime * cameraFollowYSpeed,
                                         -10);
	}

    public void setCameraProperties(float cameraY, float cameraMaxY)
    {
        cameraPosY = cameraY;
        cameraMaxDistY = cameraMaxY;
    }
}

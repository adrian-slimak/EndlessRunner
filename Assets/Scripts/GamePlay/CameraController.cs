using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public List<Vector3> cameraStates;

    private Camera _camera;
    private CameraFollow _cameraFollow;
    private int currentState = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    void Start ()
    {
        _camera = GetComponent<Camera>();
        _cameraFollow = GetComponent<CameraFollow>();
        _cameraFollow.setCameraProperties(cameraStates[0].x, cameraStates[0].y);
        _camera.orthographicSize = cameraStates[0].z;
    }

    public void nextState()
    {
        currentState++;
        _cameraFollow.setCameraProperties(cameraStates[currentState].x, cameraStates[currentState].y);
        _camera.orthographicSize = cameraStates[currentState].z;
    }
}

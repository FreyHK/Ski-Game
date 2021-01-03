using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoomer : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    Camera cam;
    float minSize = 15f;
    float maxSize = 30f;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = minSize;
    }

    void Update()
    {
        float target = Mathf.Clamp(7.5f + 5f * playerController.SpeedScale, minSize, maxSize);
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, target, Time.deltaTime * 10f);
    }
}

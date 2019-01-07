using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    float Speed = 6f;

    void Awake () {
        //Offset
        Vector3 offset = transform.right * Speed * GameSpeedController.SpeedScale;
        transform.position += offset;
    }

    private void Update() {
        if (GameManager.State == GameState.InMenu)
            return;

        transform.position += -transform.right * Speed * GameSpeedController.SpeedScale *  Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes game go faster over time.
/// </summary>
public class GameSpeedController : MonoBehaviour
{
    public PlayerController playerController;
    public ObstacleSpawner obstacleSpawner;

    float speed = 1.5f;

    void Update()
    {
        if (GameManager.State != GameState.InGame)
            return;
        
        speed += Time.deltaTime * .05f;
        //print("Speed: " + speed);

        playerController.SpeedScale = speed;
        obstacleSpawner.SpeedScale = speed;
    }
}

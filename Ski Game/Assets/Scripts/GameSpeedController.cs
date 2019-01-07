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

    public static float SpeedScale { get; private set; }

    private void Awake() {
        SpeedScale = 1.5f;
    }

    void Update()
    {
        if (GameManager.State != GameState.InGame)
            return;

        SpeedScale += Time.deltaTime * .1f; //.05f;
        //print("Speed: " + speed);

        playerController.SpeedScale = SpeedScale;
        obstacleSpawner.SpeedScale = SpeedScale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    InMenu,
    InGame,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameState State;

    public PlayerController playerController;
    public Rigidbody2D playerBody;

    void Start() {
        PlayerCollision.OnPlayerDied += OnPlayerDied;

        State = GameState.InMenu;
        playerBody.isKinematic = true;
        playerController.StopMoving();
    }

    private void OnDestroy() {
        PlayerCollision.OnPlayerDied -= OnPlayerDied;
    }

    public void StartGame() {
        State = GameState.InGame;

        Invoke("ActivatePlayer", .1f);
    }

    /// <summary>
    /// Bad fix for player jumping on the frame you press start.
    /// </summary>
    void ActivatePlayer()
    {
        playerBody.isKinematic = false;
        playerController.StartMoving();
    }

    void OnPlayerDied() {
        State = GameState.GameOver;

        //playerBody.isKinematic = true;
        playerController.StopMoving();
    }
}

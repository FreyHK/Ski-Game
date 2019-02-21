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
        //We can only do this IF we are in menu
        if (State != GameState.InMenu)
            return;

        State = GameState.InGame;
        // Bad fix for player jumping on the frame you press start.
        Invoke("ActivatePlayer", .1f);
    }

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

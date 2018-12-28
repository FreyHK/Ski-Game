﻿using System.Collections;
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

    void Start()
    {
        PlayerCollision.OnPlayerDied += OnPlayerDied;

        State = GameState.InMenu;
        playerBody.isKinematic = true;
        playerController.IsFrozen = true;
    }
    
    public void StartGame() {
        State = GameState.InGame;

        playerBody.isKinematic = false;
        playerController.IsFrozen = false;
    }

    void OnPlayerDied() {
        State = GameState.GameOver;
        //playerBody.isKinematic = true;
        //playerController.IsFrozen = true;
        //TODO: show menu
    }
}
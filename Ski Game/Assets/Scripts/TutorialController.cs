﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public PlayerController PlayerController;

    void Start()
    {
        PlayerCollision.OnPlayerDied += OnPlayerDied;

        PlayerController.StartMoving();
    }

    private void OnDestroy() {
        PlayerCollision.OnPlayerDied -= OnPlayerDied;
    }

    void OnPlayerDied()
    {
        //Load tutorial again
        GameInitializer.Instance.Restart(true);
    }

    public void OnPlayerReachedEnd() {
        //Save that player completed tutorial

        //Load main game
        GameInitializer.Instance.Restart(false);
    }
}
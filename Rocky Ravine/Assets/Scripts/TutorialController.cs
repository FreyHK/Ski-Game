using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public PlayerController PlayerController;

    void Start()
    {
        PlayerCollision.OnHitObstacle += OnPlayerDied;

        PlayerController.StartMoving();
    }

    private void OnDestroy() {
        PlayerCollision.OnHitObstacle -= OnPlayerDied;
    }

    void OnPlayerDied()
    {
        //Load tutorial again
        GameInitializer.Instance.Restart(true);
    }

    public void OnPlayerReachedEnd() {
        //Save that player completed tutorial
        PlayerPrefs.SetInt("CompletedTutorial", 1);

        //Load main game
        GameInitializer.Instance.Restart(false);
    }
}

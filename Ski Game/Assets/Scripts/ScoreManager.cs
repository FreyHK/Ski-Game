using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        //PlayerPrefs.SetInt("HighScore", 0);

        PlayerCollision.OnPlayerDied += ValidateScore;
        //Highscore is saved as integer (divide by 10 to get .0)
        HighScore = PlayerPrefs.GetInt("HighScore", 0);

        CurrentScore = 0f;
        scoreText.text = "";
    }

    //Called when game restarts (on scene load), or when player exits app
    private void OnDestroy() {
        //Unsubscribe
        PlayerCollision.OnPlayerDied -= ValidateScore;

        //Save highscore to integer (x10 to save .0)
        PlayerPrefs.SetInt("HighScore", HighScore);
    }

    public static int HighScore { get; private set; }
    public static float CurrentScore { get; private set; }

    void Update()
    {
        if (GameManager.State == GameState.InGame) {
            CurrentScore += playerController.Speed * playerController.SpeedScale * Time.deltaTime / 2f;
            
            scoreText.text = TextFormatter.GetDistance(Mathf.FloorToInt(CurrentScore));
        }
        //float s = -playerTransform.position.y/2f;
        //if (s > CurrentScore) {
        //    CurrentScore = s;
        //}
    }

    //Triggered when round ends
    void ValidateScore() {
        if (CurrentScore > HighScore) {
            HighScore = Mathf.FloorToInt(CurrentScore);
            
            //TODO: callback
        }
    }
}

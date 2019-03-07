using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    void Start()
    {
        PlayerCollision.OnHitObstacle += ValidateScore;
        //Highscore is saved as integer (divide by 10 to get .0)
        HighScore = PlayerPrefs.GetInt("HighScore", 0);

        CurrentScore = 0f;
        scoreText.text = "";
        if (HighScore == 0)
            highscoreText.text = "";
        else
            highscoreText.text = "High: " + TextFormatter.GetDistance(HighScore);
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        HighScore = 0;
        highscoreText.text = "";
    }

    //Called when game restarts (on scene load), or when player exits app
    private void OnDestroy() {
        //Unsubscribe
        PlayerCollision.OnHitObstacle -= ValidateScore;

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
    }

    //Triggered when round ends
    void ValidateScore() {
        if (CurrentScore > HighScore) {
            HighScore = Mathf.FloorToInt(CurrentScore);
            
            //TODO: callback
        }
    }
}

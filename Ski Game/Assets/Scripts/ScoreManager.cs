using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Transform playerTransform;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        //PlayerPrefs.SetInt("HighScore", 0);

        PlayerCollision.OnPlayerDied += ValidateScore;
        //Highscore is saved as integer (divide by 10 to get .0)
        HighScore = (float)PlayerPrefs.GetInt("HighScore", 0)/10f;

        CurrentScore = 0;
        scoreText.text = "";
    }

    public static float HighScore { get; private set; }
    public static float CurrentScore { get; private set; }

    void Update()
    {
        float s = -playerTransform.position.y/2f;
        if (s > CurrentScore) {
            CurrentScore = s;
            scoreText.text = CurrentScore.ToString("0.0");
        }
    }

    //Triggered when round ends
    void ValidateScore() {
        if (CurrentScore > HighScore) {
            HighScore = CurrentScore;
        }
    }

    //Called when game restarts (on scene load), or when player exits app
    private void OnDestroy() {
        //Save highscore to integer (x10 to save .0)
        PlayerPrefs.SetInt("HighScore", Mathf.FloorToInt(HighScore * 10f));
    }
}

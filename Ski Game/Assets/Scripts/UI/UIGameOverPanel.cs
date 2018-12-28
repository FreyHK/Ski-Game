using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOverPanel : MonoBehaviour
{
    public Animator anim;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI scoreText;

    void Start() {
        PlayerCollision.OnPlayerDied += Open;
    }

    void Open() {
        if (anim != null)
            anim.SetTrigger("Open");

        scoreText.text = "You descended " + ScoreManager.CurrentScore.ToString("0.0") + "m.";
        highscoreText.text = "Best: " + ScoreManager.HighScore.ToString("0.0") + "m";
    }
}

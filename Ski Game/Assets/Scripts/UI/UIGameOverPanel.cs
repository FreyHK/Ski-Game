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

    private void OnDestroy() {
        PlayerCollision.OnPlayerDied -= Open;
    }

    void Open() {
        if (anim != null)
            anim.SetTrigger("Open");

        scoreText.text = "Descended\n" + TextFormatter.GetDistance(Mathf.FloorToInt(ScoreManager.CurrentScore));
            //ScoreManager.CurrentScore.ToString("0.0") + "m";
        highscoreText.text = "Best\n" + TextFormatter.GetDistance(ScoreManager.HighScore);
            //ScoreManager.HighScore.ToString("0.0") + "m";
    }

    //Called by UI button
    public void Restart()
    {
        GameInitializer.Instance.Restart();
    }
}

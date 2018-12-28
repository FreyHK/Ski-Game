using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    private void Start()
    {
        //PlayerCollision.OnPlayerDied += Restart;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class belongs to persistent scene
/// </summary>
public class GameRestarter : MonoBehaviour
{
    public static GameRestarter Instance;

    public Image overlayImage;
    public AdManager adManager;

    private void Start()
    {
        Instance = this;

        Color c = overlayImage.color;
        c.a = 0f;
        overlayImage.color = c;

        if (SceneManager.sceneCount == 1)
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void Restart()
    {
        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene()
    {
        //Fade 
        Color c = overlayImage.color;
        float t = 0f;
        while (t < 1f)
        {
            c.a = t;
            overlayImage.color = c;
            t += Time.deltaTime * 2f;
            yield return null;
        }

        //Unload old scene
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(1);
        //Load new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);


        //We can show ad while we wait.
        adManager.TryShowAd();

        //Wait for both to finish
        while (!asyncLoad.isDone || !asyncUnload.isDone)
        {
            yield return null;
        }

        //Fade
        t = 0f;
        while (t < 1f)
        {
            c.a = 1 - t;
            overlayImage.color = c;
            t += Time.deltaTime * 2f;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class belongs to persistent scene
/// </summary>
public class GameInitializer : MonoBehaviour
{
    public static GameInitializer Instance;

    public Image overlayImage;
    public AdManager adManager;

    Scene loadedScene;

    LoadSceneParameters loadParameters = new LoadSceneParameters(LoadSceneMode.Additive);

    private void Start() {
        Instance = this;

        Color c = overlayImage.color;
        c.a = 0f;
        overlayImage.color = c;

        bool playTutorial = true;

        if (playTutorial) {
            //Load tutorial
            if (SceneManager.sceneCount == 1)
                loadedScene = SceneManager.LoadScene(2, loadParameters);
        } else {
            //Only load mainScene if we haven't already (used in editor)
            if (SceneManager.sceneCount == 1)
                loadedScene = SceneManager.LoadScene(1, loadParameters);
        }
    }

    public void Restart(bool loadTutorial = false)
    {
        int index = loadTutorial ? 2 : 1;
        StartCoroutine(ReloadScene(index));
    }

    IEnumerator ReloadScene(int buildIndex)
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
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(loadedScene);
        //Load new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex, loadParameters);

        //We can show ad while we wait.
        adManager.TryShowAd();

        //Wait for both to finish
        while (!asyncLoad.isDone || !asyncUnload.isDone)
        {
            yield return null;
        }
        //Remember loaded scene (used when unloading)
        loadedScene = SceneManager.GetSceneAt(1);

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

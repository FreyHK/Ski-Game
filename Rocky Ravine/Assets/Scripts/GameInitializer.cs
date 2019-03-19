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

        //Hide overlayimage
        Color c = overlayImage.color;
        c.a = 0f;
        overlayImage.color = c;

        if (SceneManager.sceneCount == 1)
        {
            //Scene 1 = main, 2 = tutorial
            int index = PlayerPrefs.HasKey("CompletedTutorial") ? 1 : 2;

            LoadScene(index, true);
        }
        else
            //Find the level, that we loaded in from editor.
            loadedScene = SceneManager.GetSceneAt(1);
    }

    public void LoadScene(int buildIndex, bool instant = false)
    {
        //Don't load if we are already
        if (IsLoading)
            return;

        if (instant)
        {
            loadedScene = SceneManager.LoadScene(buildIndex, loadParameters);
            //Remember loaded scene (used when unloading)
            loadedScene = SceneManager.GetSceneAt(1);

            return;
        }
        //Load with fading animations
        StartCoroutine(LoadSceneAsync(buildIndex));
    }

    public bool IsLoading { get; private set; }

    IEnumerator LoadSceneAsync(int buildIndex)
    {
        IsLoading = true;

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

        //Wait for both to finish
        while (!asyncLoad.isDone || !asyncUnload.isDone)
        {
            yield return null;
        }
        //Remember loaded scene (used when unloading)
        loadedScene = SceneManager.GetSceneAt(1);

        //Try and show ad
        adManager.TryShowAd();

        //Reset flag
        IsLoading = false;

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

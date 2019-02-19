using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    const string AndroidGameId = "3049309";
    const string IOSGameId = "3049308";

    //In seconds
    const int AdTimerDuration = 45;
    float adTimer;

    void Awake()
    {

        if (Application.platform == RuntimePlatform.Android)
            Advertisement.Initialize(AndroidGameId, true);
        else
            Advertisement.Initialize(IOSGameId, true);

        adTimer = (float)PlayerPrefs.GetInt("AdTimer", AdTimerDuration);
    }

    private void Update()
    {
        adTimer -= Time.deltaTime;
    }

    public void TryShowAd()
    {
        if (Advertisement.IsReady() && adTimer <= 0f)
        {
            adTimer = AdTimerDuration;
            Advertisement.Show();
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("AdTimer", (int)adTimer);
    }
}

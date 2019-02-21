using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    #if UNITY_ANDROID
    const string gameId = "3049309";
    #elif UNITY_IOS
    const string gameId = "3049308";
    #endif

    //In seconds
    const int AdTimerDuration = 60;
    float adTimer;

    void Awake()
    {
        if (Advertisement.isSupported)
            Advertisement.Initialize(gameId, true);

        //Debugging
        print("ADINFO - IsInitialized: " + Advertisement.isInitialized + ", IsReady: " + Advertisement.IsReady() + ", Timer: " + adTimer.ToString());

        adTimer = (float)PlayerPrefs.GetInt("AdTimer", AdTimerDuration);
    }

    private void Update()
    {
        adTimer -= Time.deltaTime;
    }

    public void TryShowAd()
    {
        print("ADINFO - IsInitialized: " + Advertisement.isInitialized + ", IsReady: " + Advertisement.IsReady() + ", Timer: " + adTimer.ToString());
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

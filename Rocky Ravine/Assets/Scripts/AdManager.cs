using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AdManager : MonoBehaviour
{

    public bool TestMode = false;

    //In seconds
    const int AdTimerDuration = 60;
    float adTimer;


    void Awake()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { print("Admob Initialized."); RequestInterstitial(); });


        adTimer = PlayerPrefs.GetInt("AdTimer", AdTimerDuration);
    }

    private void Update()
    {
        adTimer -= Time.deltaTime;
    }

    InterstitialAd interstitial;

    private void RequestInterstitial()
    {
        print("RequestInterstitial");


#if UNITY_ANDROID
        string adUnitId = TestMode ? "ca-app-pub-3940256099942544/1033173712" : "ca-app-pub-9363144461440233/6487327625";
#elif UNITY_IPHONE
        string adUnitId = TestMode ? "ca-app-pub-3940256099942544/4411468910" : "ca-app-pub-9363144461440233/1430442068";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdEnded;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdEnded;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    void HandleOnAdEnded (object sender, EventArgs args)
    {
        print("HandleOnAdEnded");

        //Clean up referece to avoid memory leak
        interstitial.Destroy();

        //Reset timer
        adTimer = AdTimerDuration;

        //Unpause game (AdMob doesn't do this)
        Time.timeScale = 1f;

        RequestInterstitial();
    }

    void HandleOnAdFailedToLoad (object sender, AdFailedToLoadEventArgs args)
    {
        print("Interstitial failed to load: " + args.Message);

        //Clean up referece to avoid memory leak
        interstitial.Destroy();

        //Try again
        RequestInterstitial();
    }


    public void TryShowAd()
    {
        print("TryShowAd - Ad Timer: " + adTimer);
        if (this.interstitial.IsLoaded() && adTimer <= 0f)
        {
            this.interstitial.Show();
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("AdTimer", (int)adTimer);
    }
    
}

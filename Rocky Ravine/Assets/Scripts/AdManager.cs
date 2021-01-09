using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;

public class AdManager : MonoBehaviour
{
    public bool TestMode = false;

    //In seconds
    const int AdCooldown = 60;
    const int ErrorCooldown = 20;


    float curAdTimer;
    bool requestingAd = false;

    void Awake()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { print("Admob Initialized.");});

        curAdTimer = PlayerPrefs.GetInt("AdTimer", AdCooldown);
    }
    
    private void Update()
    {
        curAdTimer -= Time.deltaTime;
        if (curAdTimer <= 0f && !requestingAd) {
            RequestInterstitial();
        }
    }

    InterstitialAd interstitial;

    private void RequestInterstitial()
    {
        requestingAd = true;
        print("RequestInterstitial");

#if UNITY_ANDROID
        string adUnitId = TestMode ? "ca-app-pub-3940256099942544/1033173712" : "ca-app-pub-9363144461440233/6487327625";
#elif UNITY_IPHONE
        string adUnitId = TestMode ? "ca-app-pub-3940256099942544/4411468910" : "ca-app-pub-9363144461440233/1430442068";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdEnded;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    void HandleOnAdOpening (object sender, EventArgs args) {
#if UNITY_EDITOR
        Time.timeScale = 1f;
#endif
    }

    void HandleOnAdEnded (object sender, EventArgs args)
    {
        requestingAd = false;
        print("HandleOnAdEnded");

        //Clean up reference to avoid memory leak
        interstitial.Destroy();
        interstitial = null;

        //Unpause game (AdMob doesn't do this)
        Time.timeScale = 1f;

        //Reset timer
        curAdTimer = AdCooldown;
    }

    void HandleOnAdFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        requestingAd = false;
        print("Interstitial failed to load: " + args.Message);

        //Clean up referece to avoid memory leak
        interstitial.Destroy();
        interstitial = null;

        //Try again (after a few seconds)
        curAdTimer = ErrorCooldown;
    }
    

    public void TryShowAd()
    {
        
        print("TryShowAd - Ad Timer: " + curAdTimer);
        if (interstitial != null && interstitial.IsLoaded() && curAdTimer <= 0f)
        {
            interstitial.Show();
        }
        
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("AdTimer", (int)curAdTimer);
    }
    
}

using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    //In seconds
    const int AdCooldown = 120;

    float curAdTimer;

#if UNITY_ANDROID
   //Android ID:
   string gameId = "3049309";
#else 
    //IOS ID:
    string gameId = "3049308";
#endif

    string placementID = "video";
    bool testMode = false;

    void Awake()
    {
        curAdTimer = PlayerPrefs.GetInt("AdTimer", AdCooldown);
        Advertisement.Initialize(gameId, testMode);
    }
    void OnDestroy() {
        PlayerPrefs.SetInt("AdTimer", (int)curAdTimer);
    }

    void Update()
    {
        curAdTimer -= Time.deltaTime;
    }

    public void TryShowAd()
    {
        print("TryShowAd - Ad Timer: " + curAdTimer);
        
        if (curAdTimer <= 0f && Advertisement.IsReady(placementID)) {
            Advertisement.Show(placementID);
            curAdTimer = AdCooldown;
        }
    }
}

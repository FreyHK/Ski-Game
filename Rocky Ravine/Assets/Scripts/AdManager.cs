using UnityEngine.Events;
using UnityEngine;
using GleyMobileAds;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public GameObject GDPRConsentPanel;

    //In seconds
    const int AdCooldown = 60;

    float curAdTimer;
    bool initialized = false;

    void Awake()
    {
        curAdTimer = PlayerPrefs.GetInt("AdTimer", AdCooldown);

        //Show consent popup
        if (Advertisements.Instance.UserConsentWasSet() == false)
        {
            //Ask user for GDPR consent
            ShowAdConsentSettings();
        }
        else
        {
            //User already gave conscent, just initialize
            InitAds();
        }
    }

    public void ShowAdConsentSettings ()
    {
        Time.timeScale = 0f;
        GDPRConsentPanel.SetActive(true);
    }

    public void SetConsent (bool v)
    {
        Time.timeScale = 1f;
        Advertisements.Instance.SetUserConsent(v);
        if (!initialized)
        {
            InitAds();
        }
    }

    void InitAds ()
    {
        Advertisements.Instance.Initialize();
        initialized = true;
    }

    private void Update()
    {
        curAdTimer -= Time.deltaTime;
    }

    public void TryShowAd()
    {
        
        print("TryShowAd - Ad Timer: " + curAdTimer);
        
        if (curAdTimer <= 0f)
        {
            Advertisements.Instance.ShowInterstitial(() => { curAdTimer = AdCooldown; });
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("AdTimer", (int)curAdTimer);
    }
    
}

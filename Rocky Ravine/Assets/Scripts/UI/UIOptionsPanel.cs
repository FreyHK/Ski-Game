using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIOptionsPanel : MonoBehaviour
{
    public Animator anim;

    bool isOpen = false;

    float cooldown;

    private void Update() {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    public void Toggle() {
        //Make sure player can't spam this
        if (cooldown > 0)
            return;

        isOpen = !isOpen;
        anim.SetBool("IsOpen", isOpen);
        //Set cooldown
        cooldown = .5f;
    }

    public TextMeshProUGUI soundButtonText;

    private void Start()
    {
        soundButtonText.text = "SOUND " + (AudioManager.IsGloballyMuted ? "OFF" : "ON");
    }

    public void ToggleSound()
    {
        bool v = AudioManager.IsGloballyMuted;
        //Toggle in game sounds on/off
        AudioManager.Instance.SetSoundMuted(!v);
        //Update display
        soundButtonText.text = "SOUND " + (AudioManager.IsGloballyMuted ? "OFF" : "ON");
    }

    public void OnHowToPlay()
    {
        GameInitializer.Instance.LoadScene(2);
    }

    public void OpenAdSettings()
    {
        FindObjectOfType<AdManager>().ShowAdConsentSettings();
    }
}

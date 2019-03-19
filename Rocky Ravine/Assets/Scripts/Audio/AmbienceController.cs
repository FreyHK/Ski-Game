using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.Play("Seagulls");
        AudioManager.Instance.Play("Wind");
    }

    void OnDestroy()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Stop("Seagulls");
            AudioManager.Instance.Stop("Wind");
        }
    }
}

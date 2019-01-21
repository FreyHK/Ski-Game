using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopResolution : MonoBehaviour
{
    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || 
            Application.platform == RuntimePlatform.OSXPlayer)
        {

            float aspect = 2f / 3f;
            int width = Mathf.FloorToInt(Screen.height * aspect);
            Screen.SetResolution(width, Screen.height, true);
        }
    }
}

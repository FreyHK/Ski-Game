using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotCreator : MonoBehaviour {

    [SerializeField] int ScaleFactor = 1;

    void LateUpdate() {
        if (Input.GetKeyDown("k")) {

            string fileName = string.Format("{0}/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             Screen.width*ScaleFactor, Screen.height*ScaleFactor,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

            ScreenCapture.CaptureScreenshot(fileName, ScaleFactor);

            Debug.Log(string.Format("Took screenshot to: {0}", fileName));
        }
    }
}

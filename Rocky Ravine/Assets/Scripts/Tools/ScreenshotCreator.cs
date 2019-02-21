using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotCreator : MonoBehaviour {

    private void Awake() {
        Screen.SetResolution(1242, 2208, false);
    }

    void LateUpdate() {
        if (Input.GetKeyDown("k")) {

            string fileName = string.Format("{0}/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             Screen.width, Screen.height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

            ScreenCapture.CaptureScreenshot(fileName);

            Debug.Log(string.Format("Took screenshot to: {0}", fileName));
        }
    }
}

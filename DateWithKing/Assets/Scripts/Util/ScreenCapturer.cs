using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ScreenCapturer : Singleton<ScreenCapturer>
{
    string screenshotPath;
    void Start()
    {
        screenshotPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Wait!Misonyeo");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!Directory.Exists(screenshotPath))
            {
                Directory.CreateDirectory(screenshotPath);
            }
            CaptureScreen();
        }
    }
    public void CaptureScreen()
    {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
        string fileName = timestamp + ".png";
        string fullPath = Path.Combine(screenshotPath, fileName);
        
        ScreenCapture.CaptureScreenshot(fullPath);
        Debug.Log("스크린샷 저장: " + fullPath);
    }
}

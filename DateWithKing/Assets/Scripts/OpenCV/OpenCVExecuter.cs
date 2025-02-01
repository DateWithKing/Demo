using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class OpenCVExecuter : MonoBehaviour
{
    string exePath = System.IO.Path.Combine(Application.streamingAssetsPath, "handShaking/handShaking.exe");
    void Start()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = exePath,  // PyInstaller로 빌드한 실행 파일 경로
            Arguments = "",  // 스크립트 인수가 있다면 여기에 추가
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        string output = process.StandardOutput.ReadLine();
        print(output);
    }
}

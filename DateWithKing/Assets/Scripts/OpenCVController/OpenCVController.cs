using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = System.Diagnostics.Debug;

/// <summary>
/// How To Use? <br/>
/// OpenCVController.Instance.InvokeDetector
/// </summary>
public class OpenCVController : Singleton<OpenCVController>
{
    private Process pythonProcess;
    private StreamWriter writer;
    private readonly string exePath = Application.streamingAssetsPath + "/Detector.exe";
    void Awake()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = exePath,  // PyInstaller로 빌드한 실행 파일 경로
            Arguments = "",  // 스크립트 인수가 있다면 여기에 추가
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        pythonProcess = new Process();
        pythonProcess.StartInfo = startInfo;
        pythonProcess.Start();
        
        writer = pythonProcess.StandardInput;
    }
    
    /// <summary>
    /// function 종류 - 반환값<br/>
    /// Start - (반환이 하나라서 신경 안 써도 됨)
    /// Dialogue - Positive(끄덕끄덕), Negative(도리도리), Fuck(중지 업), MultipleFace(얼굴 두 개 이상 체크)
    /// Picture - (반환이 하나라서 신경 안 써도 됨) : 근데 현재 구현되지 않음!
    /// </summary>
    /// <param name="function"></param>
    /// <param name="action">function이 끝난 뒤 실행될 함수</param>
    public async void InvokeDetector(string function, Action<string> action)
    {
        string result = await RunFunction(function);
        action?.Invoke(result);
    }

    private async Task<string> RunFunction(string function)
    {
        if (pythonProcess != null && !pythonProcess.HasExited)
        {
            return await Task.Run(() =>
            {
                writer.WriteLine(function);
                return pythonProcess.StandardOutput.ReadLine();
            });
        }
        
        return null;
    }
    
    void OnApplicationQuit()
    {
        if (pythonProcess != null && !pythonProcess.HasExited){
            InvokeDetector("End", null);
            pythonProcess?.Kill();  // 프로세스 강제 종료
            pythonProcess?.Dispose();  // 리소스 정리
        }
    }
}

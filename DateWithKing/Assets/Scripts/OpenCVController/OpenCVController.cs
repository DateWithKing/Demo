using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
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
    new void Awake()
    {
        base.Awake();
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
        pythonProcess.PriorityClass = ProcessPriorityClass.High;
    }

    /// <summary>
    /// function 종류 - 반환값<br/>
    /// Start - (반환이 하나라서 신경 안 써도 됨)
    /// Dialogue - Positive(끄덕끄덕), Negative(도리도리), Fuck(중지 업), MultipleFace(얼굴 두 개 이상 체크)
    /// Picture - (반환이 하나라서 신경 안 써도 됨) : 근데 현재 구현되지 않음!
    /// </summary>
    /// <param name="function"></param>
    /// <param name="action">function이 끝난 뒤 실행될 함수</param>
    /// <param name="timeLimit">시간제한 옵션 추가</param>
    public async void InvokeDetector(string function, Action<string> action, bool timeLimit = false)
    {
        if (timeLimit)
        {
            string result = await RunFunctionWithTimeLimit(function);
            action?.Invoke(result);
        }
        else
        {
            string result = await RunFunction(function);
            action?.Invoke(result);
        }
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
    
    private async Task<string> RunFunctionWithTimeLimit(string function)
    {
        if (pythonProcess != null && !pythonProcess.HasExited)
        {
            using (CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(5)))
            {
                return await Task.Run(async () =>
                {
                    writer.WriteLine(function);
                    // 결과를 읽기 위한 Task
                    Task<string> readTask = Task.Run(() => pythonProcess.StandardOutput.ReadLine());

                    // 타임아웃과 결과 읽기 작업을 동시에 대기
                    if (await Task.WhenAny(readTask, Task.Delay(-1, cts.Token)) == readTask)
                    {
                        // 읽기가 완료되면 결과 반환
                        return await readTask; // 결과를 비동기적으로 반환
                    }
                    else
                    {
                        // 타임아웃이 발생한 경우
                        return "Timeout"; // 또는 적절한 오류 메시지 반환
                    }
                }, cts.Token);
            }
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

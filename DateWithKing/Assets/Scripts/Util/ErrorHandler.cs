using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 에러/오류를 출력.
/// </summary>
public static class ErrorHandler
{
    /// <summary>
    /// 에러 로그를 출력.
    /// </summary>
    /// <param name="error"> enum Error에서 정의한 에러 </param>
    public static void PrintError(Define.Error error)
    {
        Debug.LogError(error.ToString());
    }
}

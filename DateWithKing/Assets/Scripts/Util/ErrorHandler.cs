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
    /// <param name="comment"> 에러와 함께 같이 출력될 코멘트(default 빈 문자열) </param>
    public static void PrintError(Define.Error error, string comment = "")
    {
        Debug.LogError(error.ToString() + comment);
    }

    /// <summary>
    /// 경고 로그를 출력.
    /// </summary>
    /// <param name="warning"> enum Warning에서 정의한 경고 </param>
    /// <param name="comment"> 에러와 함께 같이 출력될 코멘트(default 빈 문자열) </param>

    public static void PrintWarning(Define.Warning warning, string comment = "")
    {
        Debug.LogWarning(warning.ToString() + comment);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 에러/오류를 출력.
/// </summary>
public static class ErrorHandler
{
    public static void PrintError(Define.Error error)
    {
        Debug.LogError(error.ToString());
    }
}

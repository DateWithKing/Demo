using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ErrorHandler
{
    public static void PrintError(Define.Error error)
    {
        Debug.LogError(error.ToString());
    }
}

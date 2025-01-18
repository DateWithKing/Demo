using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneLoadTracker
{
    private static HashSet<string> loadedScenes = new HashSet<string>();

    public static bool IsFirstLoad(string sceneName)
    {
        if (loadedScenes.Contains(sceneName))
        {
            return false; // 이미 로드된 적 있음
        }
        else
        {
            loadedScenes.Add(sceneName); // 첫 로드 기록
            return true; // 첫 로드
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 화면에서 할 수 있는 기본적인 동작 함수를 정의
/// </summary>
public class Screen : MonoBehaviour
{
    /// <summary>
    /// 화면 활성화
    /// </summary>
    public void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 화면 비활성화
    /// </summary>
    public void HideScreen()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 씬 이동
    /// </summary>
    /// <param name="sceneName"> 씬 이름<br/>
    /// 씬이 빌드 설정에 포함되지 않을 경우 동작하지 않음
    /// </param>
    public void MoveScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 게임 종료 <br/>
    /// </summary>
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

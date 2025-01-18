using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSemesterScreen : MonoBehaviour
{
    public GameObject targetObject; // 활성화/비활성화할 오브젝트

    void Awake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 씬이 처음 로드된 경우만 활성화
        if (SceneLoadTracker.IsFirstLoad(currentSceneName))
        {
            targetObject.SetActive(true);
            GameManager.Instance.ticket = 5;
        }
        else
        {
            targetObject.SetActive(false);
        }
    }
}

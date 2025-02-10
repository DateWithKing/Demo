using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSemesterScreen : MonoBehaviour
{
    public GameObject targetObject; // 활성화/비활성화할 오브젝트
    [SerializeField] GameObject AllUnder70;

    void Awake()
    {
        
        string currentSceneName = SceneManager.GetActiveScene().name;
        

        // 씬이 처음 로드된 경우만 활성화
        if (SceneLoadTracker.IsFirstLoad(currentSceneName))
        {
            BackgroundController.Instance.ChangeImage(Background.Intro);
            targetObject.SetActive(true);
            GameManager.Instance.ticket = 0;
        }
        else
        {
            BackgroundController.Instance.ChangeImage(Background.종강총회);
            targetObject.SetActive(false);
        }
    }

    public void ChangeBackgroundDay()
    {
        BackgroundController.Instance.ChangeImage(Background.종강총회);
    }

    public void isAllUnder70()
    {
        if (GameManager.Instance.data.stats["lvSan"].value < -50 &&
            GameManager.Instance.data.stats["lvHyun"].value < -50 &&
            GameManager.Instance.data.stats["lvPyo"].value < -50)
        {
            AllUnder70.SetActive(true);
        }
    }

    public void StartEndSemester()
    {
        YarnManager.Instance.RunDialogue("종강총회_시작");
    }

    public void ClickSound()
    {
        SoundManager.Instance.PlaySFX("UI버튼_클릭");
    }
}

using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class DayNightFade : MonoBehaviour
{
    public CanvasGroup blackScreen; // 검정색 화면을 위한 CanvasGroup
    public GameObject targetObject;  // 비활성화할 게임 오브젝트
    public UnityEvent AfterFadeOut;

    void Start()
    {
        blackScreen.alpha = 0; 
    }

    // 검정색 화면 페이드 인 메서드
    public void FadeInBlackScreen()
    {
        blackScreen.alpha = 0; 
        blackScreen.gameObject.SetActive(true); // 검정색 화면 활성화
        blackScreen.DOFade(1, 1).OnComplete(DisableTargetObject); // 1초 동안 페이드 인 후 게임 오브젝트 비활성화
    }

    // 게임 오브젝트 비활성화 메서드
    private void DisableTargetObject()
    {
        targetObject.SetActive(false); // 게임 오브젝트 비활성화
        FadeOutBlackScreen(); // 검정색 화면 페이드 아웃
    }

    // 검정색 화면 페이드 아웃 메서드
    private void FadeOutBlackScreen()
    {
        blackScreen.DOFade(0, 1).OnComplete(AfterFadeOut.Invoke); // 1초 동안 페이드 아웃
    }
}
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] public bool enableFadeIn;  // 씬 로드 시 페이드인 적용 여부
    [SerializeField] public bool enableFadeOut; // 씬 전환 시 페이드아웃 적용 여부
    [SerializeField] public GameObject panel; // 페이드 효과를 적용할 패널
    private Action onCompleteCallback;
    private RectTransform panelTransform; // 패널의 위치를 조절하기 위한 RectTransform

    void Start()
    {
        if (!panel)
        {
            UnityEngine.Debug.LogError("Panel 오브젝트를 찾을 수 없습니다.");
            throw new MissingComponentException();
        }

        panelTransform = panel.GetComponent<RectTransform>();
        if (!panelTransform)
        {
            UnityEngine.Debug.LogError("Panel에 RectTransform이 없습니다.");
            throw new MissingComponentException();
        }

        // 🌟 패널을 기본적으로 화면 밖 (2000, 1500)에 배치
        panelTransform.anchoredPosition = new Vector2(2000, 1500);
        panel.GetComponent<CanvasRenderer>().SetAlpha(0f);

        // 씬 로드시 enableFadeIn이 true면 페이드인 실행
        if (enableFadeIn) StartCoroutine(CoFadeIn());
    }

    public void FadeIn()
    {
        StartCoroutine(CoFadeIn());
    }

    public void FadeOut()
    {
        StartCoroutine(CoFadeOut());
    }

    IEnumerator CoFadeIn()
    {
        // 🌟 페이드인 시작 전에 패널을 화면 중앙 (0,0)으로 이동
        panelTransform.anchoredPosition = Vector2.zero;
        panel.SetActive(true);  // 🔹 비활성화된 패널을 활성화

        float elapsedTime = 0f;
        float fadedTime = 0.5f;

        while (elapsedTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, elapsedTime / fadedTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 🌟 페이드인 완료 후 패널을 다시 화면 밖으로 이동하고 비활성화
        panelTransform.anchoredPosition = new Vector2(2000, 1500);
        onCompleteCallback?.Invoke();
    }

    IEnumerator CoFadeOut()
    {
        // 🌟 페이드아웃 적용 여부 체크
        if (!enableFadeOut) yield break;

        // 🌟 페이드아웃 시작 전에 패널을 화면 중앙 (0,0)으로 이동
        panelTransform.anchoredPosition = Vector2.zero;
        panel.SetActive(true);  // 🔹 패널을 다시 활성화해서 코루틴 실행 가능하게 함

        float elapsedTime = 0f;
        float fadedTime = 0.77f;

        while (elapsedTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 🌟 페이드아웃 완료 후 패널을 화면 밖으로 이동 (비활성화하지 않음)
        panelTransform.anchoredPosition = new Vector2(2000, 1500);
        onCompleteCallback?.Invoke();
    }

    public void RegisterCallback(Action callback)
    {
        onCompleteCallback = callback;
    }
}
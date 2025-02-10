using System.Collections;
using UnityEngine;

public class CanvasGroupFader : MonoBehaviour
{
    public CanvasGroup canvasGroup; // CanvasGroup 컴포넌트 참조
    public float fadeInDuration = 0.25f; // 페이드인 시간
    public float fadeOutDuration = 0.05f; // 페이드아웃 시간

    private Coroutine fadeCoroutine;

    private void Start()
    {
        // CanvasGroup 자동으로 가져오기
        canvasGroup = GetComponent<CanvasGroup>();
    }
    void Init(){
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void EnableCanvasGroup()
    {
        // 이미 실행 중인 코루틴이 있으면 중지
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        // 오브젝트 활성화 후 페이드인 시작
        gameObject.SetActive(true);
        if(canvasGroup == null) Init();
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(0, 1, fadeInDuration));
    }

    public void DisableCanvasGroup()
    {
        // 이미 실행 중인 코루틴이 있으면 중지
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        // 페이드아웃 후 오브젝트 비활성화
        if(gameObject.activeSelf)
            fadeCoroutine = StartCoroutine(FadeCanvasGroup(1, 0, fadeOutDuration, () => gameObject.SetActive(false)));
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration, System.Action onComplete = null)
    {
        canvasGroup.alpha = startAlpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        // 페이드가 끝난 후 작업 수행
        onComplete?.Invoke();
    }
}

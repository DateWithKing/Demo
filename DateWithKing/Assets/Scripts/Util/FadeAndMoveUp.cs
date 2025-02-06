using UnityEngine;

/// <summary>
/// UI오브젝트에 부착하면 enable 시 3초동안 서서히 흐려짐
/// </summary>
public class FadeAndMoveUp : MonoBehaviour  
{
    public float duration = 3f;     // 페이드아웃 시간

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 startPos;

    private void Awake()
    {
        // RectTransform 및 CanvasGroup 컴포넌트 가져오기
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void OnEnable()
    {
        // 초기화 및 코루틴 시작
        canvasGroup.alpha = 1f;
        StartCoroutine(FadeAndMoveCoroutine());
    }

    private System.Collections.IEnumerator FadeAndMoveCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 진행도 계산
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);

            // 투명도 감소
            canvasGroup.alpha = 1f - progress;

            yield return null;
        }

        // 애니메이션 완료 후 오브젝트 비활성화
        gameObject.SetActive(false);
    }
}

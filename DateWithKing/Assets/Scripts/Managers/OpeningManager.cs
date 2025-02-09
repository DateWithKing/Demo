using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class OpeningManager : MonoBehaviour
{
    [SerializeField] public Image warningImage;  // 경고문 이미지
    [SerializeField] public Image teamLogoImage; // 팀 로고 이미지
    [SerializeField] public GameObject mainScreenObject; // 메인 화면(배경+로고+자막)이 포함된 부모 오브젝트
    [SerializeField] public Image backgroundImage; // 배경 이미지
    [SerializeField] public GameObject skipButtonObject; // 스킵 버튼

    private List<SpriteRenderer> mainScreenSprites = new List<SpriteRenderer>();

    private float fadeDuration = 1.2f; // 페이드 시간
    private float displayDuration = 3.0f; // 표시 유지 시간
    private bool isScreenClicked = false;
    public bool isHandWaveDetected = false; // 손 흔들기 감지 여부

    private void Start()
    {
        // 처음에 스킵 버튼과 배경 비활성화
        if (skipButtonObject != null)
            skipButtonObject.SetActive(false);
        if (backgroundImage != null)
            backgroundImage.gameObject.SetActive(false);

        if (mainScreenObject != null)
        {
            mainScreenSprites.AddRange(mainScreenObject.GetComponentsInChildren<SpriteRenderer>());
        }

        // EventTrigger 컴포넌트 추가
        EventTrigger eventTrigger = skipButtonObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = skipButtonObject.AddComponent<EventTrigger>();
        }

        // PointerClick 이벤트 추가
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;  // 클릭 이벤트
        entry.callback.AddListener((data) => { SkipSequence(); });  // 클릭 시 SkipSequence() 호출
        eventTrigger.triggers.Add(entry);  // 트리거 추가

        StartCoroutine(PlayOpeningSequence());
    }

    private IEnumerator PlayOpeningSequence()
    {
        // 경고문, 팀 로고, 메인 화면 스프라이트 페이드인
        yield return StartCoroutine(FadeInOut(warningImage));
        yield return StartCoroutine(FadeInOut(teamLogoImage));
        yield return StartCoroutine(FadeInGroup(mainScreenSprites));

        // 손 흔들기 감지 시작
        StartHandWaveDetection();

        // 손 흔들릴 때까지 대기
        yield return new WaitUntil(() => isHandWaveDetected);

        // UnityEngine.Debug.Log("손 흔들기 감지 완료됨 -> 다음 단계 진행");

        // 손 흔들기 감지 후 배경 스프라이트 설정
        backgroundImage.sprite = backgroundImage.sprite; // 원하는 배경 설정 (Resources 폴더에 저장된 이미지)

        // 배경 스프라이트 페이드인
        yield return StartCoroutine(FadeInBackground(backgroundImage));

        // 배경이 활성화되면 메인 화면 스프라이트 비활성화
        foreach (var sprite in mainScreenSprites)
        {
            sprite.gameObject.SetActive(false);
        }

        // 다이얼로그가 시작될 때 스킵 버튼과 배경을 활성화
        if (skipButtonObject != null)
            skipButtonObject.SetActive(true);
        if (backgroundImage != null)
            backgroundImage.gameObject.SetActive(true);
        if (mainScreenObject != null)
            mainScreenObject.SetActive(false);

        // 얀 스크립트 실행
        YarnManager.Instance.RunDialogue("개강총회", () =>
        {
            StartCoroutine(FadeOutBackground(backgroundImage));

            SceneManager.LoadScene("Lobby");
        });
    }

    private void StartHandWaveDetection()
    {
        OpenCVController.Instance.InvokeDetector("Start", (result) =>
        {
            UnityEngine.Debug.Log($"OpenCV 감지 결과: {result}");
            isHandWaveDetected = true;
        });
    }

    private IEnumerator FadeInOut(Image image)
    {
        yield return StartCoroutine(FadeIn(image));

        float elapsedTime = 0;
        while (elapsedTime < displayDuration)
        {
            if (Input.GetMouseButtonDown(0)) // 클릭 감지
            {
                isScreenClicked = true;
                break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(FadeOut(image));
    }

    private IEnumerator FadeIn(Image image)
    {
        image.gameObject.SetActive(true);
        float elapsedTime = 0;
        Color color = image.color;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1;
        image.color = color;
    }

    private IEnumerator FadeOut(Image image)
    {
        float elapsedTime = 0;
        Color color = image.color;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 0;
        image.color = color;
        image.gameObject.SetActive(false);
    }

    private IEnumerator FadeInGroup(List<SpriteRenderer> sprites)
    {
        mainScreenObject.SetActive(true);
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            foreach (var sprite in sprites)
            {
                Color color = sprite.color;
                color.a = alpha;
                sprite.color = color;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (var sprite in sprites)
        {
            Color color = sprite.color;
            color.a = 1;
            sprite.color = color;
        }
    }

    // 스킵 버튼 클릭 시 씬 전환
    private void SkipSequence()
    {
        SceneManager.LoadScene("Lobby");
    }

    private IEnumerator FadeInBackground(Image image) // SpriteRenderer -> Image 변경
    {
        image.gameObject.SetActive(true);
        float elapsedTime = 0;
        Color color = image.color;
        color.a = 0;  // 초기 투명도 설정
        image.color = color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        image.color = color;
    }


    private IEnumerator FadeOutBackground(Image image)
    {
        float elapsedTime = 0;
        Color color = image.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        image.color = color;
        image.gameObject.SetActive(false);
    }
}

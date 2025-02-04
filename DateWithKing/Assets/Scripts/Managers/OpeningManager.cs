using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
    [SerializeField] public SpriteRenderer warningSprite;  // 경고문 스프라이트
    [SerializeField] public SpriteRenderer teamLogoSprite; // 팀 로고 스프라이트
    [SerializeField] public GameObject mainScreenObject; // 메인 화면(배경+로고+자막)이 포함된 부모 오브젝트

    private List<SpriteRenderer> mainScreenSprites = new List<SpriteRenderer>();

    public float fadeDuration = 1.2f; // 페이드 시간
    public float displayDuration = 1.5f; // 표시 유지 시간

    private void Start()
    {
        // mainScreenObject 내부의 모든 SpriteRenderer를 가져오기
        if (mainScreenObject != null)
        {
            mainScreenSprites.AddRange(mainScreenObject.GetComponentsInChildren<SpriteRenderer>());
        }

        StartCoroutine(PlayOpeningSequence());
    }

    private IEnumerator PlayOpeningSequence()
    {
        yield return StartCoroutine(FadeInOut(warningSprite));
        yield return StartCoroutine(FadeInOut(teamLogoSprite));
        yield return StartCoroutine(FadeInGroup(mainScreenSprites));

        // 손 흔들면 (openCV 연결 필요) 넘어가게 하는 부분 추가
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Lobby");
    }

    private IEnumerator FadeInOut(SpriteRenderer sprite)
    {
        yield return StartCoroutine(FadeIn(sprite));
        yield return new WaitForSeconds(displayDuration);
        yield return StartCoroutine(FadeOut(sprite));
    }

    private IEnumerator FadeIn(SpriteRenderer sprite)
    {
        sprite.gameObject.SetActive(true);
        float elapsedTime = 0;
        Color color = sprite.color;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            sprite.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1;
        sprite.color = color;
    }

    private IEnumerator FadeOut(SpriteRenderer sprite)
    {
        float elapsedTime = 0;
        Color color = sprite.color;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            sprite.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 0;
        sprite.color = color;
        sprite.gameObject.SetActive(false); 
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

        // 최종적으로 알파값을 1로 고정
        foreach (var sprite in sprites)
        {
            Color color = sprite.color;
            color.a = 1;
            sprite.color = color;
        }
    }
}
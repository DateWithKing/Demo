using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance; // 싱글톤
    [SerializeField] private FadeController fadeController; // 페이드 효과를 담당할 오브젝트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(CoLoadScene(sceneName));
    }

    private IEnumerator CoLoadScene(string sceneName)
    {
        // 🌟 씬 전환 전 페이드아웃 실행 (enableFadeOut이 true일 때)
        if (fadeController != null)
        {
            fadeController.enableFadeOut = true;
            fadeController.FadeOut();
            yield return new WaitForSeconds(0.75f);
        }

        // 씬 전환 실행
        SceneManager.LoadScene(sceneName);

        // 새로운 씬 로드 후, 1 프레임 기다린 후 페이드 인 실행
        yield return new WaitForSeconds(0.2f);
        if (fadeController != null)
        {
            fadeController.enableFadeIn = true;
            fadeController.FadeIn();
        }
    }
}
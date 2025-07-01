using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using static System.Net.Mime.MediaTypeNames;

public class PunchingManager : MonoBehaviour
{
    [SerializeField] public Slider gaugeSlider; // 게이지 UI 슬라이더
    [SerializeField] public GameObject circle; // 클릭할 원 오브젝트

    [Header("UI objects")]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI countDown;
    [SerializeField] private GameObject punchEffectPrefab;
    [SerializeField] private Canvas canvas; // UI 위치 기준


    private float gauge = 0f;
    private float maxGauge = 100f;
    private float decreaseRate = 5f; // 0.001초 동안 클릭이 없으면 감소하는 양
    private float increasePerClick = 5f; // 클릭 시 증가량
    private float gameTime = 5f; // 게임 지속 시간
    private float timeLeft;
    private float lastClickTime; // 마지막 클릭 시간

    public bool isPlaying = false;
    private bool isGameActive = false;
    [SerializeField] private Screen ChangeScreen;

    void Start()
    {
        // BackgroundController.Instance.ChangeImage(Background.미니게임);
        YarnManager.Instance.RunDialogue("종강총회_펀치머신_시작");
        SoundManager.Instance.PlaySFX("미니게임_시작");
    }

    [YarnCommand("StartPunch")]
    public async void StartPunch()
    {
        timeLeft = gameTime;
        lastClickTime = Time.time;
        gaugeSlider.value = 0;

        // 클릭 이벤트 감지를 위한 Collider 확인
        if (circle.GetComponent<Collider2D>() == null)
        {
            circle.AddComponent<CircleCollider2D>();
        }

        gauge = 0;
        gaugeSlider.value = 0;
        timeLeft = gameTime;
        StartCoroutine(CountdownRoutine());
        await Task.Delay(4000); // 3초 카운트다운 할 동안 대기
        isGameActive = true;
        isPlaying = true;
    }

    // 카운트다운 코루틴 메서드
    IEnumerator CountdownRoutine()
    {
        for (int i = 3; i > 0; i--)
        {
            countDown.gameObject.SetActive(true);
            countDown.text = i.ToString();

            // 알파값 조정 (페이드 인 효과)
            countDown.color = new Color(countDown.color.r, countDown.color.g, countDown.color.b, 1);

            yield return new WaitForSeconds(1f); // 1초 대기

            // 알파값 조정 (페이드 아웃 효과)
            countDown.color = new Color(countDown.color.r, countDown.color.g, countDown.color.b, 0);
        }

        countDown.gameObject.SetActive(false); // 카운트다운 숨기기
        isPlaying = true; // 게임 시작
        StartCoroutine(GameTimer()); // 타이머 시작
    }

    void Update()
    {
        if (!isGameActive) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 0;
        }
        timeText.text = $"{(int)timeLeft % 60:D2}";

        // 1초 동안 클릭이 없으면 게이지 감소
        if (Time.time - lastClickTime >= 0.001f)
        {
            ChangeGauge(-decreaseRate * Time.deltaTime);
        }

        if (timeLeft <= 0)
        {
            EndGame();
        }
    }

    public void OnCircleClick()
    {
        SoundManager.Instance.PlaySFX("펀치_때리기");
        if (!isGameActive) return;

        ChangeGauge(increasePerClick);
        lastClickTime = Time.time;

        ShowPunchEffect();
    }

    private void ChangeGauge(float amount)
    {
        gauge = Mathf.Clamp(gauge + amount, 0, maxGauge);
        gaugeSlider.value = gauge / maxGauge;
    }

    IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(gameTime);
        if (!isGameActive)
        {
            EndGame();
            isGameActive = true;
        }
    }

    private void EndGame()
    {
        isGameActive = false;
        UnityEngine.Debug.Log("gauge : " + gauge);

        if (gauge >= 85)
        {
            SoundManager.Instance.PlaySFX("미니게임_성공");
            YarnManager.Instance.RunDialogue("종강총회_사격게임_성공", () => ChangeScreen.MoveScene("SelectMinigame"));
            GameManager.Instance.ticket += 5;
        }
        else
        {
            SoundManager.Instance.PlaySFX("미니게임_게임오버");
            YarnManager.Instance.RunDialogue("종강총회_사격게임_실패", () => ChangeScreen.MoveScene("SelectMinigame"));
        }
    }

    private void ShowPunchEffect()
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = -1; // 깊이 값 고정 (카메라 거리 조정)

        GameObject punch = Instantiate(punchEffectPrefab, worldPos, Quaternion.identity);
        StartCoroutine(DestroyAfterDelay(punch, 0.3f));
    }

    IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
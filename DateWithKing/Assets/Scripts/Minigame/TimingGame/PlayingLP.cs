using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using Yarn.Unity;

public class PlayingLP : MonoBehaviour
{
    [SerializeField] private Transform reflection; // 빛반사부분
    [SerializeField] private Transform turntableArm;
    
    [Header("UI objects")]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI comboText;
    [SerializeField] private TMPro.TextMeshProUGUI countDown;

    private float playingTime = 10f;
    private float timeRemaining;
    private int combo; // 올바른 클릭을 연속으로 하는 횟수
    private bool isClicking = false;
    public bool isPlaying = false;
    private bool isReflecting = false;

    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }

    [YarnCommand("GameStart")]
    async void GameStart()
    {
        gameUI.SetActive(true);
        timeRemaining = playingTime;
        combo = 0;
        comboText.text = "0"; // 콤보는 0으로 시작
        StartCoroutine(CountdownRoutine());
        await Task.Delay(4000); // 3초 카운트다운 할 동안 대기
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

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            timeRemaining -= Time.deltaTime;
            if(timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver();
            }
            timeText.text = $"{(int)timeRemaining % 60:D2}";
            if (isPlaying && isReflecting && Input.GetMouseButtonDown(0))
            {
                // 클릭이 충돌한 범위 내에서 발생하는지 확인
                CheckClick();
            }
        }
        
    }

    void CheckClick()
    {
        combo++;
        UnityEngine.Debug.Log("콤보!: " + combo);
        if (combo >= 3 && isPlaying) // isPlaying이 true일 때만 GameOver 호출
        {
            isPlaying = false; // 중복 호출 방지
                               // GameOver(); // 중복 호출 방지
        }
    }

    IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(playingTime);
        GameOver();
    }

    void GameOver()
    {
        if (combo >= 3) 
        {
            UnityEngine.Debug.Log("티켓을 5장 얻었다!");
            GameManager.Instance.ticket += 5;
        }
        else
        {
            UnityEngine.Debug.Log("티켓을 얻지 못했다...");
        }
        isPlaying = false; // 게임 종료
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log("OnTriggerEnter2D 호출됨! 충돌한 오브젝트: " + other.gameObject.name);

        if (other.CompareTag("TurntableArm"))
        {
            isReflecting = true;
            UnityEngine.Debug.Log("빛반사 부분이 턴테이블 팔과 충돌!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        UnityEngine.Debug.Log("OnTriggerExit2D 호출됨! 충돌 해제된 오브젝트: " + other.gameObject.name);

        if (other.CompareTag("TurntableArm"))
        {
            isReflecting = false;
            UnityEngine.Debug.Log("빛반사 부분이 턴테이블 팔에서 벗어남!");
        }
    }
}
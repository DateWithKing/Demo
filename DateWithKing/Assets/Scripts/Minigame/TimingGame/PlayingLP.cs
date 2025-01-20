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
    [SerializeField] private GameObject backGround;

    private float playingTime = 10f;
    private float timeRemaining;
    private int combo; // 올바른 클릭을 연속으로 하는 횟수
    public bool isPlaying = false;
    private bool isMouseHeld = false;

    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }

    [YarnCommand("GameStart")]
    async void GameStart()
    {
        gameUI.SetActive(true);
        AdjustBackgroundPosition();
        timeRemaining = playingTime;
        combo = 0;
        comboText.text = $"{combo:D3}"; // 콤보는 0으로 시작
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
    void FixedUpdate()
    {
        if (!isPlaying) return;

        // LP판의 회전 각도 구하기
        float angle = reflection.transform.eulerAngles.z; // 빛반사부분의 회전 각도

        // 2시에서 4시 방향 
        bool isInValidAngleRange = (angle >= 0f && angle <= 30f) || (angle >= 330f && angle <= 360f);

        // 마우스 클릭 여부 확인 (클릭 "순간"만 감지)
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !isMouseHeld) // 마우스나 스페이스바 클릭
        {
            isMouseHeld = true; // 클릭 상태로 설정

            if (!isInValidAngleRange)
            {
                combo = 0; // 틀린 타이밍에 클릭하면 콤보 초기화
                comboText.text = $"{combo:D3}";
            }
            else
            {
                CheckClick();
            }
        }

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)) // 마우스를 떼거나 스페이스바를 떼면 클릭 상태를 풀어줌
        {
            isMouseHeld = false; // 클릭 상태 초기화
        }

        // 시간 감소
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
        }

        // UI 업데이트
        timeText.text = $"{(int)timeRemaining % 60:D2}";
        comboText.text = $"{combo:D3}";
        if (int.Parse(comboText.text) >= 3)
        {
            GameOver();
        }
    }

    void CheckClick()
    {
        combo++;
        UnityEngine.Debug.Log("콤보!: " + combo);
        isMouseHeld = false;
    }

    IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(playingTime);
        GameOver();
    }

    void GameOver()
    {
        reflection.GetComponent<Rigidbody2D>().angularVelocity = 0f; // 회전 속도를 0으로 설정해서 회전 멈추기

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

    void AdjustBackgroundPosition()
    {
        Vector3 turnCenterPos = turntableArm.transform.position;
        turnCenterPos.z = 0;
        backGround.transform.position = turnCenterPos;
    }
}
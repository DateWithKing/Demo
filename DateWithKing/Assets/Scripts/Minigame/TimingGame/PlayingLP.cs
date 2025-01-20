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
    public bool isReflecting = false;

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
    void FixedUpdate()
    {
        if (!isPlaying) return;

        // 마우스 클릭 여부 확인
        bool isMouseHeld = Input.GetMouseButton(0); // 클릭 중인지 확인

        // 충돌 감지 (reflection 영역과 turntableArm 영역이 겹칠 경우 감지)
        Collider2D hit = Physics2D.OverlapArea(
            reflection.GetComponent<Collider2D>().bounds.min,
            reflection.GetComponent<Collider2D>().bounds.max
        );

        // 디버그: 충돌 감지 여부 출력
        if (hit != null)
        {
            UnityEngine.Debug.Log("✅ 감지된 충돌: " + hit.name);
        }
        else
        {
            UnityEngine.Debug.Log("❌ 충돌 감지 안됨");
        }

        // TurntableArm과의 충돌 감지 (LP판과 겹쳤을 때)
        if (hit != null && hit.CompareTag("TurntableArm"))
        {
            // 겹친 상태에서 마우스를 클릭하고 있으면
            if (isMouseHeld)
            {
                UnityEngine.Debug.Log("🖱️ 마우스 클릭 감지됨!");
                CheckClick();
            }

            // 충돌 영역이 겹친 상태로 반사판 영역에서 클릭 시 콤보 추가
            if (!isReflecting)
            {
                StopCoroutine(nameof(DisableReflecting));  // 충돌 후 반사판 유지
                StartCoroutine(ReflectCooldown());  // 반사판 효과 대기
            }
        }
        else
        {
            // 충돌이 사라지면 반사판 해제
            if (isReflecting)
            {
                StopCoroutine(nameof(ReflectCooldown));
                StartCoroutine(DisableReflecting());
            }
        }

        // 시간 감소 (게임 진행 중)
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            GameOver();
        }

        timeText.text = $"{(int)timeRemaining % 60:D2}";
    }


    // 잠시 동안 isReflecting 유지
    IEnumerator ReflectCooldown()
    {
        isReflecting = true;
        yield return new WaitForSeconds(0.1f);
    }

    // 충돌 감지가 사라지면 0.1초 후 isReflecting 해제
    IEnumerator DisableReflecting()
    {
        yield return new WaitForSeconds(0.1f);
        isReflecting = false;
        UnityEngine.Debug.Log("Update()에서 충돌 해제!");
    }

    void CheckClick()
    {
        combo++;
        UnityEngine.Debug.Log("콤보!: " + combo);
        if (combo >= 3)
        {
            GameOver();
        }
    }

    IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(playingTime);
        GameOver();
    }

    void GameOver()
    {
        UnityEngine.Debug.Log("combo:" + combo);
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

    void OnDrawGizmos()
    {
        if (reflection != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(reflection.GetComponent<Collider2D>().bounds.center, reflection.GetComponent<Collider2D>().bounds.size);
        }
        if (turntableArm != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(turntableArm.GetComponent<Collider2D>().bounds.center, turntableArm.GetComponent<Collider2D>().bounds.size);
        }
    }

    void AdjustBackgroundPosition()
    {
        Vector3 turnCenterPos = turntableArm.transform.position;
        turnCenterPos.z = 0;
        backGround.transform.position = turnCenterPos;
    }
}
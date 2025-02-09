using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class GameMoleHole : MonoBehaviour
{
    [SerializeField] private List<Mole> moles;

    [Header("UI objects")]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI countDown;
    [SerializeField] private GameObject backGround;
    [SerializeField] private Texture2D hammerTexture;
    [SerializeField] private Texture2D runTexture;

    [Header("Cursor Settings")]
    [SerializeField] private Texture2D hammerIdleCursor;  // 기본 망치 커서
    [SerializeField] private Texture2D hammerHitCursor;   // 내려치는 망치 커서
    [SerializeField] private float hitEffectDuration = 0.2f; // 클릭 효과 지속 시간

    [SerializeField] private GameObject ChangeScreen;

    private float startingTime = 10f;
    private float timeRemaining;
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    private int score;
    public bool playing = false;
    private bool isClicking = false; // 클릭 중인지 체크

    // Start is called before the first frame update
    void Start()
    {
        BackgroundController.Instance.ChangeImage(Background.미니게임);
        gameUI.SetActive(true);
        YarnManager.Instance.RunDialogue("종강총회_두더지잡기_시작");
        SoundManager.Instance.PlaySFX("미니게임_시작");
    }

    [YarnCommand("StartMoleHole")]
    public async void StartMoleHole()
    {
        
        
        ChangeScreen.SetActive(false);
        AdjustBackgroundToMolePosition();
        ChangeCursorToHammerIdle();

        for (int i = 0; i < moles.Count; i++)
        {
            moles[i].Hide();
            moles[i].SetIndex(i);
        }
        currentMoles.Clear();
        timeRemaining = startingTime; // 시작시간(10초)로 타이머 시작
        score = 0;
        scoreText.text = "0"; // 점수는 0으로 시작
        StartCoroutine(CountdownRoutine());
        await Task.Delay(4000); // 3초 카운트다운 할 동안 대기
        playing = true;
    }

    // 커서를 망치로 변경하는 함수
    void ChangeCursorToHammerIdle()
    {
        CursorHandler.ChangeCursor(hammerIdleCursor);
    }

    // 카운트다운 코루틴 메서드
    IEnumerator CountdownRoutine()
    {
        for (int i = 3; i > 0; i--)
        {
            countDown.gameObject.SetActive(true);
            countDown.text = i.ToString();

            //알파값 조정 (페이드 인 효과)
            countDown.color = new Color(countDown.color.r, countDown.color.g, countDown.color.b, 1);

            yield return new WaitForSeconds(1f); // 1초 대기

            // 알파값 조정 (페이드 아웃 효과)
            countDown.color = new Color(countDown.color.r, countDown.color.g, countDown.color.b, 0);
        }

        countDown.gameObject.SetActive(false); // 카운트다운 숨기기
    }

    IEnumerator WaitSecond()
    {
        
        yield return new WaitForSeconds(1.0f);
        CursorHandler.ChangeCursor(runTexture);
        ChangeScreen.SetActive(true);
    }


    public void GameOver(int type)
    {
        
        if (int.Parse(scoreText.text) >= 15)
        {
            SoundManager.Instance.PlaySFX("미니게임_성공");
            YarnManager.Instance.RunDialogue("종강총회_두더지잡기_성공"); // -> 다이얼로그
            GameManager.Instance.ticket += 5;
        }
        else
        {
            SoundManager.Instance.PlaySFX("미니게임_게임오버");
            YarnManager.Instance.RunDialogue("종강총회_두더지잡기_실패"); // -> 다이얼로그
        }
        foreach (Mole mole in moles)
        {
            mole.StopGame();
        }

        
        playing = false;
        
        StartCoroutine(WaitSecond());

    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver(0);
            }
            timeText.text = $"{(int)timeRemaining % 60:D2}"; // 남은 시간(초)를 항상 두 자리로 보여줌
            if (currentMoles.Count <= 1)
            {
                UnityEngine.Debug.Log(currentMoles);
                int index = UnityEngine.Random.Range(0, moles.Count);
                if (!currentMoles.Contains(moles[index]))
                {
                    currentMoles.Add(moles[index]);
                    moles[index].Activate(3);
                }
            }

            if (Input.GetMouseButtonDown(0) && !isClicking)
            {
                StartCoroutine(ClickEffect());
                
            }
        }
    }

    private IEnumerator ClickEffect()
    {
        isClicking = true;
        CursorHandler.ChangeCursor(hammerHitCursor); // 내려치는 커서 적용
        yield return new WaitForSeconds(hitEffectDuration); // 일정 시간 대기
        CursorHandler.ChangeCursor(hammerIdleCursor); // 기본 커서로 복귀
        isClicking = false;
    }

    public void AddScore(int moleIndex)
    {
        score += 1;
        SoundManager.Instance.PlaySFX("두더지_때리기");
        scoreText.text = $"{score}";
        currentMoles.Remove(moles[moleIndex]);
    }

    public void RemoveMole(int moleIndex) {
        currentMoles.Remove(moles[moleIndex]);
    }

    // 배경 위치 고정하는 메서드
    void AdjustBackgroundToMolePosition()
    {
        if (moles.Count == 0) return;

        Vector3 moleCenterPos = moles[0].transform.position;
        moleCenterPos.z = 0;
        backGround.transform.position = moleCenterPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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

    private float startingTime = 10f;
    private float timeRemaining;
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    private int score;
    private bool playing = false;

    // Start is called before the first frame update
    async void Start()
    {
        gameUI.SetActive(true);
        AdjustBackgroundToMolePosition();
        ChangeCursorToHammer();
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
    void ChangeCursorToHammer()
    {
        if (hammerTexture != null)
        {
            // Texture2D hammerTexture = hammerSprite.texture;
            CursorHandler.ChangeCursor(hammerTexture);  // 커서 변경
        }
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

    public void GameOver(int type)
    {
        CursorHandler.DefaultCursor();
        if (int.Parse(scoreText.text) >= 6)
        {
            UnityEngine.Debug.Log("티켓을 5개 얻었다!");
        }
        else
        {
            UnityEngine.Debug.Log("티켓을 얻지 못했다...");
        }
        foreach (Mole mole in moles)
        {
            mole.StopGame();
        }
        playing = false;
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
                int index = UnityEngine.Random.Range(0, moles.Count);
                if (!currentMoles.Contains(moles[index]))
                {
                    currentMoles.Add(moles[index]);
                    moles[index].Activate(3);
                }
            }
        }
    }

    public void AddScore(int moleIndex)
    {
        score += 1;
        scoreText.text = $"{score}";
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


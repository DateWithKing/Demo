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
    [SerializeField] private Sprite backGround;

    private float startingTime = 10f;
    private float timeRemaining;
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    private int score;
    private bool playing = false;

    // Start is called before the first frame update
    async void Start()
    {
        gameUI.SetActive(true);
        for (int i = 0; i < moles.Count; i++)
        {
            moles[i].Hide();
            moles[i].SetIndex(i);
        }
        currentMoles.Clear();
        timeRemaining = startingTime;
        score = 0;
        scoreText.text = "0";
        StartCoroutine(CountdownRoutine());
        await Task.Delay(3000);
        playing = true;
    }

    // 카운트다운 코루틴
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
            timeText.text = $"{(int)timeRemaining % 60:D2}";
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
}

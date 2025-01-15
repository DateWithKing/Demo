using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayMole : MonoBehaviour
{
    [SerializeField] private List<Mole> moles;

    [Header("UI Objects")]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    // 게임 플레이 시간 : 10초
    private float startingTime = 10f;

    private float timeRemaining;
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    private int score;
    private bool playing = false;

    public void StartGame()
    {
        // UI 숨기기
        gameUI.SetActive(true);

        // mole 숨기기
        for (int i = 0; i < moles.Count; i++)
        {
            moles[i].Hide();
            moles[i].SetIndex(i);
        }
        currentMoles.Clear();
        timeRemaining = startingTime;
        score = 0;
        scoreText.text = "0";
        playing = true;
    }

    public void GameOver(int time)
    {
        if (time == 0)
        {
            UnityEngine.Debug.Log("타임 오버!");
        }
        // 모든 두더지 숨기기
        foreach (Mole mole in moles)
        {
            mole.StopGame();
        }
        playing = false;

        // 점수에 따라 티켓 여부
        if (score >= 6)
        {
            UnityEngine.Debug.Log("티켓을 5개 얻었다!");
        }
        else
        {
            UnityEngine.Debug.Log("티켓을 얻지 못했다...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            // 시간 업데이트
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver(0);
            }
            timeText.text = $"{(int)timeRemaining % 60:D2}";
            // 랜덤 두더지 선택
            if (currentMoles.Count < 1)
            {
                int index = UnityEngine.Random.Range(0, moles.Count);
                if (!currentMoles.Contains(moles[index]))
                {
                    currentMoles.Add(moles[index]);
                    moles[index].Activate(score / 10);
                }
            }
        }
    }

    public void AddScore(int moleIndex)
    {
        score += 1;
        scoreText.text = $"{score}";
        // timeRemaining += 1;
        // 활성화된 두더지 삭제
        currentMoles.Remove(moles[moleIndex]);
    }
}

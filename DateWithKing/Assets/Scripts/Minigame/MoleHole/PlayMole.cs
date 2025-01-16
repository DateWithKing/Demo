using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMole : MonoBehaviour
{
    [SerializeField] private List<Mole> moles;

    [Header("UI objects")]
    [SerializeField] private GameObject playButton;

    private float startingTime = 10f;

    private float timeRemaining;
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    private int score;
    private bool playing = false;
    /*
    public void StartGame()
    {
        // UI 숨기기
        // playButton.SetActive(false);
        // mole 숨기기
        for (int i = 0; i < moles.Count; i++)
        {
            moles[i].Hide();
            moles[i].SetIndex(i);
        }
        currentMoles.Clear();
        timeRemaining = startingTime;
        score = 0;
        playing = true;
    }

    public void GameOver(int type)
    {
        playing = false;
    }

    public void AddScore(int moleIndex)
    {
        score += 1;
        currentMoles.Remove(moles[moleIndex]);
    }

    // Start is called before the first frame update
    void Start()
    {

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
            if (currentMoles.Count <= (score / 10))
            {
                // 랜덤 몰 선택
                int index = UnityEngine.Random.Range(0, moles.Count);
                if (!currentMoles.Contains(moles[index]))
                {
                    currentMoles.Add(moles[index]);
                    // moles[index].Activate(score / 10);
                }
            }
        }
    }
    */
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMoleHole : MonoBehaviour
{
    [SerializeField] private List<Mole> moles;

    [Header("UI objects")]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    private float startingTime = 10f;
    private float timeRemaining;
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    private int score;
    private bool playing = false;

    // Start is called before the first frame update
    void Start()
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
        playing = true;
    }

    public void GameOver(int type)
    {
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

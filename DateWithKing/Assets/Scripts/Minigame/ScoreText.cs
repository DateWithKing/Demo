using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreText : SceneSingleton<ScoreText>
{
    private TMP_Text scoreText;
    void Start()
    {
        scoreText = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    /// <summary>
    /// ScoreTextModule을 BaseCanvas에 붙이고 해당 함수를 부르면 점수가 표시됩니다.
    /// </summary>
    /// <param name="score">표시하고 싶은 점수</param>
    public void SetScore(int score){
        scoreText.text = $"{score:D2}";
    }
}

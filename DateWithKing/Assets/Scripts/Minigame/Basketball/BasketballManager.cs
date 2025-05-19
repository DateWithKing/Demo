using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yarn.Unity;

public class BasketballManager : SceneSingleton<BasketballManager>
{
    [SerializeField] private Ball[] balls;
    public event Action StartGame;
    public event Action EndGame;
    private int score = 0;
    const int CLEAR_SCORE = 3;
    const int TICKET = 5;
    const int TIME_LIMIT = 20;
    private bool gameOverSemaphore = true;
    [SerializeField] private Screen ChangeScreen;


    void Start()
    {
        BackgroundController.Instance.ChangeImage(Background.미니게임);
        YarnManager.Instance.RunDialogue("종강총회_농구게임_시작");
        SoundManager.Instance.PlaySFX("미니게임_시작");
    }

    [YarnCommand("PlayBasketball")]
    public void PlayBasketball(){
        NumTimer.Instance.StartTimer(TIME_LIMIT, GameOver);
        StartGame?.Invoke();
    }

    public Ball GetBall(){
        foreach(Ball ball in balls){
            if(ball.isReady) return ball;
        }
        return null;
    }

    public void ScoreUp(){
        score++;
        ScoreText.Instance.SetScore(score);
        if(score >= CLEAR_SCORE){
            GameOver();
        }
    }

    private void GameOver(){
        if(gameOverSemaphore) gameOverSemaphore = false;
        else return;

        EndGame?.Invoke();
        NumTimer.Instance.HideTimer();

        if(score >= CLEAR_SCORE){
            SoundManager.Instance.PlaySFX("미니게임_성공");
            YarnManager.Instance.RunDialogue("종강총회_농구게임_성공", () => ChangeScreen.MoveScene("SelectMinigame"));
            GameManager.Instance.ticket += TICKET;
        }
        else{
            SoundManager.Instance.PlaySFX("미니게임_게임오버");
            YarnManager.Instance.RunDialogue("종강총회_농구게임_실패", () => ChangeScreen.MoveScene("SelectMinigame"));
        }
    }

}

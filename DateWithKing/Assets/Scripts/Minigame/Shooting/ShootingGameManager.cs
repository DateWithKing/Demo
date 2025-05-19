using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yarn.Unity;

public class ShootingGameManager : SceneSingleton<ShootingGameManager>
{
    public event Action StartGame;
    public event Action EndGame;
    public int TargetScore;
    public int TrapScore;
    int score = 0;
    const int CLEAR_SCORE = 10;
    const int TICKET = 5;
    const int TIME_LIMIT = 10;
    private bool gameOverSemaphore = true;
    [SerializeField] private Screen ChangeScreen;


    void Start()
    {
        BackgroundController.Instance.ChangeImage(Background.미니게임);
        YarnManager.Instance.RunDialogue("종강총회_사격게임_시작");
        SoundManager.Instance.PlaySFX("미니게임_시작");   
    }

    [YarnCommand("PlayShootingGame")]
    public void PlayBasketball(){
        NumTimer.Instance.StartTimer(TIME_LIMIT, GameOver);
        StartGame?.Invoke();
    }

    public void GetScore(string tag){
        if(tag == "Trap"){
            score += TrapScore;
            if(score < 0) score = 0;
        }
        else{
            score += TargetScore;
        }
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
            YarnManager.Instance.RunDialogue("종강총회_사격게임_성공", () => ChangeScreen.MoveScene("SelectMinigame"));
            GameManager.Instance.ticket += TICKET;
        }
        else{
            SoundManager.Instance.PlaySFX("미니게임_게임오버");
            YarnManager.Instance.RunDialogue("종강총회_사격게임_실패", () => ChangeScreen.MoveScene("SelectMinigame"));
        }
    }
}

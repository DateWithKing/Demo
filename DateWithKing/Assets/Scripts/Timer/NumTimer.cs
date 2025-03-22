using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NumTimer : SceneSingleton<NumTimer>
{
    [SerializeField] private TMP_Text remainingTime; // 타이머 텍스트
    [SerializeField] private GameObject timerBox; // 타이머 상자

    private int time;
    private event Action timeout;

    void Start(){
        timerBox.SetActive(false);
    }

    /// <summary>
    /// 해당 씬의 BaseCanvas에 NumTimerModule을 붙이고 부르세요, 타이머를 종료하려면 HideTimer를 호출하세요 <br/>
    /// </summary>
    /// <param name="time">제한 시간</param>
    /// <param name="timeout">타임오버 시 실행될 함수</param>
    public void StartTimer(int time, Action timeout){
        if(time > 99) time = 99;
        this.time = time;
        this.timeout += timeout;
        timerBox.SetActive(true);
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown(){
        for(int i = time; i > 0; i--){
            remainingTime.text = $"{i:D2}";
            yield return new WaitForSeconds(1f);
        }
        timerBox.SetActive(false);
        timeout?.Invoke();
    }

    public void HideTimer(){
        StopAllCoroutines();
        timerBox.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TimeBarController : SceneSingleton<TimeBarController>
{
    TimeBar timeBar;
    void Start() {
        timeBar = transform.Find("TimeBar").GetComponent<TimeBar>();
    }

    /// <summary>
    /// 해당 씬에 TimeBarModule을 붙이고 부르세요, 타이머를 종료하려면 HideTimer를 호출하세요 <br/>
    /// </summary>
    /// <param name="time">제한 시간</param>
    /// <param name="timeout">타임오버 시 실행될 함수</param>
    public void StartTimer(int time = 5, Action timeout = null){
        if (timeBar == null){ Debug.LogError("타임바 없음"); return; }
        timeBar.totalTime = time;
        timeBar.Timeout += timeout;
        timeBar.gameObject.SetActive(true);
        timeBar.gameObject.SetActive(true);
    }

    /// <summary>
    /// 타이머 종료 함수
    /// </summary>
    public void HideTimer(){
        timeBar.gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemesterScreenPresenter : MonoBehaviour
{
    private Screen screen;

    void Awake()
    {
        screen = GetComponent<Screen>();
    }
    void Start()
    {
        BackgroundController.Instance.ChangeImage(Background.Intro);
        SemesterSceneData.Instance.clock.DateChanged -= SemesterEnd;
        SemesterSceneData.Instance.clock.DateChanged += SemesterEnd;
        SemesterSceneData.Instance.clock.DateChanged -= OneDateLater;
        SemesterSceneData.Instance.clock.DateChanged += OneDateLater;
        SemesterSceneData.Instance.clock.TimeChanged -= BackgroundChanger;
        SemesterSceneData.Instance.clock.TimeChanged += BackgroundChanger;
    }
    
    //Semester 동안 배경 변경 담당
    private void BackgroundChanger()
    {
        if (SemesterSceneData.Instance.clock.GetCurrentWeekCycle() == WeekCycle.Day)
            BackgroundController.Instance.ChangeImage((Background)Enum.Parse(typeof(Background), $"Day{SemesterSceneData.Instance.clock.GetCurrentTimeAsPeriod()}"));
        else BackgroundController.Instance.ChangeImage(Enum.Parse<Background>(SemesterSceneData.Instance.clock.GetCurrentWeekCycle().ToString()));
    }

    private void OneDateLater()
    {
        //초기화 값 GameManager에서 가져오도록 수정해야 함
        SemesterSceneData.Instance.hp.InitHp(GameManager.Instance.data.stats["hp"].value * 10, GameManager.Instance.data.stats["bonusHp"].value);
        GameManager.Instance.data.stats["bonusHp"].InitStat();
    }

    private void SemesterEnd()
    {
        if (GameManager.Instance.data.date.CountPassedDate() != Date.SemesterDays) return;
        
        Popup.Instance.PopPanel(String.Empty);
        screen.MoveScene("EndSemester");
    }
}

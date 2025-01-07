using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPresenter : Presenter
{
    [SerializeField]
    private WeekCycle weekCycle = WeekCycle.Intro;
    private Screen screen;
    void Awake()
    {
        screen = GetComponent<Screen>();
    }
    void Start()
    {
        SemesterSceneData.Instance.clock.TimeChanged -= UpdateBackgroundScreen;
        SemesterSceneData.Instance.clock.TimeChanged += UpdateBackgroundScreen;
    }

    private void UpdateBackgroundScreen()
    {
        //Intro로 돌아가면 Date 업데이트
        if (weekCycle == WeekCycle.Intro 
            && SemesterSceneData.Instance.clock.GetCurrentWeekCycle() == WeekCycle.Intro)
        {
            GameManager.Instance.data.date.NextDate(); 
        }
        //현재 씬 활성화
        if (SemesterSceneData.Instance.clock.GetCurrentWeekCycle() == weekCycle)
        {
            screen.ShowScreen();
        }
        //직전 weekCycle 씬 제외 전부 비활성화
        else if ((int)SemesterSceneData.Instance.clock.GetCurrentWeekCycle() - 1 != (int)weekCycle)
            screen.HideScreen();
    }
}

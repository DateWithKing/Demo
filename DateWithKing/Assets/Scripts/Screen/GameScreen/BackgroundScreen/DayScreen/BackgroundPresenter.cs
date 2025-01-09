using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundPresenter : Presenter
{
    [SerializeField] private WeekCycle weekCycle = WeekCycle.Intro;
    [SerializeField] private Texture2D cursorImage;
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
            if (weekCycle != WeekCycle.Intro)
            {
                CursorHandler.ChangeCursor(cursorImage);
            }
        }
        //직전 weekCycle 씬 제외 전부 비활성화
        else if (SemesterSceneData.Instance.clock.GetBeforeWeekCycle() != weekCycle)
            screen.HideScreen();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 하루의 시간을 나타내는 타입 <br/>
/// WeekCycle에 종속됨(기획 변경 시 취약함)
/// </summary>
public class Clock
{
    [JsonProperty]
    private int currentHour = 10;

    /// <summary>
    /// 시간이 바뀔 때 호출
    /// </summary>
    public event Action TimeChanged;
    public event Action DateChanged;

    /// <summary>
    /// 현재 시간을 반환<br/>
    /// 형식 : 12:00
    /// </summary>
    /// <returns></returns>
    public string GetCurrentTime()
    {
        return IntToTime(currentHour);
    }

    /// <summary>
    /// 현재 시간을 교시로 반환(1, 2, 3) <br/>
    /// 만약 현재 시간이 낮이 아닐 경우 0을 반환
    /// </summary>
    /// <returns></returns>
    public int GetCurrentTimeAsPeriod()
    {
        switch (currentHour)
        {
            case 12:
                return 1;
            case 14:
                return 2;
            case 16:
                return 3;
            default:
                return 0;
        }
    }

    /// <summary>
    /// 시계를 하루를 시작하는 시간으로 회귀
    /// </summary>
    public void InitTime()
    {
        currentHour = 10;
    }

    /// <summary>
    /// 다음 시간으로 시계를 돌림
    /// </summary>
    public void NextTime()
    {
        //12, 14, 16, 18 사이클
        currentHour.LimitIncrement(18, 2, 10);
        TimeChanged?.Invoke();
        if(currentHour == 10) DateChanged?.Invoke();
    }

    /// <summary>
    /// 다음 시간으로 시계를 돌린 뒤 그 시간을 string으로 반환
    /// </summary>
    /// <returns></returns>
    public string UpdateAndGetNextTime()
    {
        NextTime();
        return GetCurrentTime();
    }

    /// <summary>
    /// 현재 시간을 WeekCycle로 반환
    /// </summary>
    /// <returns></returns>
    public WeekCycle GetCurrentWeekCycle()
    {
        switch (currentHour)
        {
            case 10:
                return WeekCycle.Intro;
            case 12:
            case 14:
            case 16:
                return WeekCycle.Day;
            case 18:
                return WeekCycle.Night;
            default:
                return WeekCycle.Intro;
        }
    }

    public WeekCycle GetBeforeWeekCycle()
    {
        if((int)GetCurrentWeekCycle() == 0) return WeekCycle.Night;
        return (WeekCycle)((int)GetCurrentWeekCycle() - 1);
    }

    private string IntToTime(int hour)
    {
        return hour.ToString() + ":00";
    }
}

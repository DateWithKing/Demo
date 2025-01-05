using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 하루의 시간을 나타내는 타입
/// </summary>
public class Clock
{
    [JsonProperty]
    private int currentHour = 12;

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
        currentHour = 12;
    }

    /// <summary>
    /// 다음 시간으로 시계를 돌림
    /// </summary>
    public void NextTime()
    {
        //12, 14, 16, 18 사이클
        currentHour.LimitIncrement(18, 2, 12);
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

    private string IntToTime(int hour)
    {
        return hour.ToString() + ":00";
    }
}

using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Date
{
    private const int BeginMonth = 3;
    private const int EndMonth = 12;
    private const int BeginWeek = 1;
    private const int EndWeek = 4;

    [JsonProperty]
    private int currentMonth;
    [JsonProperty]
    private int currentWeek;
    [JsonProperty]
    private Days currentDays;
    [JsonProperty]
    private int countPassedDate = 1; //주차가 몇 번 지났는지
    
    public string GetCurrentDate(bool enterDays = false)
    {
        if(enterDays) return $"{currentMonth}월 {currentWeek}주차\n{currentDays.ToString()}요일";
        return $"{currentMonth}월 {currentWeek}주차 {currentDays.ToString()}요일";
    }

    public void InitDate()
    {
        currentMonth = BeginMonth;
        currentWeek = BeginWeek;
        currentDays = Days.월;
        countPassedDate = 1;
    }

    public void NextDate()
    {
        //달 업데이트
        if (currentWeek == EndWeek) currentMonth++;
        
        //주차 업데이트
        currentWeek.LimitIncrement(EndWeek, BeginWeek);
        
        //요일 업데이트
        int days = (int)currentDays;
        days.LimitIncrement((int)Days.금);
        currentDays = (Days)days;
        
        //지난 날 수 증가
        countPassedDate++;
    }

    public int CountPassedDate()
    {
        return countPassedDate;
    }

    public int GetCurrentMonth()
    {
        return currentMonth;
    }

    public int GetCurrentWeek()
    {
        return currentWeek;
    }

    public Days GetCurrentDays()
    {
        return currentDays;
    }
    
}

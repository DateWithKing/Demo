using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Date
{
    public const int SemesterDays = 5;
    private const int BeginMonth = 3;
    private const int EndMonth = 12;
    private const int BeginWeek = 1;
    private const int EndWeek = 4;

    [JsonProperty]
    private int currentMonth = BeginMonth;
    [JsonProperty]
    private int currentWeek = BeginWeek;
    [JsonProperty]
    private Days currentDays = Days.MON;
    [JsonProperty]
    private int countPassedDate = 1; //주차가 몇 번 지났는지
    
    public string GetCurrentDate(bool enterDays = false)
    {
        string format = LocalizationSettings.StringDatabase.GetLocalizedString("Days", enterDays ? "DATE_MULTILINE" : "DATE_INLINE");
        string currentDay = LocalizationSettings.StringDatabase.GetLocalizedString("Days", $"DAYS_{currentDays.ToString()}");

        string weekString;
        if (LocalizationSettings.SelectedLocale.Identifier.Code.StartsWith("en", System.StringComparison.OrdinalIgnoreCase))
        {
            weekString = ToOrdinal(currentWeek);
        }
        else
        {
            weekString = currentWeek.ToString();
        }
        
        return string.Format(
            format, 
            currentMonth,
            weekString,
            currentDay
        );
    }
    
    private string ToOrdinal(int number)
    {
        if (number <= 0) return number.ToString(); // 0이나 음수는 그대로 반환

        switch (number % 100)
        {
            case 11:
            case 12:
            case 13:
                return number + "th";
        }

        switch (number % 10)
        {
            case 1:
                return number + "st";
            case 2:
                return number + "nd";
            case 3:
                return number + "rd";
            default:
                return number + "th";
        }
    }

    public void InitDate()
    {
        currentMonth = BeginMonth;
        currentWeek = BeginWeek;
        currentDays = Days.MON;
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
        days.LimitIncrement((int)Days.FRI);
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

    public int GetPassedDays()
    {
        return countPassedDate;
    }

    public Days GetCurrentDays()
    {
        return currentDays;
    }

    public string GetCurrentDaysInKr()
    {
        switch (currentDays)
        {
            case Days.MON:
                return "월";
            case Days.TUE:
                return "화";
            case Days.WED:
                return "수";
            case Days.THU:
                return "목";
            case Days.FRI:
                return "금";
        }
        return "월";
    }
    
}

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// enum 정의를 모음.
/// 해당 파일 외에 public enum을 만들지 않음.
/// </summary>

    public enum Error
    {

    }

    public enum Warning
    {
        인덱스_범위를_넘어서는_접근을_시도,
        
        //SceneSingleton
        SceneSingleton을_사용하지_않는_씬에서_싱글톤_접근을_함,
    }

    public enum DynamicData
    {
        Save0 = 0,
        Save1 = 1,
        Save2 = 2,
    }

    public enum WeekCycle
    {
        Intro,
        Day,
        Night,
    }
    
    /// <summary>
    /// 요일을 나타내는 enum
    /// </summary>
    public enum Days{
        월,
        화,
        수,
        목,
        금
    }

    public enum DaySpot
    {
        동아리방,
        강의실,
        공대도서관,
        식당,
        정문
    }

    public enum Character
    {
        양나현,
        신아산,
        서은표
    }
    
    public enum Background
    {
        Intro,
        Day,
        Night,
        Looking,
        SummerVacation,
        ClosingClass,
        MiniGame,
    }

    public enum Appearance
    {
        머리색,
        헤어스타일,
        키,
        인상
    }

    public enum InitialStat
    {
        hp,
        str,
        wis,
        slv,
        otk
    }
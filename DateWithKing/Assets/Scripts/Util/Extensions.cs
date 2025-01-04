using System;
using System.Collections;
using System.Collections.Generic;

///<summary>
/// c# extension 함수를 모음.
/// 해당 클래스 외에 extension 함수를 만들지 않음. 
///</summary>
public static class Extensions
{
    /// <summary>
    /// enum 값을 소문자 string으로 반환
    /// </summary>
    /// <param name="input">enum 값</param>
    /// <returns></returns>
    public static string ToLowerString(this Enum input)
    {
        return input.ToString().ToLower();
    }

    /// <summary>
    /// int를 1 증가시키는 과정에서 값 limit를 넘는 것을 방지하고, 제약 값을 넘었을 시 limitValue로 값을 변경 <br/>
    /// 변수의 값을 제한하고 제한 값을 벗어날 시 원점으로 회귀해야 할 때 사용<br/>
    /// 만약 값 증가와 무관하게 범위 제약만 하고 싶을 경우 Math.Clamp()를 사용할 것
    /// </summary>
    /// <param name="number"> 1 증가시킬 변수</param>
    /// <param name="limit"> number의 상한값(상한값이 number보다 클 시 limitValue로 회귀) </param>
    /// <param name="limitValue"> limit을 넘겼을 시 회귀할 값(default 0) </param>
    public static void LimitIncrement(ref this int number, int limit, int limitValue = 0)
    {
        number++;
        if (number > limit) number = limitValue;
    }

    /// <summary>
    /// int를 1 증가시키는 과정에서 값 limit를 넘는 것을 방지하고, 제약 값을 넘었을 시 limitValue로 값을 변경 <br/>
    /// 변수의 값을 제한하고 제한 값을 벗어날 시 원점으로 회귀해야 할 때 사용<br/>
    /// 만약 값 증가와 무관하게 범위 제약만 하고 싶을 경우 Math.Clamp()를 사용할 것
    /// </summary>
    /// <param name="number"> 1 증가시킬 변수</param>
    /// <param name="limit"> number의 상한값(상한값이 number보다 클 시 limitValue로 회귀) </param>
    /// <param name="increment">증가시킬 폭</param>
    /// <param name="limitValue"> limit을 넘겼을 시 회귀할 값(default 0) </param>
    public static void LimitIncrement(ref this int number, int limit, int increment, int limitValue)
    {
        number += increment;
        if (number > limit) number = limitValue;
    }
}

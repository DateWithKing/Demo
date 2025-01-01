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
}

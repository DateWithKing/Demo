using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INextWeekView
{
    /// <summary>
    /// 주차를 화면에 출력
    /// </summary>
    /// <param name="date">출력할 주차</param>
    public void PrintDate(Date date);
}

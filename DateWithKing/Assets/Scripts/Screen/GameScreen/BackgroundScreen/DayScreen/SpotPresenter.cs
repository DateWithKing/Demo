using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장소의 행동을 관리
/// </summary>
public class SpotPresenter : Presenter
{
    [SerializeField]
    private DaySpot spot = DaySpot.강의실;
    private void OnEnable()
    {
        CursorHandler.ChangeCursor();
        string spotData = $"{GameManager.Instance.data.date.GetCurrentDays().ToString()}" +
                      $"_{SemesterSceneData.Instance.clock.GetCurrentTimeAsPeriod()}" +
                      $"_{spot}";
        if (SemesterSceneData.Instance.DayDialogue.spotCharacters.ContainsKey(spotData))
        {
            Debug.Log($"{spotData}에 방문해 {SemesterSceneData.Instance.DayDialogue.spotCharacters[spotData]}을/를 만났습니다.");
            SemesterSceneData.Instance.hp.UseHp(20);
            Debug.Log($"체력을 20 사용했습니다. 현재 체력 : {SemesterSceneData.Instance.hp.GetHp()}");
        }
        else
        {
            SemesterSceneData.Instance.hp.RecoverHp(10);
            Debug.Log($"체력을 10 회복했습니다. 현재 체력 : {SemesterSceneData.Instance.hp.GetHp()}");
        }
    }

    private void OnDisable()
    {
        CursorHandler.ReturnCursor();
        SemesterSceneData.Instance.clock.NextTime();
    }
}

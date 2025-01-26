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
            string dialogue = $"주{GameManager.Instance.data.date.GetCurrentWeek().ToString()}" +
                              $"_{GameManager.Instance.data.date.GetCurrentDays().ToString()}" +
                              $"_{SemesterSceneData.Instance.clock.GetCurrentTimeAsPeriod()}교시" +
                              $"_{spot.ToString()}" +
                              $"_{SemesterSceneData.Instance.DayDialogue.spotCharacters[spotData]}";
            Debug.Log($"{spotData}에 방문해 {SemesterSceneData.Instance.DayDialogue.spotCharacters[spotData]}을/를 만났습니다.");
            YarnManager.Instance.RunDialogue(dialogue);
            SemesterSceneData.Instance.hp.UseHp(SemesterSceneData.Instance.spot.deltaStat[spot.ToString()].hpCost);
            Debug.Log($"체력을 사용했습니다. 현재 체력 : {SemesterSceneData.Instance.hp.GetHp()}");
        }
        else
        {
            Debug.Log("아무도 없습니다.");
        }
    }

    private void OnDisable()
    {
        CursorHandler.ReturnCursor();
        SemesterSceneData.Instance.clock.NextTime();
    }
}

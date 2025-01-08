using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPresenter : Presenter
{
    [SerializeField]
    private DaySpot spot = DaySpot.강의실;
    private void OnEnable()
    {
        string spotData = $"{GameManager.Instance.data.date.GetCurrentDays().ToString()}" +
                      $"_{SemesterSceneData.Instance.clock.GetCurrentTimeAsPeriod()}" +
                      $"_{spot}";
        Debug.Log($"{spotData}에 방문해 {SemesterSceneData.Instance.daySpot.spotCharacters[spotData]}을/를 만났습니다.");
    }

    private void OnDisable()
    {
        SemesterSceneData.Instance.clock.NextTime();
    }
}

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
        if (SemesterSceneData.Instance.daySpot.spotCharacters.ContainsKey(spotData))
        {
            Debug.Log($"{spotData}에 방문해 {SemesterSceneData.Instance.daySpot.spotCharacters[spotData]}을/를 만났습니다.");
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
        SemesterSceneData.Instance.clock.NextTime();
    }
}

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
        Debug.Log($"{spot}에 방문했습니다. ({SemesterSceneData.Instance.clock.GetCurrentTime()})");
    }

    private void OnDisable()
    {
        SemesterSceneData.Instance.clock.NextTime();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekCyclePresenter : MonoBehaviour
{
    [SerializeField] private WeekCycle weekCycle = WeekCycle.Intro;

    void Start()
    {
        SemesterSceneData.Instance.clock.TimeChanged -= UpdateBackgroundScreen;
        SemesterSceneData.Instance.clock.TimeChanged += UpdateBackgroundScreen;
    }
    
    private void UpdateBackgroundScreen()
    {
        //현재 씬 활성화
        if (SemesterSceneData.Instance.clock.GetCurrentWeekCycle() == weekCycle)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

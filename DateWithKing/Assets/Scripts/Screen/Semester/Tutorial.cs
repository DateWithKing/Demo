using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    void Start()
    {
        SemesterSceneData.Instance.clock.TimeChanged -= DayAfterOne;
        SemesterSceneData.Instance.clock.TimeChanged += DayAfterOne;
    }

    private void DayAfterOne()
    {
        if (gameObject.activeSelf == false) return;
        
        if(gameObject.name == "Day_Tutorial" && SemesterSceneData.Instance.clock.GetCurrentTimeAsPeriod() == 0)
            gameObject.SetActive(false);
        
        if (GameManager.Instance.data.date.GetPassedDays() > 1)
            gameObject.SetActive(false);
    }
}

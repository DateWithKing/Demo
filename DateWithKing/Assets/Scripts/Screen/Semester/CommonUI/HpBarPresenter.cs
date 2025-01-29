using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBarPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI date;
    [SerializeField] private Slider currentHp;
    [SerializeField] private Slider maxHp;
    
    // Start is called before the first frame update
    void Start()
    {
        DateUpdate();
        HpUpdate();
        SemesterSceneData.Instance.clock.TimeChanged -= DateUpdate;
        SemesterSceneData.Instance.clock.TimeChanged += DateUpdate;
        SemesterSceneData.Instance.hp.CurrentHpChanged -= HpUpdate;
        SemesterSceneData.Instance.hp.CurrentHpChanged += HpUpdate;
    }

    private void HpUpdate()
    {
        currentHp.value = SemesterSceneData.Instance.hp.GetHp() / (float)Hp.LimitHp;
        maxHp.value = SemesterSceneData.Instance.hp.GetMaxHp() / (float)Hp.LimitHp;
    }

    private void DateUpdate()
    {
        if (SemesterSceneData.Instance.clock.GetCurrentWeekCycle() is WeekCycle.Night)
        {
            date.text = GameManager.Instance.data.date.GetCurrentDate();
            return;
        }
        date.text =
            $"{GameManager.Instance.data.date.GetCurrentDate()}\n{SemesterSceneData.Instance.clock.GetCurrentTimeAsPeriod()}교시";
    }
}

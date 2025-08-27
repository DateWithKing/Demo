using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBarPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI date;
    [SerializeField] private TextMeshProUGUI lesson; //교시
    [SerializeField] private Slider currentHp;
    [SerializeField] private Slider maxHp;
    [SerializeField] private Image hpImage;
    [SerializeField] private Sprite normalHpImage;
    [SerializeField] private Sprite shortageHpImage;
    
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

        if (currentHp.value < 20)
        {
            hpImage.sprite = shortageHpImage;
        }
        else
        {
            hpImage.sprite = normalHpImage;
        }
    }

    private void DateUpdate()
    {
        if (SemesterSceneData.Instance.clock.GetCurrentWeekCycle() is WeekCycle.Night)
            lesson.text = "저녁";
        else 
            lesson.text = $"{SemesterSceneData.Instance.clock.GetCurrentTimeAsPeriod()}교시";
        date.text = GameManager.Instance.data.date.GetCurrentDate();
    }
}

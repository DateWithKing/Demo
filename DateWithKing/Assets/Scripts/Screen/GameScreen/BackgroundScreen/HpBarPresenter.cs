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
        SemesterSceneData.Instance.clock.TimeChanged -= ViewUpdate;
        SemesterSceneData.Instance.clock.TimeChanged += ViewUpdate;
    }

    private void ViewUpdate()
    {
        date.text =
            $"{GameManager.Instance.data.date.GetCurrentDate()}/n{SemesterSceneData.Instance.clock.GetCurrentTime()}";
        currentHp.value = SemesterSceneData.Instance.hp.GetHp() / (float)Hp.LimitHp;
        maxHp.value = SemesterSceneData.Instance.hp.GetMaxHp() / (float)Hp.LimitHp;
    }
}

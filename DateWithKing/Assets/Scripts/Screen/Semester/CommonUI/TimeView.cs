using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TMP가 붙은 오브젝트에 붙이면 해당 TMP에 시간을 출력해줌
public class TimeView : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Start()
    {
        SemesterSceneData.Instance.clock.TimeChanged -= ChangeTime;
        SemesterSceneData.Instance.clock.TimeChanged += ChangeTime;
    }

    void ChangeTime()
    {
        text.text = SemesterSceneData.Instance.clock.GetCurrentTime();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemesterScreenPresenter : MonoBehaviour
{
    /// <summary>
    /// 한 학기 일수
    /// </summary>
    [SerializeField] private int semesterDays = 14;

    private Screen screen;

    void Awake()
    {
        screen = GetComponent<Screen>();
    }
    void Start()
    {
        SemesterSceneData.Instance.clock.DateChanged -= SemesterEnd;
        SemesterSceneData.Instance.clock.DateChanged += SemesterEnd;
    }

    private void SemesterEnd()
    {
        if(GameManager.Instance.data.date.CountPassedDate() == semesterDays)
            screen.MoveScene("EndSemester");
    }
}

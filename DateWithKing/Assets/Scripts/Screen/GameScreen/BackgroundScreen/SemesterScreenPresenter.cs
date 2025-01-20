using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemesterScreenPresenter : MonoBehaviour
{
    /// <summary>
    /// 한 학기 일수
    /// </summary>
    [SerializeField] private int semesterDays = 10;

    private Screen screen;

    void Awake()
    {
        screen = GetComponent<Screen>();
    }
    void Start()
    {
        SemesterSceneData.Instance.clock.DateChanged -= SemesterEnd;
        SemesterSceneData.Instance.clock.DateChanged += SemesterEnd;
        SemesterSceneData.Instance.clock.DateChanged -= OneDateLater;
        SemesterSceneData.Instance.clock.DateChanged += OneDateLater;
    }

    private void OneDateLater()
    {
        //초기화 값 GameManager에서 가져오도록 수정해야 함
        SemesterSceneData.Instance.hp.InitHp(GameManager.Instance.data.stats["hp"].value * 10, GameManager.Instance.data.stats["bonusHp"].value);
        GameManager.Instance.data.stats["bonusHp"].InitStat();
    }

    private void SemesterEnd()
    {
        if(GameManager.Instance.data.date.CountPassedDate() == semesterDays)
            screen.MoveScene("EndSemester");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWeekPresenter : Screen
{
    private INextWeekView view;
    void Awake()
    {
        view = GetComponent<INextWeekView>();
    }
    void OnEnable()
    {
        view.PrintDate(GameManager.Instance.data.date);
    }
}

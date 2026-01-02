using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class NextWeekPresenter : Screen
{
    private INextWeekView view;
    void Awake()
    {
        view = GetComponent<INextWeekView>();
        LocalizationSettings.SelectedLocaleChanged -= PrintDate;
        LocalizationSettings.SelectedLocaleChanged += PrintDate;
    }
    void OnEnable()
    {
        view.PrintDate(GameManager.Instance.data.date);
    }

    private void PrintDate(Locale locale)
    {
        view.PrintDate(GameManager.Instance.data.date);
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= PrintDate;
    }
}

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

/// <summary>
/// SpotButton의 UI버전
/// </summary>
public class UI_SpotButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private string spotName;
    private int hpCost = 20;
    private Button button;
    private ButtonComment comment;

    void Awake()
    {
        spotName = name;
        button = GetComponent<Button>();
        comment = GetComponent<ButtonComment>();

        LocalizationSettings.SelectedLocaleChanged -= SetComment;
        LocalizationSettings.SelectedLocaleChanged += SetComment;
    }
    private void Start()
    {
        //말풍선 내 대사 세팅
        SetComment(LocalizationSettings.SelectedLocale);

        hpCost = SemesterSceneData.Instance.spot.deltaStat[spotName].hpCost;
        
        SemesterSceneData.Instance.hp.CurrentHpChanged -= EnableCheck;
        SemesterSceneData.Instance.hp.CurrentHpChanged += EnableCheck;
    }

    private void OnEnable()
    {
        EnableCheck();
    }

    private void SetComment(Locale newLocale)
    {
        SpotData spotData = SemesterSceneData.Instance.GetSpotData(newLocale);
        string text = "<b>";
        foreach (var data in spotData.deltaStat[spotName].GetDeltaData())
        {
            text += $"{data.Key} {data.Value.ToString()} ";
        }

        text += "</b>";
        text += $"\n{spotData.deltaStat[spotName].comment}";
        comment.SetComment(text);
    }

    private void EnableCheck()
    {
        if (SemesterSceneData.Instance.hp.GetHp() < hpCost)
        {
            button.interactable = false;
        }
        else button.interactable = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(button.interactable) SoundManager.Instance.PlaySFX("장소_활동_호버");
        else SoundManager.Instance.PlaySFX("버튼_선택불가");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(button.interactable) SoundManager.Instance.PlaySFX("장소_활동_클릭");
        else SoundManager.Instance.PlaySFX("버튼_선택불가");
    }
}

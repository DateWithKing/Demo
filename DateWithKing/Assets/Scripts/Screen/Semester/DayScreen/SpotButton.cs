using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

/// <summary>
/// 장소/활동 - 버튼과 관련된 스크립트 <br/>
/// 버튼과 관련된 모든 책임을 가짐 <br/>
/// 봐서 장소명을 그냥 오브젝트에서 긁어와도 될 것 같기도...
/// </summary>
public class SpotButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private string spotName;
    private int hpCost = 20;
    private ObjectButton button;
    private ButtonComment comment;

    void Awake()
    {
        spotName = name;
        button = GetComponent<ObjectButton>();
        comment = GetComponent<ButtonComment>();

        LocalizationSettings.SelectedLocaleChanged -= SetComment;
        LocalizationSettings.SelectedLocaleChanged += SetComment;
    }
    private void Start()
    {
        SetComment(LocalizationSettings.SelectedLocale);

        hpCost = SemesterSceneData.Instance.spot.deltaStat[spotName].hpCost;
        
        SemesterSceneData.Instance.hp.CurrentHpChanged -= EnableCheck;
        SemesterSceneData.Instance.hp.CurrentHpChanged += EnableCheck;
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
            button.isInteractable = false;
        }
        else button.isInteractable = true;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(button.isInteractable) SoundManager.Instance.PlaySFX("장소_활동_호버");
        else SoundManager.Instance.PlaySFX("버튼_선택불가");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(button.isInteractable) SoundManager.Instance.PlaySFX("장소_활동_클릭");
        else SoundManager.Instance.PlaySFX("버튼_선택불가");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장소의 행동을 관리
/// </summary>
public class SpotPresenter : Presenter
{
    public const string SpotImagePath = "Sprites/Spot/";
    [SerializeField] private Image spotImage;
    [SerializeField] private GiftView giftSystem;
    private string currentSpot = "";
    
    public void ChangeSpot(string spot)
    {
        currentSpot = spot;
        spotImage.sprite = Resources.Load<Sprite>(SpotImagePath + spot);
        gameObject.SetActive(true);
        
        CursorHandler.ChangeCursor();

        string spotData = DayDialogueData.GetSpotIndex(currentSpot);
        
        if (SemesterSceneData.Instance.DayDialogue.spotCharacters.ContainsKey(spotData))
        {
            Debug.Log($"{spotData}에 방문해 {SemesterSceneData.Instance.DayDialogue.spotCharacters[spotData]}을/를 만났습니다.");
            
            giftSystem.StartPresent(SemesterSceneData.Instance.DayDialogue.spotCharacters[spotData].ToString());
            SemesterSceneData.Instance.hp.UseHp(SemesterSceneData.Instance.spot.deltaStat[spot].hpCost);
            
            Debug.Log($"체력을 사용했습니다. 현재 체력 : {SemesterSceneData.Instance.hp.GetHp()}");
        }
        else
        {
            Debug.Log("아무도 없습니다.");
        }
    }
    
    public void StartDialogue()
    {
        YarnManager.Instance.RunDialogue(GetDialogueIndex());
    }

    private void OnDisable()
    {
        CursorHandler.ReturnCursor();
        SemesterSceneData.Instance.clock.NextTime();
    }

    private string GetDialogueIndex()
    {
        return $"주{GameManager.Instance.data.date.GetCurrentWeek().ToString()}" +
            $"_{GameManager.Instance.data.date.GetCurrentDays().ToString()}" +
            $"_{SemesterSceneData.Instance.clock.GetCurrentTimeAsPeriod()}교시" +
            $"_{currentSpot}" +
            $"_{SemesterSceneData.Instance.DayDialogue.spotCharacters[DayDialogueData.GetSpotIndex(currentSpot)]}";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightActivityPresenter : Presenter
{
    private Screen _screen;
    public const string SpotImagePath = "Sprites/Spot/";
    [SerializeField] private Image meetGirls;
    private string currentActivity;

    private Screen screen
    {
        get
        {
            if (_screen == null) _screen = GetComponent<Screen>();
            return _screen;
        }
        set => _screen = value;
    }

    void Awake()
    {
        screen = GetComponent<Screen>();
    }

    public void DoActivity(string activity)
    {
        currentActivity = activity;
        screen.ShowScreen();
        if(GameManager.Instance.data.date.GetCurrentWeek() == 3) YarnManager.Instance.RunDialogue("밤_3_무관");
        else MeetGirls();
    }
    public void PrintActivityDialogue()
    {
        string dialogue = $"밤_{currentActivity}";
        SemesterSceneData.Instance.hp.UseHp(SemesterSceneData.Instance.spot.deltaStat[currentActivity].hpCost);
            
        Debug.Log($"체력을 사용했습니다. 현재 체력 : {SemesterSceneData.Instance.hp.GetHp()}");
        YarnManager.Instance.RunDialogue(dialogue);
    }
    
    public void MeetGirls()
    {
        string spotData = $"밤_{GameManager.Instance.data.date.GetCurrentWeek()}_{currentActivity}";
        if (SemesterSceneData.Instance.DayDialogue.spotCharacters.ContainsKey(spotData))
        {
            Debug.Log($"{spotData}에 방문해 {SemesterSceneData.Instance.DayDialogue.spotCharacters[spotData]}을/를 만났습니다.");
            spotData += $"_{SemesterSceneData.Instance.DayDialogue.spotCharacters[spotData]}";
            meetGirls.gameObject.SetActive(true);
            meetGirls.sprite = Resources.Load<Sprite>(SpotImagePath + currentActivity);
            YarnManager.Instance.RunDialogue(spotData);
        }
        else
        {
            meetGirls.gameObject.SetActive(false);
            Debug.Log("아무도 없습니다.");
            PrintActivityDialogue();
        }
    }
}

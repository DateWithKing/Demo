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
        MeetGirls();
    }
    public void PrintActivityDialogue()
    {
        string dialogue = $"밤_{currentActivity}";
        YarnManager.Instance.RunDialogue(dialogue);
    }
    
    public void MeetGirls()
    {
        string spotData = $"밤_{GameManager.Instance.data.date.GetCurrentWeek()}_{currentActivity}";
        if (SemesterSceneData.Instance.DayDialogue.spotCharacters.ContainsKey(spotData))
        {
            meetGirls.gameObject.SetActive(true);
            meetGirls.sprite = Resources.Load<Sprite>(SpotImagePath + currentActivity);
            Debug.Log($"{spotData}에 방문해 {SemesterSceneData.Instance.DayDialogue.spotCharacters[spotData]}을/를 만났습니다.");
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

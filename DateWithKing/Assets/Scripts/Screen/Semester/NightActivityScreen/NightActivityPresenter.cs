using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightActivityPresenter : Presenter
{
    private Screen _screen;
    public const string SpotImagePath = "Sprites/Spot/";
    [SerializeField] private Image spotImage;

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
        spotImage.sprite = Resources.Load<Sprite>(SpotImagePath + "밤");
        screen.ShowScreen();
        string dialogue = $"밤_{activity}";
        YarnManager.Instance.RunDialogue(dialogue, delegate { MeetGirls(activity);});
    }
    
    private void MeetGirls(string activity)
    {
        string spotData = $"밤_{GameManager.Instance.data.date.GetCurrentWeek()}_{activity}";
        
        if (SemesterSceneData.Instance.DayDialogue.spotCharacters.ContainsKey(spotData))
        {
            spotImage.sprite = Resources.Load<Sprite>(SpotImagePath + activity);
            Debug.Log($"{spotData}에 방문해 {SemesterSceneData.Instance.DayDialogue.spotCharacters[spotData]}을/를 만났습니다.");
            YarnManager.Instance.RunDialogue(spotData);
        }
        else
        {
            Debug.Log("아무도 없습니다.");
        }
    }
}

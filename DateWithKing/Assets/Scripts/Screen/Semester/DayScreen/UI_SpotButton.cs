using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SpotButton의 UI버전
/// </summary>
public class UI_SpotButton : MonoBehaviour
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
    }
    private void Start()
    {
        //말풍선 내 대사 세팅
        string text = "<b>";
        foreach (var data in SemesterSceneData.Instance.spot.deltaStat[spotName].GetDeltaData())
        {
            text += $"{data.Key} {data.Value.ToString()} ";
        }

        text += "</b>";
        text += $"\n{SemesterSceneData.Instance.spot.deltaStat[spotName].comment}";
        comment.SetComment(text);

        hpCost = SemesterSceneData.Instance.spot.deltaStat[spotName].hpCost;
        
        SemesterSceneData.Instance.hp.CurrentHpChanged -= EnableCheck;
        SemesterSceneData.Instance.hp.CurrentHpChanged += EnableCheck;
    }

    private void EnableCheck()
    {
        if (SemesterSceneData.Instance.hp.GetHp() < hpCost)
        {
            button.interactable = false;
        }
        else button.interactable = true;
    }
}

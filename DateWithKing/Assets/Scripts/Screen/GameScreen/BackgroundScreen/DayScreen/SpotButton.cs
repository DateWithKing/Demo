using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장소/활동 - 버튼과 관련된 스크립트 <br/>
/// 버튼과 관련된 모든 책임을 가짐 <br/>
/// 봐서 장소명을 그냥 오브젝트에서 긁어와도 될 것 같기도...
/// </summary>
public class SpotButton : MonoBehaviour
{
    [SerializeField] private string spotName;
    private int hpCost = 20;
    //private Button button;
    private ButtonComment comment;

    void Awake()
    {
        //button = GetComponent<Button>();
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
        /*if (SemesterSceneData.Instance.hp.GetHp() < hpCost)
        {
            button.interactable = false;
        }
        else button.interactable = true;*/
    }
}

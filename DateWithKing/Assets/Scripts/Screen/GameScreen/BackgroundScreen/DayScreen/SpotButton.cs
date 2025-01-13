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
    [SerializeField] private int hpCost = 20;
    private Button button;
    private ButtonComment comment;

    void Awake()
    {
        button = GetComponent<Button>();
        comment = GetComponent<ButtonComment>();
    }
    private void Start()
    {
        //코멘트에 스탯 변경사항 써달라고 하면 해주기..ㅎㅎ
        comment.SetComment(SemesterSceneData.Instance.spot.deltaStat[spotName].comment);
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

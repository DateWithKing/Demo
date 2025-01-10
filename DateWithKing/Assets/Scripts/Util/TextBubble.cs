using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 말풍선을 컨트롤하는 스크립트 <br/>
/// 말풍선 조정과 관련된 모든 책임은 해당 스크립트에 있음
/// </summary>
public class TextBubble : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    /// <summary>
    /// 말풍선 텍스트를 지정한다.
    /// </summary>
    /// <param name="comment"></param>
    public void SetText(string comment)
    {
        text.text = comment;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 설명을 팝업하고 싶은 버튼이 있는 오브젝트에 부착 시 말풍선을 팝업
/// </summary>
public class ButtonComment : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string comment;
    private GameObject textBubblePrefab;
    private TextBubble text;
    private GameObject textBubble;
    public Transform canvasObject;

    void Awake()
    {
        textBubblePrefab = Resources.Load<GameObject>("Module/TextBubble");
        text = textBubblePrefab.GetComponent<TextBubble>();
    }

    /// <summary>
    /// 말풍선에 출력할 문구를 바꾼다.
    /// </summary>
    /// <param name="comment"></param>
    public void SetComment(string comment)
    {
        this.comment = comment;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.SetText(comment);
        textBubble = Instantiate(textBubblePrefab, canvasObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(textBubble); 
    }
}

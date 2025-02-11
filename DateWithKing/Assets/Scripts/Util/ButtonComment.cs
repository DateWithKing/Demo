using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

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
        // 말풍선 텍스트 설정
        text.SetText(comment);

        textBubble = Instantiate(textBubblePrefab, canvasObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(textBubble is not null)
            Destroy(textBubble); 
    }

    public void OnDisable()
    {
        if(textBubble is not null)
            Destroy(textBubble);
    }
}
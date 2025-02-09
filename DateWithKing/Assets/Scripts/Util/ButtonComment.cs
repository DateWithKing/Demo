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

    private Coroutine destroyCoroutine; // 파괴 타이머를 위한 Coroutine

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
        
        if(textBubble is null) textBubble = Instantiate(textBubblePrefab, canvasObject);
        else textBubble.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textBubble.SetActive(false);
        //Destroy(textBubble); 
    }
}
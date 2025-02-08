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

        // 기존의 파괴 코루틴이 있으면 중단
        if (destroyCoroutine != null)
        {
            StopCoroutine(destroyCoroutine);
        }
        else
            textBubble = Instantiate(textBubblePrefab, canvasObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 1초 후에 말풍선을 파괴하는 코루틴 시작
        destroyCoroutine = StartCoroutine(DestroyTextBubbleAfterDelay(0.5f));
    }

    private IEnumerator DestroyTextBubbleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(textBubble); 
        destroyCoroutine = null; // 파괴 후 코루틴 null로 설정
    }
}
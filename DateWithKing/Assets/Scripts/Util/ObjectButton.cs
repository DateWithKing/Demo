using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent OnClick;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    private SpriteRenderer sprite;
    private bool interactable = true;
    public bool isInteractable
    {
        get
        {
            return interactable;
        }
        set
        {
            //grayscale 필요 시 주석 제거
            // if(value) sprite.color = Color.white;
            //else sprite.color = Color.gray;
            interactable = value;
        }
    }

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke(); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExit?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable)
        {
            Debug.Log("해당 장소와 상호작용할 수 없습니다.");
            return;
        }
        OnClick?.Invoke();
    }
}

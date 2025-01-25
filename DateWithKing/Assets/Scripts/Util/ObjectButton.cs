using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent OnClick;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    
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
        OnClick?.Invoke();
    }
}

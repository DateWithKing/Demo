using UnityEngine;
using UnityEngine.EventSystems;

public class UI_DoubleClickButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX("UI버튼_호버");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2)
            SoundManager.Instance.PlaySFX("UI버튼_클릭");
    }
}

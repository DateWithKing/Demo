using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI 버튼 클릭 시 사운드용
/// </summary>
public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX("UI버튼_호버");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX("UI버튼_클릭");
    }
}

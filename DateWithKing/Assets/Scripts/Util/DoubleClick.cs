using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int index;
    public void OnPointerClick(PointerEventData eventData)
    {

        ChatManager.Instance.ChooseEmoticon(index);
        SoundManager.Instance.PlaySFX("폰_이모티콘");

    }


}

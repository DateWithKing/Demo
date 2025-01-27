using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 상점 아이템 슬롯 UI
/// </summary>
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private const string ItemImagePath = "Item/";
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Image image;
    [SerializeField] private Image type;

    private int itemId;
    
    public event Action<int> BuyItem;
    
    public void InitSlot(Item item)
    {
        itemId = item.id;
        name.text = item.name;
        description.text = item.description;
        price.text = item.price.ToString();
        image.sprite = Resources.Load<Sprite>(ItemImagePath + item.name);
        image.sprite = Resources.Load<Sprite>(ItemImagePath + item.type.ToString());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //더블 클릭 감지
        if (eventData.clickCount == 2)
        {
            BuyItem?.Invoke(itemId);
        }
    }
}

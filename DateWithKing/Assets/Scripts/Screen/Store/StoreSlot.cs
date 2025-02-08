using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Image image;
    [SerializeField] private Image type;
    
    private const string ItemImagePath = "Sprites/Item/"; 

    private int itemId;
    
    public event Action<int> DoubleClick;

    public void InitSlot(Item item)
    {
        itemId = item.id;
        name.text = item.name;
        description.text = item.description;
        price.text = item.price.ToString();
        image.sprite = Resources.Load<Sprite>(ItemImagePath + item.name);
        type.sprite = Resources.Load<Sprite>(ItemImagePath + item.type.ToString());
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2) DoubleClick?.Invoke(itemId);
    }
}

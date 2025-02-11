using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image image;
    
    private const string ItemImagePath = "Sprites/Item/";

    private int slot;
    public event Action<int> Click;

    public void InitSlot(int slot, Item item)
    {
        this.slot = slot;
        Sprite sprite = Resources.Load<Sprite>(ItemImagePath + item.name);
        if(sprite == null) image.color = Color.clear;
        else
        {
            image.color = Color.white;
            image.sprite = sprite;
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 1) Click?.Invoke(slot);
    }
}

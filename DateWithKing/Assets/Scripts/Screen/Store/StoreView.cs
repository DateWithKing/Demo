using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreView : MonoBehaviour, IStoreView
{
    [SerializeField] private GameObject itemSlotPrefab;
    private List<ItemSlot> items = new List<ItemSlot>();
    public event Action<int> BuyItem = null;

    public void InitStore()
    {
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
    }

    public void RegisterItem(Item item)
    {
        ItemSlot slot = Instantiate(itemSlotPrefab, transform).GetComponent<ItemSlot>();
        slot.InitSlot(item);
        items.Add(slot);
        slot.BuyItem -= BuyItem;
        slot.BuyItem += BuyItem;
    }
}

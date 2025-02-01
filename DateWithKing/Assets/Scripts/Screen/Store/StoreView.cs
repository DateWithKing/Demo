using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreView : MonoBehaviour, IStoreView
{
    [SerializeField] private GameObject itemSlotPrefab;
    private List<StoreSlot> items = new List<StoreSlot>();
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
        StoreSlot slot = Instantiate(itemSlotPrefab, transform).GetComponent<StoreSlot>();
        slot.InitSlot(item);
        items.Add(slot);
        slot.DoubleClick -= BuyItem;
        slot.DoubleClick += BuyItem;
    }
}

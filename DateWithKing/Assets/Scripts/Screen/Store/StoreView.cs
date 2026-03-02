using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreView : MonoBehaviour, IStoreView
{
    [SerializeField] private GameObject itemSlotPrefab;
    private Dictionary<int, StoreSlot> items = new Dictionary<int, StoreSlot>();
    public event Action<int> BuyItem = null;

    public void InitStore()
    {
        foreach (var item in items)
        {
            Destroy(item.Value.gameObject);
        }
    }

    public void UpdateItem(int index, Item item)
    {
        items[index].InitSlot(item);
    }

    public void RegisterItem(Item item)
    {
        StoreSlot slot = Instantiate(itemSlotPrefab, transform).GetComponent<StoreSlot>();
        slot.InitSlot(item);
        items[item.id] = slot;
        slot.DoubleClick -= BuyItem;
        slot.DoubleClick += BuyItem;
    }
}

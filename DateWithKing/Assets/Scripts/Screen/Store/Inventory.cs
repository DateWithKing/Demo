using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Inventory
{
    [JsonProperty]
    private List<Item> items = new List<Item>();
    private readonly Item nullItem = new Item();
    public const int Inventory_Capacity = 4;

    public event Action OnItemChanged = null;
    public event Action<int> OnUseItem = null;

    public bool CanAddItem()
    {
        if (items.Count >= Inventory_Capacity) return false;
        return true;
    }

    public void AddItem(Item item)
    {
        if (items.Count >= Inventory_Capacity) return;
        items.Add(item);
        OnItemChanged?.Invoke();
    }

    public void UseItem(int slot)
    {
        if (items.Count <= slot) return;
        items[slot].UseItem();
        OnUseItem?.Invoke(items[slot].id);
        items[slot] = nullItem;
        OnItemChanged?.Invoke();
    }

    //이거 때문에 현재 매우 위험함...
    //개선할 시간이 있다면 개선할 것.
    //Item 내부 함수를 사용하면 안 됨...
    public Item GetItem(int slot)
    {
        if (items.Count <= slot) return nullItem;
        return items[slot];
    }
}

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

    public Action OnItemChanged = null;

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
        items[slot] = nullItem;
        OnItemChanged?.Invoke();
    }

    public Item GetItem(int slot)
    {
        if (items.Count <= slot) return nullItem;
        return items[slot];
    }
}

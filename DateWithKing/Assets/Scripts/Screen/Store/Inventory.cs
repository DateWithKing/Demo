using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Inventory
{
    [JsonProperty]
    private List<Item> items = new List<Item>();
    private Item nullItem = new Item();
    private const int Inventory_Capacity = 4;

    public event Action OnItemAdd;

    public bool CanAddItem()
    {
        if (items.Count >= Inventory_Capacity) return false;
        return true;
    }

    public void AddItem(Item item)
    {
        if (items.Count >= Inventory_Capacity) return;
        items.Add(item);
        OnItemAdd?.Invoke();
    }

    public Item GetItem(int slot)
    {
        if (items.Count <= slot) return nullItem;
        return items[slot];
    }
}

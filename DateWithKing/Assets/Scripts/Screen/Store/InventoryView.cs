using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour, IInventoryView
{
    [SerializeField] private GameObject itemSlotPrefab;
    private List<InventorySlot> items = new List<InventorySlot>();

    public void Awake()
    {
        for (int i = 0; i < Inventory.Inventory_Capacity; i++)
        {
            items.Add(Instantiate(itemSlotPrefab, transform).GetComponent<InventorySlot>());
        }
    }
    
    public void UpdateView()
    {
        for (int i = 0; i < Inventory.Inventory_Capacity; i++)
        { 
            items[i].InitSlot(i, GameManager.Instance.data.inventory.GetItem(i));
        }
    }

    public void OnClick(Action<int> click)
    {
        for (int i = 0; i < Inventory.Inventory_Capacity; i++)
        {
            items[i].Click += click;
        }
    }
}

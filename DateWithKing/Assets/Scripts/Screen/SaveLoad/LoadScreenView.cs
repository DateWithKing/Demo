using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreenView : View, ILoadScreenView
{
    [SerializeField] private List<Slot> slots;
    public event Action<int> ClickSlot;
    
    void Start()
    {
        foreach (Slot slot in slots)
        {
            slot.slot.onClick.AddListener(() => { ClickSlot?.Invoke(slot.slotID); });
        }
    }

    public void DisableSlot(int index)
    {
        slots[index].slot.interactable = false;
    }


    /// <summary>
    /// ȭ�鿡 ���� ������ ���
    /// </summary>
    /// <param name="slot"> ����� ���� ��ȣ (0, 1, 2 ��) </param>
    /// <param name="slotDTO"> ����� ���� ������ </param>
    public void PrintSlot(int slot, SlotDTO slotDto)
    {
        slots[slot].PrintSlot(slot, slotDto);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ILoadScreenView
{
    public void DisableSlot(int index);
    public void PrintSlot(int slot, SlotDTO slotDTO);

    public event Action<int> ClickSlot;
}

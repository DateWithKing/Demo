using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreenView : View, ILoadScreenView
{

    [SerializeField] private Button slot;
    
    Slot slotInstance = new Slot();



    public event Action<int> ClickSlot;
    

    void Start()
    {
        int slotID = slotInstance.slotID;
        slot.onClick.AddListener(() => { ClickSlot?.Invoke(slotID); });
    }


    /// <summary>
    /// 화면에 슬롯 데이터 출력
    /// </summary>
    /// <param name="slot"> 출력할 슬롯 번호 (0, 1, 2 순) </param>
    /// <param name="slotDTO"> 출력할 슬롯 데이터 </param>
    public void PrintSlot(int slot, SlotDTO slotDTO)
    {
        slotInstance.PrintSlot(slot, slotDTO);

    }
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    [field: SerializeField] public int slotID { get; private set; }

    [SerializeField] private TextMeshProUGUI week;
    [SerializeField] private TextMeshProUGUI day;
    [SerializeField] private Image polaroidImage;


    internal void PrintSlot(int slot, SlotDTO slotDTO)
    {
        if (slot == slotID)
        {
            week.text = slotDTO.week.ToString();
            day.text = slotDTO.day.ToString();
            polaroidImage = slotDTO.polaroidImage;


        }
    }
}

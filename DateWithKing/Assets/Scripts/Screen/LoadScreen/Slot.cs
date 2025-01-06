using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    [field: SerializeField] public int slotID { get; private set; }

    [field: SerializeField] public Button slot { get; private set; }
    [SerializeField] private TextMeshProUGUI date;
    [SerializeField] private Sprite polaroidImage;


    internal void PrintSlot(int slot, SlotDTO slotDTO)
    {
        if (slot == slotID)
        {
            date.text = slotDTO.date.ToString();
            polaroidImage = slotDTO.polaroidImage;


        }
    }
}

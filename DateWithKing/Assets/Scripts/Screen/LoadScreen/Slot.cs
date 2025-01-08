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
    [SerializeField] private Image polaroidImage;


    internal void PrintSlot(int slot, SlotDTO slotDTO)
    {
        if (slot != slotID)
        {
            Debug.LogWarning($"Slot ID mismatch: Expected {slotID}, but got {slot}");
            return;
        }

        if (slotDTO == null)
        {
            Debug.LogError("SlotDTO is null! Cannot print slot data.");
            return;
        }

        if (date != null)
        {
            date.text = string.IsNullOrEmpty(slotDTO.date) ? "No Date" : slotDTO.date;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI (date) is not assigned in the Inspector.");
        }

        if (polaroidImage != null)
        {
            polaroidImage.sprite = slotDTO.polaroidImage ?? Resources.Load<Sprite>("DefaultImage");
        }
        else
        {
            Debug.LogError("polaroidImage is not assigned in the Inspector.");
        }
    }
}

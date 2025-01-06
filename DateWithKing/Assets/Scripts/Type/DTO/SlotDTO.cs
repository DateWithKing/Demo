using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotDTO
{
    public string week;
    public string day;
    public Image polaroidImage;

    public SlotDTO(string week, string day)
    {
        this.week = week;
        this.day = day;
    }

    public SlotDTO(Image polaroidImage)
    {
        this.polaroidImage = polaroidImage;
    }
}

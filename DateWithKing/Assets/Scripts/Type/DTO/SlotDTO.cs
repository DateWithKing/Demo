using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class SlotDTO
{
    public string date = "";
    public Sprite polaroidImage = null; // 기본 무표정

    public SlotDTO() { }

    public SlotDTO(string date, Sprite polaroidImage)
    {
        this.date = date;
        this.polaroidImage = polaroidImage;
    }
}
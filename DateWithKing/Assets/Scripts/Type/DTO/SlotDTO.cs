using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class SlotDTO
{
    public string date = "";
    public Sprite polaroidImage = null; // 기본 무표정
    public StatDataDTO statData = new StatDataDTO();

    public SlotDTO() { }

    public SlotDTO(string date)
    {
        this.date = date;
        
    }

    public SlotDTO(Sprite polaroidImage)
    {
        this.polaroidImage = polaroidImage;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotDTO
{
    public string date;
    public Sprite polaroidImage;

    public SlotDTO(string date)
    {
        this.date = date;
        
    }

    public SlotDTO(Sprite polaroidImage)
    {
        this.polaroidImage = polaroidImage;
    }
}

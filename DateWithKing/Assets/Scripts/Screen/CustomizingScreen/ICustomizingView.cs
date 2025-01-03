using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICustomizingView
{
    public event UnityAction StartGame;
    public void SetCustomizingData(CustomizingDTO customizingDto);
    public CustomizingDTO GetCustomizingData();
}

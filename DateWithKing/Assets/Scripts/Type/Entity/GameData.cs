using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 상태를 정의하는 동적 데이터(Entity)<br/>
/// </summary>
public class GameData : Entity
{
    public string name { get; private set; }

    public void SetCustomizingData(CustomizingDTO customizingDto)
    {
        name = customizingDto.name;
    }
    
    public GameData(CustomizingDTO customizingDto)
    {
        SetCustomizingData(customizingDto);
    }

    public GameData()
    {
        
    }
}

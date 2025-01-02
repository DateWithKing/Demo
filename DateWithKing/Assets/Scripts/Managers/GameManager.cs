using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameData data { get; set; }

    /// <summary>
    /// 최초 게임 데이터 세팅 <br/>
    /// </summary>
    public void InitData(CustomizingDTO customizingDto)
    {
        data = DataLoader.ReadData<GameData>();
        data.SetCustomizingData(customizingDto);
    }
    
    /// <summary>
    /// 세이브 데이터 로드 <br/>
    /// </summary>
    /// <param name="slot">세이브 데이터 슬롯 번호</param>
    public void LoadData(int slot)
    {
        GameData load = DataLoader.ReadData<GameData>((Define.DynamicData)slot);
    }

    /// <summary>
    /// 세이브 데이터 저장
    /// </summary>
    /// <param name="slot"> 세이브 데이터 슬롯 번호</param>
    public void SaveData(int slot)
    {
        DataLoader.WriteData((Define.DynamicData)slot, data);
    }
}

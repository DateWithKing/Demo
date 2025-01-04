using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameData data { get; set; } = new GameData();

    /// <summary>
    /// 최초 게임 데이터 세팅 <br/>
    /// </summary>
    public void InitData(CustomizingDTO customizingDto)
    {
        //1-3
        //데이터를 추가하셨다면 아래 형식에 맞게 작성해주세요~
        GameData tempData = new GameData();
        tempData.date = new Date();
        tempData.customizing = new CustomizingDTO();
        tempData.stat = new PlayerStatDTO();
        tempData.setting = new SettingDTO();
        data = tempData;
        //data = DataLoader.ReadData<GameData>();
        data.SetCustomizingData(customizingDto);
    }
    
    /// <summary>
    /// 세이브 데이터 로드 <br/>
    /// </summary>
    /// <param name="slot">세이브 데이터 슬롯 번호</param>
    public void LoadData(int slot)
    {
        GameData load = DataLoader.ReadData<GameData>((DynamicData)slot);
    }

    /// <summary>
    /// 세이브 데이터 저장
    /// </summary>
    /// <param name="slot"> 세이브 데이터 슬롯 번호</param>
    public void SaveData(int slot)
    {
        DataLoader.WriteData((DynamicData)slot, data);
    }
}

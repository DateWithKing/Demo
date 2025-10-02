using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameData data { get; set; } = new GameData();
    public PermanentData PermanentData { get; set; } = new PermanentData();

    public int ticket = 0;

    void Start()
    {
        PermanentData = DataLoader.ReadData<PermanentData>(DynamicData.Permanent) ?? PermanentData;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("OpeningScene");
        }
    }

    /// <summary>
    /// 최초 게임 데이터 세팅 <br/>
    /// </summary>
    public void InitData()
    {
        //1-3
        //데이터를 추가하셨다면 아래 형식에 맞게 작성해주세요~
        GameData tempData = new GameData();
        data = tempData;
        //data = DataLoader.ReadData<GameData>();

        ticket = 0;
    }
    
    /// <summary>
    /// 세이브 데이터를 로드하여 GameManager.Instance.data를 교체 <br/>
    /// 즉, 현재 게임 상태를 덮어씀
    /// </summary>
    /// <param name="slot">세이브 데이터 슬롯 번호</param>
    public void LoadData(int slot)
    {
        data = DataLoader.ReadData<GameData>((DynamicData)slot);
        if (data == null)
        {
            InitData();
        }
    }

    /// <summary>
    /// 현재 GameManager.Instance.data를 세이브 데이터로 저장
    /// 즉, 현재 게임 상태를 세이브 데이터에 저장함
    /// </summary>
    /// <param name="slot"> 세이브 데이터 슬롯 번호</param>
    public void SaveData(int slot)
    {
        DataLoader.WriteData((DynamicData)slot, data);
    }
    
    private void OnApplicationQuit()
    {
        PermanentData.SavePermanentData();
    }
}
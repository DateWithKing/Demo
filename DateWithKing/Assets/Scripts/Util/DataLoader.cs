using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

public class Entity
{
}

/// <summary>
/// 동적/정적 데이터를 Read/Write 함
/// </summary>
public static class DataLoader
{
    private const string StaticDataPath = "StaticData/";
    private static readonly string DynamicDataPath = Application.persistentDataPath + "/";

    /// <summary>
    /// 정적 데이터를 T 오브젝트로 반환. <br/>
    /// 해당 데이터는 Resources/StaticData/에 클래스명으로 존재해야 함
    /// </summary>
    /// <typeparam name="T">읽어올 오브젝트의 클래스 <br/>
    /// Entity 클래스를 상속받은 클래스만 읽어올 수 있음
    /// </typeparam>
    /// <returns></returns>
    public static T ReadData<T>() where T : Entity
    {
        TextAsset jsonData = Resources.Load<TextAsset>(StaticDataPath + typeof(T).Name);
        T data = JsonConvert.DeserializeObject<T>(jsonData.text);
        return data;
    }

    /// <summary>
    /// 동적 데이터를 T 오브젝트로 반환. <br/>
    /// </summary>
    /// <param name="name">데이터 저장명. <br/>
    /// enum DynamicData에 정의 후 사용 가능</param>
    /// <typeparam name="T">읽어올 오브젝트의 클래스</typeparam>
    /// <returns></returns>
    public static T ReadData<T>(Define.DynamicData name)
    {
        string jsonData = File.ReadAllText(DynamicDataPath + name + ".json"); 
        T data = JsonConvert.DeserializeObject<T>(jsonData);
        return data;
    }

    /// <summary>
    /// 동적 데이터를 저장합니다.
    /// </summary>
    /// <param name="name">데이터 저장명 </param>
    /// <param name="data">저장할 오브젝트 </param>
    /// <typeparam name="T">저장할 오브젝트의 클래스</typeparam>
    public static void WriteData<T>(Define.DynamicData name, T data)
    {
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(DynamicDataPath + name + ".json", jsonData);
    }
}

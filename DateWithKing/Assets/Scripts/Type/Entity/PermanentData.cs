using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PermanentData : Entity
{
    public void UpdateGallery(string spriteName)
    {
        if (!gallery.ContainsKey(spriteName)) return;
        
        gallery[spriteName] = true;
        DataLoader.WriteData(DynamicData.Permanent, this);
    }
    public Dictionary<string, bool> gallery { get; }= new Dictionary<string, bool>()
    {
        { "서은표_진엔딩", false },
        { "서은표_일반엔딩", false },
        { "서은표_종강총회", false },
        { "서은표_특수", false },
        { "양나현_진엔딩", false },
        { "양나현_일반엔딩", false },
        { "양나현_종강총회", false },
        { "양나현_특수", false },
        { "신아산_진엔딩", false },
        { "신아산_일반엔딩", false },
        { "신아산_종강총회", false },
        { "신아산_특수", false },
        { "서은표_납치엔딩", false }
    };
}

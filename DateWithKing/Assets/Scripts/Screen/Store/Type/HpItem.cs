using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class HpItem : Item
{
    [JsonProperty] public int hp { get; private set; } = 0;
    public override void UseItem()
    {
        Debug.Log("Hp 아이템 사용");
    }
}

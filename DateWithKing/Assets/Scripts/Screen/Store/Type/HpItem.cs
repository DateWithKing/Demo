using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class HpItem : Item
{
    [JsonProperty] public int hp { get; private set; } = 0;
    public override void UseItem()
    {
        Debug.Log("아이템을 사용해 체력을 회복했다: " + hp);
        GameManager.Instance.data.stats["hp"].ChangeStat(hp);
    }
}

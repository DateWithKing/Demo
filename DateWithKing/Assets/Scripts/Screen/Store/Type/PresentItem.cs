using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PresentItem : Item
{
    [JsonProperty] public int lvSan { get; private set; } = 0;
    [JsonProperty] public int lvHyun { get; private set; } = 0;
    [JsonProperty] public int lvPyo { get; private set; } = 0;
    public override void UseItem()
    {
        Debug.Log("Present 아이템 사용");
    }
}

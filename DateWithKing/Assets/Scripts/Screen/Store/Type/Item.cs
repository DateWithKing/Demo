using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Item
{
    [JsonProperty] public int id { get; private set; }= 0;
    [JsonProperty] public string name { get; private set; }= "";
    [JsonProperty] public string description { get; private set; }= "";
    [JsonProperty] public int price { get; private set; } = 0;
    [JsonProperty] public ItemType type { get; private set; } = ItemType.none;
    public virtual void UseItem()
    {
        
    }
}
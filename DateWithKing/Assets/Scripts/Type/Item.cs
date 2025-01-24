using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Item
{
    [JsonProperty] private int id;
    [JsonProperty] private string name;
    [JsonProperty] private string description;
    [JsonProperty] private int price;
    [JsonProperty] private ItemType type;
}
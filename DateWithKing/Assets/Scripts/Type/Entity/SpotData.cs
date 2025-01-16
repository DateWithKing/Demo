using System.Collections.Generic;
using Newtonsoft.Json;


public class SpotData : Entity
{
    [JsonProperty]
    public Dictionary<string, SpotDTO> deltaStat { get; private set; } 
}

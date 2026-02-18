using System.Collections.Generic;
using Newtonsoft.Json;


public class SpotData : LocalizationEntity
{
    [JsonProperty]
    public Dictionary<string, SpotDTO> deltaStat { get; private set; } 
}

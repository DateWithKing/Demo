using System.Collections.Generic;
using Newtonsoft.Json;


public class SpotData : Entity
{
    [JsonProperty]
    public Dictionary<string, DeltaStatDTO> deltaStat { get; private set; } 
}

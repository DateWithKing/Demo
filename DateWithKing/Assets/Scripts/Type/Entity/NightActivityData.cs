using System.Collections.Generic;
using Newtonsoft.Json;


public class NightActivityData : Entity
{
    [JsonProperty]
    public Dictionary<string, DeltaStatDTO> deltaStat { get; private set; } 
}

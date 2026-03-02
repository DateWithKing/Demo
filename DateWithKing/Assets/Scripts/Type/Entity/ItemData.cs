using System.Collections.Generic;
using Newtonsoft.Json;

public class ItemData : LocalizationEntity
{
    [JsonProperty]
    public Dictionary<int, HpItem> hpItem;
    
    [JsonProperty]
    public Dictionary<int, PresentItem> presentItem;

    //타입이 늘어나야 할 시 리플렉션으로 가져올 것
    public Dictionary<int, Item> GetItems()
    {
        Dictionary<int, Item> items = new Dictionary<int, Item>();

        foreach (var item in hpItem)
        {
            items.Add(item.Key, item.Value);
        }
        
        foreach (var item in presentItem)
        {
            items.Add(item.Key, item.Value);
        }

        return items;
    }
}

using System.Collections.Generic;
using UnityEngine.Localization.Settings;

public class DataManager : Singleton<DataManager>
{
    private Dictionary<string, string> translator = new Dictionary<string, string>()
    {
        { "hpCost", "체력 비용" },
        { "hp", "체력" },
        { "str", "근력" },
        { "wis", "지능" },
        { "otk", "덕력" },
        { "slv", "의존성" },
        { "bonusHp", "보너스 체력" },
        { "lvSan", "신아산 호감도" },
        { "lvHyun", "양나현 호감도" },
        { "lvPyo", "서은표 호감도" },
        { "gold", "골드" },
        { "신아산", "san"},
        { "양나현", "hyun"},
        { "서은표", "pyo"}
    };

    //02-18 강승연 -> 손서희 eng 번역 부탁드립니다.
    //02-20 손서희 eng 번역: Hp(Health), Str(Strength), Int(Intelligence), Geek(Geekiness), Dep(Deopendency)
    //이어서: Bonus HP, San Affinity, Hyun Affinity, Pyo Affinity, Gold
    private Dictionary<string, string> translator_en = new Dictionary<string, string>()
    {
        { "hpCost", "HP Cost" },
        { "hp", "체력" },
        { "str", "근력" },
        { "wis", "지능" },
        { "otk", "덕력" },
        { "slv", "의존성" },
        { "bonusHp", "보너스 체력" },
        { "lvSan", "신아산 호감도" },
        { "lvHyun", "양나현 호감도" },
        { "lvPyo", "서은표 호감도" },
        { "gold", "골드" },
        { "신아산", "san"},
        { "양나현", "hyun"},
        { "서은표", "pyo"}
    };

    public Dictionary<string, string> GetTranslator()
    { 
        if (LocalizationSettings.SelectedLocale.Identifier.Code == "en-US")
        {
            return translator_en;
        }

        return translator;
    }

    public Dictionary<int, Item> itemData;
    
    public void Awake()
    {
        itemData = DataLoader.ReadData<ItemData>().GetItems();
    }
}

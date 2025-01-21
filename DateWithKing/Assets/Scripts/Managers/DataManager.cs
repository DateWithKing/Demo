using System.Collections.Generic;

public class DataManager : Singleton<DataManager>
{
    public Dictionary<string, string> translator = new Dictionary<string, string>()
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
        { "gold", "골드" }
    };
}

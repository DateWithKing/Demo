using System.Collections.Generic;
using Yarn.Unity;

/// <summary>
/// 변화량을 나타내는 DTO
/// </summary>
public class SpotDTO
{
    public string name = "";
    public string comment = "";
    public int hpCost = 0;
    public int hp = 0;
    public int str = 0;
    public int wis = 0;
    public int otk = 0;
    public int slv = 0;
    public int bonusHp = 0;
    public int lvSan = 0;
    public int lvHyun = 0;
    public int lvPyo = 0;
    public int gold = 0;

    public Dictionary<string, int> GetDeltaData()
    {
        Dictionary<string, int> delta = new Dictionary<string, int>();
        delta.Add(DataManager.Instance.GetTranslator()["hpCost"], hpCost);
        if (hp != 0) delta.Add(DataManager.Instance.GetTranslator()["hp"], hp);
        if (str != 0) delta.Add(DataManager.Instance.GetTranslator()["str"], str);
        if (wis != 0) delta.Add(DataManager.Instance.GetTranslator()["wis"], wis);
        if (otk != 0) delta.Add(DataManager.Instance.GetTranslator()["otk"], otk);
        if (slv != 0) delta.Add(DataManager.Instance.GetTranslator()["slv"], slv);
        if (bonusHp != 0) delta.Add(DataManager.Instance.GetTranslator()["bonusHp"], bonusHp);
        if (lvSan != 0) delta.Add(DataManager.Instance.GetTranslator()["lvSan"], lvSan);
        if (lvHyun != 0) delta.Add(DataManager.Instance.GetTranslator()["lvHyun"], lvHyun);
        if (lvPyo != 0) delta.Add(DataManager.Instance.GetTranslator()["lvPyo"], lvPyo);
        if (gold != 0) delta.Add(DataManager.Instance.GetTranslator()["gold"], gold);

        return delta;
    }
}

using System;
using Newtonsoft.Json;

public class Stat
{
    [JsonProperty]
    public readonly int MaxValue = 10;
    [JsonProperty]
    public readonly int MinValue = 1;
    [JsonProperty]
    public readonly int InitValue = 1;
    [JsonProperty]
    public int value { get; private set; } 

    public event Action StatChanged;
    
    public Stat(int startValue, int minValue, int maxValue, int initValue)
    {
        value = startValue;
        MinValue = minValue;
        MaxValue = maxValue;
        InitValue = initValue;
    }
    
    public Stat(int startValue, int minValue, int maxValue)
    {
        value = startValue;
        MinValue = minValue;
        MaxValue = maxValue;
        InitValue = MinValue;
    }

    public Stat(int minValue, int maxValue) : this(minValue, minValue, maxValue)
    {
        
    }

    public Stat()
    {
        
    }

    public void InitStat()
    {
        value = InitValue;
    }

    /// <summary>
    /// 현재 값을 기준으로 스탯을 변경시키는 함수
    /// </summary>
    /// <param name="delta">증가시키거나 감소시킬 양</param>
    public void ChangeStat(int delta)
    {
        value += delta;
        if (value < MinValue) value = MinValue;
        if (value > MaxValue) value = MaxValue;
        StatChanged?.Invoke();
    }
}

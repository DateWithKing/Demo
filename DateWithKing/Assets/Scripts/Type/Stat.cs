using System;

public class Stat
{
    public readonly int MaxValue = 10;
    public readonly int MinValue = 1;
    public int value { get; private set; }

    public event Action StatChanged;
    
    public Stat(int startValue, int minValue, int maxValue)
    {
        value = startValue;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public Stat(int minValue, int maxValue) : this(minValue, minValue, maxValue)
    {
        
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

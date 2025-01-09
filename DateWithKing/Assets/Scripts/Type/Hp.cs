
using System;
using Unity.Mathematics;

public class Hp
{
    public const int LimitHp = 100;
    private int startHp;
    private int maxHp;
    private int currentHp;

    /// <summary>
    /// 현재 Hp 변경을 알리는 이벤트 <br/>
    /// Hp 관련 UI는 MVVP로 구현
    /// </summary>
    public event Action CurrentHpChanged;
    
    public Hp(int maxHp, int bonusHp = 0)
    {
        InitHp(maxHp, bonusHp);
    }

    public void InitHp(int maxHp, int bonusHp = 0)
    {
        this.maxHp = maxHp;
        startHp = maxHp + bonusHp;
        if (startHp > LimitHp) startHp = LimitHp;
        currentHp = startHp;
        CurrentHpChanged?.Invoke();
    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    public int GetHp()
    {
        return currentHp;
    }

    /// <summary>
    /// hpCost의 hp를 소비하는 활동을 할 수 있는지 확인
    /// </summary>
    /// <param name="hpAmount">사용할 Hp의 양</param>
    /// <returns></returns>
    public bool CanUse(int hpAmount)
    {
        if ((currentHp - hpAmount) >= 0) return true;
        return false;
    }

    /// <summary>
    /// Hp를 사용 <br/>
    /// 당연히 소비할 수 있는지 체크함(음수) 소비할 수 없을 경우 사용X
    /// </summary>
    /// <param name="hpAmount">사용할 Hp의 양</param>
    public void UseHp(int hpAmount)
    {
        if (!CanUse(hpAmount)) return;
        currentHp -= hpAmount;
        CurrentHpChanged?.Invoke();
    }

    /// <summary>
    /// Hp를 회복
    /// maxHp보다 많이 회복할 경우 maxHp를 넘지못함
    /// </summary>
    /// <param name="hpAmount">회복할 Hp의 양</param>
    public void RecoverHp(int hpAmount)
    {
        currentHp += hpAmount;
        if (currentHp > maxHp) currentHp = maxHp;
        CurrentHpChanged?.Invoke();
    }
}

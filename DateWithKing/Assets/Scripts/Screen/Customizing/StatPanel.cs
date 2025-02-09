using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 리팩토링 가능성 많음(AppearancePanel이랑 묶어서) <br/>
/// 템플릿 메소드 패턴 사용 가능해보임
/// </summary>
public class StatPanel : MonoBehaviour
{
    private const int LimitStatAmount = 24; //15 + 9를 더함(초기 스탯)
    private StatSelector[] selectors;

    [SerializeField] private TextMeshProUGUI statAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        selectors = GetComponentsInChildren<StatSelector>();
        foreach (var select in selectors)
        {
            select.statChanged -= LimitStat;
            select.statChanged += LimitStat;
        }

        LimitStat();
        statAmount.text = "15 남음";
    }

    void LimitStat()
    {
        int sum = 0;
        
        foreach (var select in selectors)
        {
            sum += select.getcurrentStatAmount;
        }

        if (sum == LimitStatAmount - 1)
        {
            foreach (var select in selectors)
            {
                select.EnableButton();
            }
        }

        statAmount.text = $"{LimitStatAmount - sum} 남음";
        
        if (sum < LimitStatAmount) return;
        
        foreach (var select in selectors)
        {
           select.DisableButton();
        }
    }

    public Dictionary<string, int> GetDeltaStat()
    {
        Dictionary<string, int> data = new Dictionary<string, int>();
        foreach (var select in selectors)
        {
            data.Add(select.GetStatName(), select.getcurrentStatAmount - select.getLowerStatLimit);
        }
        return data;
    }
}

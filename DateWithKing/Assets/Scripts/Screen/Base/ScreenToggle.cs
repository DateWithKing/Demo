using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 스크린에 토글 기능을 부여 <br/>
/// 그룹으로 모인 스크린은 단 하나의 스크린이 반드시 활성되어 있어야 하며, 활성화된 스크린을 제외한 나머지는 비활성 시킴<br/>
/// 단, Screen 스크립트로 활성화 시 두 개 이상의 스크린이 활성화 되어 있을 수 있음
/// </summary>
public class ScreenToggle : MonoBehaviour
{
    [SerializeField]
    private List<Screen> group = new List<Screen>();
    
    // Start is called before the first frame update
    void Start()
    {
        if(group.Any()) ShowScreen(0);
    }

    /// <summary>
    /// 단일한 스크린을 활성화 시킴(나머지 비활성화)
    /// 스크린 번호 순서는 group에 추가한 순서
    /// </summary>
    /// <param name="screenNum">활성화 시킬 스크린의 번호(Enum 처리해 사용하는 것을 권장)</param>
    public void ShowScreen(int screenNum)
    {
        if (screenNum >= group.Count)
        {
            ErrorHandler.PrintWarning(Warning.인덱스_범위를_넘어서는_접근을_시도, 
                "Screen Toggle Group의 Screen 개수를 넘어서는 Screen Number이 매개변수로 들어왔습니다. ");
            return;
        }
        foreach (Screen screen in group)
        {
            screen.HideScreen();
        }
        group[screenNum].ShowScreen();
    }
}

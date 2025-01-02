using UnityEngine;

/// <summary>
/// ScreenToggle에 대한 사이클 기능 제공 <br/>
/// ScreenToggle의 Screen list를 순서대로 순회
/// </summary>
public class ScreenCycle : MonoBehaviour
{
    private ScreenToggle screenToggle;
    private int currentScreen = 0;
    
    public void Awake()
    {
        screenToggle = GetComponent<ScreenToggle>();
    }
    
    /// <summary>
    /// 다음 스크린 출력
    /// </summary>
    public void NextScreen()
    {
        currentScreen.LimitIncrement(screenToggle.ScreenCount());
        screenToggle.ShowScreen(currentScreen);
    }

    /// <summary>
    /// 초기(0번) 스크린 출력
    /// </summary>
    public void StartSequence()
    {
        currentScreen = 0;
        screenToggle.ShowScreen(currentScreen);
    }
}

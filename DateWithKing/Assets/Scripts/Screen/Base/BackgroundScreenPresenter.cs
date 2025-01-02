using UnityEngine;

public class BackgroundScreenPresenter : MonoBehaviour
{
    private static ScreenCycle cycle;

    void Awake()
    {
        cycle = GetComponent<ScreenCycle>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearancePanel : MonoBehaviour
{
    private Dictionary<Appearance, string> data = new Dictionary<Appearance, string>();
    private StyleSelector[] selectors;
    void Start()
    {
        selectors = GetComponentsInChildren<StyleSelector>();
    }

    public Dictionary<Appearance, string> GetStyle()
    {
        data.Clear();
        foreach (var select in selectors)
        {
            data.Add(select.GetAppearance(), select.GetCurrentChoice());
        }
        return data;
    }
}

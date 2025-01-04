using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextWeekView : MonoBehaviour, INextWeekView
{
    [SerializeField]
    private TextMeshProUGUI textPanel;

    public void PrintDate(Date date)
    {
        textPanel.text = date.GetCurrentDate(true);
    }
}

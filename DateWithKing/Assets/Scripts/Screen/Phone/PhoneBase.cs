using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneBase : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeTableText;
    [SerializeField] TextMeshProUGUI profileText;
    [SerializeField] TextMeshProUGUI settingText;

    public void BoldText(int index) { 
        timeTableText.fontStyle = index == 0 ? FontStyles.Bold : FontStyles.Normal;
        profileText.fontStyle = index == 1 ? FontStyles.Bold : FontStyles.Normal;
        settingText.fontStyle = index == 2 ? FontStyles.Bold : FontStyles.Normal;       
    }
}

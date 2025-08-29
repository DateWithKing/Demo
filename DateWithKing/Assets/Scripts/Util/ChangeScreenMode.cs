using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScreenMode : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeMode);
    }

    void ChangeMode()
    {
        if (UnityEngine.Screen.fullScreen)
        {
            UnityEngine.Screen.fullScreen = false;
        }
        else
        {
            UnityEngine.Screen.fullScreen = true;
        }
    }
}

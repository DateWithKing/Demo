using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 해당 스크립트를 붙이면 토글 그룹이 Enable 시 해당 토글이 On 됨
/// </summary>
public class ToggleInitOn : MonoBehaviour
{
    private Toggle toggle;
    // Start is called before the first frame update
    void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        toggle.isOn = true;
    }
}

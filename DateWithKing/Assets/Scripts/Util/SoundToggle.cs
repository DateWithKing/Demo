using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 토글 자동 매핑 및 이벤트 활성화
/// </summary>
public class SoundToggle : MonoBehaviour
{
    private List<Toggle> toggles = new List<Toggle>();

    /// <summary>
    /// 토글이 변할 시에 Call
    /// </summary>
    public event Action<int> toggleChanged;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            toggles.Add(transform.GetChild(i).GetComponent<Toggle>());
            int index = i;
            toggles[i].onValueChanged.AddListener(
                delegate
                {
                    if(toggles[index].isOn)
                        toggleChanged?.Invoke(index); 
                });
        }
    }

    public void ActivateToggle(int num)
    {
        toggles[num].isOn = true;
    }

    /// <summary>
    /// 활성화된 토글 번호 반환
    /// </summary>
    /// <returns></returns>
    public int GetActiveToggle()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i].isOn)
                return i;
        }
        return -1;
    }
}

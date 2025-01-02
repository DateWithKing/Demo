using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomizingView : View
{
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private Button startButton;

    public event UnityAction StartGame = null;

    public void Start()
    {
        startButton.onClick.AddListener(() => {StartGame?.Invoke();});
    }
    
    /// <summary>
    /// 화면에 선택지를 출력
    /// </summary>
    /// <param name="customizingDto"> 출력할 선택지 데이터 </param>
    public void SetCustomizingData(CustomizingDTO customizingDto)
    {
        
    }
    
    /// <summary>
    /// 화면에 출력되어 있는 데이터를 가져옴
    /// </summary>
    /// <returns></returns>
    public CustomizingDTO GetCustomizingData()
    {
        return new CustomizingDTO(nameField.text);
    }
}

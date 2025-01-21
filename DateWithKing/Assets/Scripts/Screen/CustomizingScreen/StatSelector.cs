using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Action = System.Action;

public class StatSelector : MonoBehaviour
{
    [SerializeField] private int lowerStatLimit;
    [SerializeField] private string statName;

    private int currentStatAmount;
    private int upperStatLimit = 10;

    public int getLowerStatLimit
    {
        get { return lowerStatLimit; }
    }

    public int getcurrentStatAmount
    {
        get { return currentStatAmount; }
    }

    public event Action statChanged;

    // UI 요소
    public TextMeshProUGUI statAmount;
    public Button leftButton;
    public Button rightButton;
    
    void Start()
    {
        currentStatAmount = lowerStatLimit;

        // 버튼 초기 비활성화
        if (currentStatAmount <= lowerStatLimit)
        {
            leftButton.interactable = false;
        }

        UpdateUI();

        // 버튼에 클릭 이벤트 추가
        leftButton.onClick.AddListener(SelectMinus);
        rightButton.onClick.AddListener(SelectPlus);
    }

    public string GetStatName()
    {
        return statName;
    }

    public void DisableButton()
    {
        rightButton.interactable = false;
    }

    public void EnableButton()
    {
        if (currentStatAmount < upperStatLimit)
        {
            rightButton.interactable = true;
        }
    }
    
    void SelectMinus()
    {
        // 값에 따라 버튼 활성화, 비활성화
        currentStatAmount--;
        if (currentStatAmount <= lowerStatLimit)
        {
            leftButton.interactable = false;
        }

        if (currentStatAmount < upperStatLimit)
        {
            rightButton.interactable = true;
        }

        statChanged?.Invoke();
        UpdateUI();
    }

    void SelectPlus()
    {
        currentStatAmount++;
        if (currentStatAmount >= upperStatLimit)
        {
            rightButton.interactable = false;
        }

        if (currentStatAmount > lowerStatLimit)
        {
            leftButton.interactable = true;
        }

        statChanged?.Invoke();
        UpdateUI();
    }

    void UpdateUI()
    {
        statAmount.text = currentStatAmount.ToString();
    }
}

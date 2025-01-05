using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatSelector : MonoBehaviour
{

    [SerializeField] private int lowerStatLimit;
    [SerializeField] private string statName;

    private int currentStatAmount;
    private int upperStatLimit = 10;

    public int getcurrentStatAmount
    {
        get { return currentStatAmount; }
    }

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

        UpdateUI();
    }

    void UpdateUI()
    {
        statAmount.text = currentStatAmount.ToString();
    }
}

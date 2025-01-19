using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StyleSelector : MonoBehaviour
{

    [SerializeField] private List<string> attribute = new List<string>();
    [SerializeField] private Appearance attributeName;

    // 리스트 인덱스
    private int currentIndex = 0;
    private string currentAttribute;


    // UI요소
    public TextMeshProUGUI optionText;
    public Button leftButton;
    public Button rightButton;

    // 초기화
    private void Start()
    {
        // ui 업데이트
        UpdateUI();

        // 버튼에 클릭 이벤트 추가
        leftButton.onClick.AddListener(SelectPrevious);
        rightButton.onClick.AddListener(SelectNext);
    }

    public string GetCurrentChoice()
    {
        return attribute[currentIndex];
    }

    public Appearance GetAppearance()
    {
        return attributeName;
    }

    // 이전 선택지
    void SelectPrevious()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = attribute.Count - 1;
        }
        UpdateUI();
    }

    // 다음 선택지
    void SelectNext()
    {
        currentIndex++;
        if (currentIndex >= attribute.Count)
        {
            currentIndex = 0;
        }
        UpdateUI();
    }

    // UI 업데이트
    void UpdateUI()
    {
        currentAttribute = attribute[currentIndex];
        optionText.text = currentAttribute.ToString();
    }
}


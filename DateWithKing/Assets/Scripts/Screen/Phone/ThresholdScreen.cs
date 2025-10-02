using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThresholdScreen : MonoBehaviour
{
    [SerializeField] private Scrollbar thresholdScroll;
    [SerializeField] private TextMeshProUGUI thresholdNumber;
    [SerializeField] private TextMeshProUGUI positiveText;
    [SerializeField] private TextMeshProUGUI negativeText;
    [SerializeField] private Button positiveButton;
    [SerializeField] private Button negativeButton;
    
    private const string UncheckedText = "클릭하여 확인";
    private const string CheckedText = "확인 완료";
    private Color uncheckedColor;
    private Color checkingColor;
    private Color checkedColor;

    void Start()
    {
        ColorUtility.TryParseHtmlString("#777777", out uncheckedColor);
        ColorUtility.TryParseHtmlString("#E7887D", out checkingColor);
        ColorUtility.TryParseHtmlString("#777777", out checkedColor);

        thresholdScroll.value = (GameManager.Instance.PermanentData.setting.thresholdPrecent - 0.5f) / 1.5f;
        if (thresholdScroll.value == 0f) thresholdScroll.value = 0.5f;
    } 

    void OnEnable()
    {
        SetUnchecked(positiveButton, positiveText);
        SetUnchecked(negativeButton, negativeText);
    }

    public void SaveCurrentThreshold()
    {
        GameManager.Instance.PermanentData.setting.thresholdPrecent = (float)Math.Round(Mathf.Lerp(0.5f, 2.0f, thresholdScroll.value), 1);
        OpenCVController.Instance.ChangeThreshold(GameManager.Instance.PermanentData.setting.thresholdPrecent);
    }
    
    public void ChangeThreshold()
    {
        float result = (float)Math.Round(Mathf.Lerp(0.5f, 2.0f, thresholdScroll.value), 1);
        thresholdNumber.text = "x" + result.ToString("0.0");
        SetUnchecked(positiveButton, positiveText);
        SetUnchecked(negativeButton, negativeText);
        OpenCVController.Instance.ChangeThreshold(result);
    }

    public void CheckPositive()
    {
        SetChecking(positiveButton, positiveText, "고개를 끄덕여주세요");
        OpenCVController.Instance.InvokeDetector
            ("testNod", s =>
            {
                if (s == "Timeout") SetUnchecked(positiveButton, positiveText);
                else SetChecked(positiveButton, positiveText);
            },true);
    }

    public void CheckNegative()
    {
        SetChecking(negativeButton, negativeText, "고개를 저어주세요");
        OpenCVController.Instance.InvokeDetector
            ("testShake", s =>
            {
                if (s == "Timeout") SetUnchecked(negativeButton, negativeText);
                else SetChecked(negativeButton, negativeText);
            },true);
    }

    private void SetChecking(Button btn, TextMeshProUGUI text, string checkingText)
    {
        thresholdScroll.interactable = false;
        text.text = checkingText;
        text.fontStyle = FontStyles.Normal;
        text.color = checkingColor;
        btn.interactable = false;
    }

    private void SetChecked(Button btn, TextMeshProUGUI text)
    {
        thresholdScroll.interactable = true;
        text.text = CheckedText;
        text.fontStyle = FontStyles.Normal;
        text.color = checkedColor;
        btn.interactable = false;
    }

    private void SetUnchecked(Button btn, TextMeshProUGUI text)
    {
        thresholdScroll.interactable = true;
        text.text = UncheckedText;
        text.fontStyle = FontStyles.Underline;
        text.color = uncheckedColor;
        btn.interactable = true;
    }
}

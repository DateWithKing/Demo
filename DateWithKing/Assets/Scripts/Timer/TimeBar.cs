using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeBar : MonoBehaviour
{
    [SerializeField] private Image timerBar; // UI 바 이미지
    public float totalTime; // 타이머 총 시간
    private float remainingTime;
    public event Action Timeout;

    void OnEnable()
    {
        remainingTime = totalTime;
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timerBar.fillAmount = remainingTime / totalTime; // 비율 조정
        }
        else
        {
            Timeout?.Invoke();
            gameObject.SetActive(false);
            Timeout = null;
        }
    }

    void OnDisable() {
        Timeout = null;
    }
}

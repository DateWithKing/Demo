using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoveringButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverTimeThreshold = 2.0f;
    private float hoverTimer = 0f;
    private bool isHovering = false;

    [SerializeField] private bool isRight;

    private Button button;

    private SlideSideUI slideSideUI;

    void Awake()
    {
        button = GetComponent<Button>();
        slideSideUI = GetComponent<SlideSideUI>();
        if (button != null)
        {
            // 클릭 막기: Interactable false는 아니고, 클릭 이벤트 제거
            button.onClick.RemoveAllListeners();
        }
    }

    void Update()
    {
        if (isHovering)
        {
            hoverTimer += Time.deltaTime;
            if (hoverTimer >= hoverTimeThreshold)
            {
                isHovering = false;
                hoverTimer = 0f;

                // 원하는 행동을 여기서 직접 실행
                OnAutoClick();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        hoverTimer = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        hoverTimer = 0f;
    }

    private void OnAutoClick()
    {
        if (isRight)
        {
            slideSideUI.SlideLeft();
        }
        else
        {
            slideSideUI.SlideRight();
        }
    }
}

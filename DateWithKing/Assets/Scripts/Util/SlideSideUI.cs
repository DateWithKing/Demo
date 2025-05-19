using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SlideSideUI : MonoBehaviour
{
    public RectTransform uiImage;
    public float slideDuration = 1.0f; // 슬라이드 업 시간
    public Vector2 targetPosition; // 최종 위치
    private float slideDistance = 1080.0f;

    private Vector2 offScreenPosition;

    void Start()
    {
        Canvas canvas = uiImage.GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            targetPosition = new Vector2(-slideDistance, 0);
            offScreenPosition = new Vector2(0, 0);
            uiImage.anchoredPosition = offScreenPosition;


            //SlideUp();
        }
        else
        {
            Debug.Log("null");
        }

    }

    public void SlideLeft()
    {
        uiImage.DOAnchorPos(targetPosition, slideDuration).SetEase(Ease.OutCubic);
    }

    public void SlideRight()
    {
        uiImage.DOAnchorPos(offScreenPosition, slideDuration).SetEase(Ease.OutCubic);

    }
}

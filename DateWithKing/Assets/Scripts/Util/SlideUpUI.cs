using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlideUpUI : MonoBehaviour
{

    public RectTransform uiImage;
    public float slideDuration = 1.0f; // 슬라이드 업 시간
    public Vector2 targetPosition = new Vector2 (0, 0); // 최종 위치

    private Vector2 offScreenPosition;

    void Start()
    {
        Canvas canvas = uiImage.GetComponentInParent<Canvas> ();
        if (canvas != null )
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            offScreenPosition = new Vector2(0, -canvasRect.rect.height);
            uiImage.anchoredPosition = offScreenPosition;
            

            //SlideUp();
        }
        else
        {
            Debug.Log("null");
        }
        
    }

    public void SlideUp()
    {
        uiImage.DOAnchorPos(targetPosition, slideDuration).SetEase(Ease.OutCubic);
    }

    public void SlideDown()
    {
        uiImage.DOAnchorPos(offScreenPosition, slideDuration).SetEase(Ease.InCubic);

    }
    
}

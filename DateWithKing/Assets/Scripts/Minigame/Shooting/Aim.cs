using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public RectTransform aim;
    public Canvas canvas;
    void Start()
    {
        ShootingGameManager.Instance.StartGame += () => {gameObject.SetActive(true); Cursor.visible=false;}; 
        ShootingGameManager.Instance.EndGame += () => {gameObject.SetActive(false); Cursor.visible=true;};
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        // 마우스 위치를 UI 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        aim.localPosition = localPoint;
    }
}

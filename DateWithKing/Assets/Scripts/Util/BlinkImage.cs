using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    [SerializeField] Sprite sprite1;  // 첫 번째 스프라이트
    [SerializeField] Sprite sprite2;  // 두 번째 스프라이트
    [SerializeField] float blinkInterval = 0.5f; // 깜빡이는 간격 (초 단위)

    [SerializeField] Image image;
    private bool isBlinking = false;

    

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkCoroutine());
        }
    }

    public void StopBlinking()
    {
        isBlinking = false;
        StopCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        while (isBlinking)
        {
            image.sprite = sprite1;
            image.SetNativeSize();
            yield return new WaitForSeconds(blinkInterval);

            image.sprite = sprite2;
            image.SetNativeSize();
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}

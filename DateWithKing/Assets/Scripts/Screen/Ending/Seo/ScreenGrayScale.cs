using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenGrayScale : MonoBehaviour
{

    [SerializeField] private Image Screen;

    [SerializeField] ShakingScreen shakingScreen;

    public void FadeOut()
    {
        StartCoroutine(CoFadeOut());
        SoundManager.Instance.PauseBGM();
        Debug.Log("fade out");
    }

    public IEnumerator CoFadeOut()
    { 

        float elapsedTime = 0f;
        float fadedTime = 1f;

        while (elapsedTime <= fadedTime)
        {
            Color c = Screen.color;
            c.a = Mathf.Lerp(0f, 1f, elapsedTime / fadedTime);

            Screen.color = c;
            Debug.Log(c.a);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        shakingScreen.ShakingStart();
    }
    
}

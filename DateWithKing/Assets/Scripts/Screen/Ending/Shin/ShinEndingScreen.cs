using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinEndingScreen : MonoBehaviour
{
    [SerializeField] GameObject[] EndingScreen; // 0: over 70, 1: under 70


    void Start()
    {
        if (GameManager.Instance.data.stats["lvSan"].value >= 70)
        {
            EndingScreen[1].SetActive(false);
            EndingScreen[0].SetActive(true);
            YarnManager.Instance.RunDialogue("엔딩70_신아산");
            GameManager.Instance.data.ending["신아산_진엔딩"] = true;
        }
        else if (GameManager.Instance.data.stats["lvSan"].value >= 50)
        {
            EndingScreen[0].SetActive(false);
            EndingScreen[1].SetActive(true);
            YarnManager.Instance.RunDialogue("엔딩50_신아산");
            GameManager.Instance.data.ending["신아산_일반엔딩"] = true;
        }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EndingScreen : MonoBehaviour
{
    

    [SerializeField] GameObject[] endingScreen;
    private System.Random rand = new System.Random(); // 전역 Random 객체 생성

    private void Start()
    {
        BackgroundController.Instance.ChangeImage(Background.Black);
    }

    public void StartEnding()
    {

        int lvHyun = GameManager.Instance.data.stats["lvHyun"].value;
        int lvSan = GameManager.Instance.data.stats["lvSan"].value;
        int lvPyo = GameManager.Instance.data.stats["lvPyo"].value;

        int[] lvArray = new int[] { lvSan, lvHyun, lvPyo };

        if (lvArray.All(lv => lv < 50))
        {
            endingScreen[3].SetActive(true);
            YarnManager.Instance.RunDialogue("솔로엔딩");
            return;
        }

        int maxAffinity = lvArray.Max();
        List<int> candidates = new List<int>();

        for (int i = 0; i < lvArray.Length; i++)
        {
            if (lvArray[i] == maxAffinity)
            {
                candidates.Add(i);
            }
        }

        // 3. 최고 호감도를 가진 캐릭터가 여러 명이면 랜덤 선택
        int selectedIndex = candidates[rand.Next(candidates.Count)];
        endingScreen[selectedIndex].SetActive(true);
    }

    
}

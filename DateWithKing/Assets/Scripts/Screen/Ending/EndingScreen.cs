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

    private void Start()
    {
        BackgroundController.Instance.ChangeImage(Background.Black);
    }

    public void StartEnding()
    {

        int lvHyun = GameManager.Instance.data.stats["lvHyun"].value;
        int lvSan = GameManager.Instance.data.stats["lvSan"].value;
        int lvPyo = GameManager.Instance.data.stats["lvPyo"].value;
        
        int[] lvArray = new int[3];

        lvHyun = 0;
        lvSan = 10;
        lvPyo = 90;

        lvArray[0] = lvSan;
        lvArray[1] = lvHyun;
        lvArray[2] = lvPyo;

        if (CheckSame(lvArray) == 3)
        {
            for (int i = 0; i < lvArray.Length; i++)
            {
                if (lvArray[i] == lvArray.Max())
                {
                    endingScreen[i].SetActive(true);
                }
            }
        }
        else
        {
            endingScreen[CheckSame(lvArray)].SetActive(true);
        }
    }

    private int CheckSame(int[] lvArray)
    {
        if (lvArray[0] == lvArray[1] && lvArray[1] == lvArray[2])
        {
            System.Random rand = new System.Random();
            int index = rand.Next(0, lvArray.Length); // 0부터 배열 길이 - 1 사이의 랜덤 인덱스 선택
            return index;

        }
        else if (lvArray[0] == lvArray[1])
        {
            System.Random rand = new System.Random();
            return rand.Next(2) == 0 ? 0 : 1;
        }
        else if (lvArray[0] == lvArray[2])
        {
            System.Random rand = new System.Random();
            return rand.Next(2) == 0 ? 0 : 2;
        }
        else if (lvArray[1] == lvArray[2])
        {
            System.Random rand = new System.Random();
            return rand.Next(2) == 0 ? 2 : 1;
        }
        else
        {
            return 3;
        }
    }
}

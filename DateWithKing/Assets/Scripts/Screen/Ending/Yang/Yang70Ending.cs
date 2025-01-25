using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yang70Ending : MonoBehaviour
{
    
    [SerializeField] GameObject[] EndingScreen; // 0: over 70, 1: under 70


    void Start()
    {
        if (GameManager.Instance.data.stats["lvHyun"].value >= 70)
        {
            EndingScreen[0].SetActive(true);
            YarnManager.Instance.RunDialogue("엔딩70_양나현");
        }
        else
        {
            EndingScreen[1].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

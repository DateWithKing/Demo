using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeoEndingScreen : MonoBehaviour
{
    [SerializeField] GameObject[] EndingScreen;
    [SerializeField] Image Panel;

    private void Start()
    {
        BackgroundController.Instance.ChangeImage(Background.Black);

        
        if (GameManager.Instance.data.stats["lvPyo"].value >= 90 &&
                 GameManager.Instance.data.stats["karma"].value >= 7)
        {
            EndingScreen[0].SetActive(false);
            EndingScreen[1].SetActive(false);
            EndingScreen[2].SetActive(true);
            YarnManager.Instance.RunDialogue("엔딩납치_서은표");
            Panel.color = Color.black;
        }
        else if (GameManager.Instance.data.stats["lvPyo"].value >= 70)
        {
            EndingScreen[0].SetActive(false);
            EndingScreen[2].SetActive(false);
            EndingScreen[1].SetActive(true);
            YarnManager.Instance.RunDialogue("엔딩70_서은표");
        }
        else
        {
            EndingScreen[1].SetActive(false);
            EndingScreen[2].SetActive(false);
            EndingScreen[0].SetActive(true);
            YarnManager.Instance.RunDialogue("엔딩50_서은표");
        }
        
    }
    
}

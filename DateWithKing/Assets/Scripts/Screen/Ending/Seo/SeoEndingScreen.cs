using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeoEndingScreen : MonoBehaviour
{
    [SerializeField] GameObject[] EndingScreen;

    private void Start()
    {
        BackgroundController.Instance.ChangeImage(Background.Intro);
    }
    public void ChooseEnding()
    {
        if (GameManager.Instance.data.stats["lvPyo"].value <= 50)
        {
            EndingScreen[0].SetActive(true);
            YarnManager.Instance.RunDialogue("엔딩50_서은표");
        }
        else if (GameManager.Instance.data.stats["lvPyo"].value == 100 &&
                 GameManager.Instance.data.stats["karma"].value >= 8)
        {
            EndingScreen[2].SetActive(true);
            YarnManager.Instance.RunDialogue("엔딩납치_서은표");
        }
        else
        {
            EndingScreen[1].SetActive(true);
            YarnManager.Instance.RunDialogue("엔딩70_서은표");
        }
    }
}

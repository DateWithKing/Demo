using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnding : MonoBehaviour
{
    [SerializeField] private GameObject buttonEnding;
    private void Start()
    {
        BackgroundController.Instance.ChangeImage(Background.Black);
    }

    public void StartEnding()
    {
        buttonEnding.SetActive(true);
        YarnManager.Instance.RunDialogue("방향키엔딩");
    }

}

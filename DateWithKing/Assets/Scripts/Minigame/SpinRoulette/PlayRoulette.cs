using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;


public class PlayRoulette : MonoBehaviour
{
    [SerializeField]
    private Roulette roulette;
    [SerializeField]
    private Button buttonSpin;
    [SerializeField]
    private GameObject backButton;

    private void Start()
    {
        BackgroundController.Instance.ChangeImage(Background.미니게임);
        buttonSpin.interactable = false;
        YarnManager.Instance.RunDialogue("종강총회_룰렛_시작");
        SoundManager.Instance.PlaySFX("미니게임_시작");
    }

    private void EndOfSpin(RoulettePieceData selectedData)
    {
        buttonSpin.interactable = true;
        backButton.SetActive(true);

        if (selectedData.ticketMultiple == 2)
        {
            SoundManager.Instance.PlaySFX("미니게임_성공");
            YarnManager.Instance.RunDialogue("종강총회_룰렛_성공");
            GameManager.Instance.ticket += 15;
        }
        else
        {
            SoundManager.Instance.PlaySFX("미니게임_게임오버");
            YarnManager.Instance.RunDialogue("종강총회_룰렛_실패");
        }
        

        if (GameManager.Instance.ticket <= 0)
        {
            buttonSpin.interactable = false;
        }
    }

    [YarnCommand("StartRoulette")]
    public void StartRoulette()
    {
        

        if (GameManager.Instance.ticket >= 5)
        {
            buttonSpin.interactable = true;

            buttonSpin.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX("룰렛_돌아가는중");
                buttonSpin.interactable = false;
                backButton.SetActive(false);
                roulette.Spin(EndOfSpin);
                GameManager.Instance.ticket -= 5;
            });
        }
        
    }

    public void ClickSound()
    {
        SoundManager.Instance.PlaySFX("UI버튼_클릭");
    }
}

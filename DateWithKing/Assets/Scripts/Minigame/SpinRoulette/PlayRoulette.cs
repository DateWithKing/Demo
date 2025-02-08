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
        buttonSpin.interactable = false;
        YarnManager.Instance.RunDialogue("종강총회_룰렛_시작");
    }

    private void EndOfSpin(RoulettePieceData selectedData)
    {
        buttonSpin.interactable = true;
        backButton.SetActive(true);

        if (selectedData.ticketMultiple == 2)
        {
            YarnManager.Instance.RunDialogue("종강총회_룰렛_성공");
        }
        else
        {
            YarnManager.Instance.RunDialogue("종강총회_룰렛_실패");
        }
        GameManager.Instance.ticket += 5 * selectedData.ticketMultiple;
        

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
                buttonSpin.interactable = false;
                backButton.SetActive(false);
                roulette.Spin(EndOfSpin);
                GameManager.Instance.ticket -= 5;
            });
        }
        
    }
}

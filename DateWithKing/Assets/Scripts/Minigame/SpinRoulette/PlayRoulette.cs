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

    private void Start()
    {
        buttonSpin.interactable = false;
        StartRoulette();
    }

    private void EndOfSpin(RoulettePieceData selectedData)
    {
        buttonSpin.interactable = true;

        Debug.Log($"{selectedData.ticketMultiple}");
        GameManager.Instance.ticket += 5 * selectedData.ticketMultiple;
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
                roulette.Spin(EndOfSpin);
                GameManager.Instance.ticket -= 5;
            });
        }
        
    }
}

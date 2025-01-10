using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayRoulette : MonoBehaviour
{
    [SerializeField]
    private Roulette roulette;
    [SerializeField]
    private Button buttonSpin;

    private void Start()
    {
        StartRoulette();
    }

    private void EndOfSpin(RoulettePieceData selectedData)
    {
        buttonSpin.interactable = true;

        Debug.Log($"{selectedData.ticketMultiple}");
    }

    public void StartRoulette()
    {
        buttonSpin.onClick.AddListener(() =>
        {
            buttonSpin.interactable = false;
            roulette.Spin(EndOfSpin);
        });
    }
}

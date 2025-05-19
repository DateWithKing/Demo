using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class TicketBetting : SceneSingleton<TicketBetting>
{
    [SerializeField] private TextMeshProUGUI TicketAmount;
    [SerializeField] private Button PlusButton;
    [SerializeField] private Button MinusButton;

    public int bettingTicket;

    private void Start()
    {
        bettingTicket = 1;

        MinusButton.interactable = false;
        UpdateUI();

        if (GameManager.Instance.ticket <= 0)
        {
            PlusButton.interactable = false;
        }

        PlusButton.onClick.AddListener(SelectPlus);
        MinusButton.onClick.AddListener(SelectMinus);
    }
    void SelectMinus()
    {
        bettingTicket--;
        if(bettingTicket <= 1)
        {
            MinusButton.interactable = false;
        }
        if(bettingTicket < GameManager.Instance.ticket)
        {
            PlusButton.interactable = true;
        } 

        UpdateUI();
    }

    void SelectPlus()
    {
        bettingTicket++;
        if (bettingTicket >= GameManager.Instance.ticket)
        {
            PlusButton.interactable = false;
        }
        if (bettingTicket > 1)
        {
            MinusButton.interactable = true;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        TicketAmount.text = bettingTicket.ToString();
    }

    [YarnCommand("GetPlusTicket")] 
    public int GetPlusTicket()
    {
        return bettingTicket * 3;
    }
}

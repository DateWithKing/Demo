using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseScreenTicket : MonoBehaviour
{

    [SerializeField] TMP_Text ticketAmount;

    void Start()
    {
        Debug.Log(GameManager.Instance.ticket);
        ticketAmount.text = "TICKET : " + GameManager.Instance.ticket.ToString();
    }

    void UpdateTicket()
    {
        ticketAmount.text = "TICKET : " + GameManager.Instance.ticket.ToString();
    }
}

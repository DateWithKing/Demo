using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ticket : MonoBehaviour
{

    [SerializeField] TMP_Text ticketAmount;

    void Start()
    {
        GameManager.Instance.ticket = 0;
        ticketAmount.text = GameManager.Instance.ticket.ToString();
    }

    void UpdateTicket()
    {
        ticketAmount.text = GameManager.Instance.ticket.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initiation : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.data.stats["karma"].InitStat();
        GameManager.Instance.ticket = 0;
    }
}

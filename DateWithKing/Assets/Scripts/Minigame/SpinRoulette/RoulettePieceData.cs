using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoulettePieceData
{

    public int ticketMultiple; // 티켓 배율

    [Range(1, 100)]
    public int chance = 100; // 등장 확률


    [HideInInspector]
    public int index;           // 아이템 순번
    [HideInInspector]
    public int weight;			// 가중치
}

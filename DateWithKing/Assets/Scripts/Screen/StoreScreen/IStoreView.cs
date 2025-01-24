using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoreView
{
    /// <summary>
    /// 특정 아이템 구매 시 발생하는 이벤트
    /// int 매개변수 : item id
    /// </summary>
    public event Action<int> BuyItem;
    
    /// <summary>
    /// 현재 상점 패널의 모든 아이템을 삭제한다.
    /// </summary>
    public void InitStore();

    /// <summary>
    /// 상점 패널에 아이템을 추가한다.
    /// </summary>
    public void RegisterItem(Item item);

    /// <summary>
    /// 현재 금화를 화면에 출력하는 함수
    /// </summary>
    public void PrintGold(int gold);
}

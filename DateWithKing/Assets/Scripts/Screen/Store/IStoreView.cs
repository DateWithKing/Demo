using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoreView
{
    /// <summary>
    /// 아이템 구매 시 동작할 이벤트를 구독한다.
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

    public void UpdateItem(int index, Item item);

}

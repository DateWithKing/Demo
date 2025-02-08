using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePresenter : MonoBehaviour
{
    private IStoreView view;
    private const int MaxPurchasedCount = 2;
    private int purchasedCount = 0;

    public void Awake()
    {
        view = GetComponent<IStoreView>();
    }
    void Start()
    {
        view.InitStore();
        view.BuyItem -= BuyItem;
        view.BuyItem += BuyItem;

        SemesterSceneData.Instance.clock.DateChanged -= DayUpdate;
        SemesterSceneData.Instance.clock.DateChanged += DayUpdate;

        foreach (var item in DataManager.Instance.itemData)
        {
            view.RegisterItem(item.Value);
        }
    }

    private void DayUpdate()
    {
        purchasedCount = 0;
    }

    private void BuyItem(int item)
    {
        if (GameManager.Instance.data.stats["gold"].value < DataManager.Instance.itemData[item].price)
        {
            YarnManager.Instance.RunDialogue("예외처리_상점돈부족");
            return;
        }

        if (purchasedCount >= MaxPurchasedCount)
        {
            YarnManager.Instance.RunDialogue("예외처리_상점2개이상구매");
            return;
        }
        
        if (!GameManager.Instance.data.inventory.CanAddItem())
        {
            YarnManager.Instance.RunDialogue("예외처리_아이템4개이상소지");
            return;
        }

        purchasedCount++;
        Debug.Log(DataManager.Instance.itemData[item].name + "을 구매했다! ");
        GameManager.Instance.data.inventory.AddItem(DataManager.Instance.itemData[item]); 
        GameManager.Instance.data.stats["gold"].ChangeStat(-DataManager.Instance.itemData[item].price);
    }
}

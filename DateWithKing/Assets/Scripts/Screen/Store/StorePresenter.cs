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
            Debug.Log("돈이 부족합니다.");
            return;
        }

        if (purchasedCount >= MaxPurchasedCount)
        {
            Debug.Log("오늘의 최대 구매 개수를 초과했다. ");
            return;
        }
        
        if (!GameManager.Instance.data.inventory.CanAddItem())
        {
            Debug.Log("더 이상 들 수 없다.  ");
            return;
        }
        
        Debug.Log(DataManager.Instance.itemData[item].name + "을 구매했다! ");
        GameManager.Instance.data.inventory.AddItem(DataManager.Instance.itemData[item]); 
        GameManager.Instance.data.stats["gold"].ChangeStat(-DataManager.Instance.itemData[item].price);
    }
}

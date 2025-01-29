using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GiftView : MonoBehaviour
{
    //선물주기 버튼
    [SerializeField] private Button giftButton;
    //선물 시스템 클릭 버튼
    [SerializeField] private Button giftSystemButton;
    //인벤토리 패널
    [SerializeField] private GameObject giftPanel;
    //선물 시스템 오브젝트 자체
    [SerializeField] private GameObject giftSystemObject;
    
    [SerializeField] private UnityEvent endPresent;

    private string target;

    void Start()
    {
        GameManager.Instance.data.inventory.OnUseItem -= GiveGift;
        GameManager.Instance.data.inventory.OnUseItem += GiveGift;
        
        giftSystemButton.onClick.AddListener(CloseGiftSystem);
        giftButton.onClick.AddListener(ClickGiftButton);
    }
    
    /// <summary>
    /// 선물주기 시작
    /// </summary>
    /// <param name="target">선물을 줄 대상</param>
    public void StartPresent(string target)
    {
        this.target = target;
        giftSystemObject.SetActive(true);
        giftButton.gameObject.SetActive(true);
    }

    private void ClickGiftButton()
    {
        if(giftPanel.activeSelf)
            giftPanel.SetActive(false);
        else giftPanel.SetActive(true);
    }

    private void GiveGift(int gift)
    {
        if (DataManager.Instance.itemData[gift].type != ItemType.선물) return;
        YarnManager.Instance.RunDialogue(DataManager.Instance.itemData[gift].name+target);
        giftPanel.SetActive(false);
        giftButton.gameObject.SetActive(false);
    }

    private void CloseGiftSystem()
    {
        if (giftPanel.activeSelf)
        {
            giftPanel.SetActive(false);
            return;
        }
        if (giftSystemObject.activeSelf) 
        {
            giftSystemObject.SetActive(false);
            endPresent?.Invoke();
        }
    }
}

using UnityEngine;

public class GiftInventoryPresenter : InventoryPresenter
{
    protected override void UseItem(int slot)
    {
        Item item = GameManager.Instance.data.inventory.GetItem(slot);
        if (item.type != ItemType.선물)
        {
            Popup.Instance.PopPanel("사용할 수 없습니다. ");
            return;
        }
        GameManager.Instance.data.inventory.UseItem(slot);
    }
}

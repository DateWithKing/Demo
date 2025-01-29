public class SemesterInventoryPresenter : InventoryPresenter
{
    protected override void UseItem(int slot)
    {
        Item item = GameManager.Instance.data.inventory.GetItem(slot);
        
        //Not Good
        if (item.type == ItemType.none)
        {
            return;
        }
        if (item.type != ItemType.체력)
        {
            Popup.Instance.PopPanel("사용할 수 없습니다. ");
            return;
        } 
        GameManager.Instance.data.inventory.UseItem(slot);
    }
}

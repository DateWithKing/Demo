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
            YarnManager.Instance.RunDialogue("예외처리_아이템사용불가");
            return;
        } 
        SoundManager.Instance.PlaySFX("아이템_사용");
        GameManager.Instance.data.inventory.UseItem(slot);
    }
}

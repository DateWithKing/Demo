using UnityEngine;

public abstract class InventoryPresenter : MonoBehaviour
{
    private IInventoryView view;

    public void Awake()
    {
        view = GetComponent<IInventoryView>();
    }

    public void Start()
    {
        view.UpdateView();
        GameManager.Instance.data.inventory.OnItemChanged -= view.UpdateView;
        GameManager.Instance.data.inventory.OnItemChanged += view.UpdateView; 
        view.OnClick(UseItem);
    }

    protected abstract void UseItem(int slot);
}

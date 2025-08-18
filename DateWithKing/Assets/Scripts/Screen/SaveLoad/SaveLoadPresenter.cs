using System;
using System.IO;
using UnityEngine;

public abstract class SaveLoadPresenter : Presenter
{
    protected ILoadScreenView view;
    protected static SlotDTO[] slots = new SlotDTO[3];
    protected Screen screen;

    void Awake()
    {
        view = GetComponent<ILoadScreenView>();
        screen = GetComponent<Screen>();

        view.ClickSlot -= OnSlotClicked;
        view.ClickSlot += OnSlotClicked;
    }

    protected void Start()
    {
        if (slots[1] != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                view.PrintSlot(i, slots[i]);
            }

            return;
        }
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                GameData data = DataLoader.ReadData<GameData>((DynamicData)i);
            }
            catch (FileNotFoundException ex)
            {
                slots[i] = new SlotDTO("", Resources.Load<Sprite>($"Lobby/데이터없음{i+1}"));
            }
            finally
            {
                slots[i] ??= new SlotDTO(
                    DataLoader.ReadData<GameData>((DynamicData)i).date.GetCurrentDate(true),
                    Resources.Load<Sprite>($"Lobby/데이터있음{i+1}"));
                
                view.PrintSlot(i, slots[i]);
            }
        }
    }

    protected abstract void OnSlotClicked(int slotID);
}

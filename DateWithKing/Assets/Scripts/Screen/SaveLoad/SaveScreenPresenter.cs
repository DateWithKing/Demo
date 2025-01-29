using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScreenPresenter : SaveLoadPresenter
{
    /// <summary>
    /// 슬롯 클릭 시 호출되는 메서드
    /// </summary>
    /// <param name="SlotID"> 클릭된 슬롯 번호 (0, 1, 2) </param>
    protected override void OnSlotClicked(int slotID)
    {
        slots[slotID] = new SlotDTO(GameManager.Instance.data.date.GetCurrentDate(),
            Resources.Load<Sprite>("Lobby/Smile"));
        view.PrintSlot(slotID, slots[slotID]);
        GameManager.Instance.SaveData(slotID);
    }
}

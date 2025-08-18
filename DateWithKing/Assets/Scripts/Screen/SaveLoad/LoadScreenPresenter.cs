using UnityEngine;
using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

public class LoadScreenPresenter : SaveLoadPresenter
{
    void OnEnable()
    {
        try
        {
            GameData data = DataLoader.ReadData<GameData>((DynamicData)0);
        }
        catch (FileNotFoundException ex)
        {
            slots[0] = new SlotDTO("", Resources.Load<Sprite>($"Lobby/데이터없음{1}"));
        }
        finally
        {
            slots[0] ??= new SlotDTO(
                DataLoader.ReadData<GameData>((DynamicData)0).date.GetCurrentDate(true),
                Resources.Load<Sprite>($"Lobby/데이터있음{1}"));
        }
    }
    
    /// <summary>
    /// 슬롯 클릭 시 호출되는 메서드
    /// </summary>
    /// <param name="SlotID"> 클릭된 슬롯 번호 (0, 1, 2) </param>
    protected override void OnSlotClicked(int slotID)
    {
        SlotDTO selectedSlot = slots[slotID];

        // 슬롯이 비어있는지 확인
        if (string.IsNullOrEmpty(selectedSlot.date))
        {
            Debug.Log($"Slot {slotID} is empty. Cannot start the game.");
            return;
        }

        // 슬롯 데이터를 게임 매니저에 저장
        GameManager.Instance.LoadData(slotID);

        // 게임 시작
        screen.MoveScene("Semester");
    }
}
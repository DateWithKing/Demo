using UnityEngine;
using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

public class LoadScreenPresenter : Presenter
{
    private ILoadScreenView view;
    private SlotDTO[] slots = new SlotDTO[3];
    private Screen screen;

    void Awake()
    {
        view = GetComponent<ILoadScreenView>();
        screen = GetComponent<Screen>();

        view.ClickSlot -= OnSlotClicked;
        view.ClickSlot += OnSlotClicked;
    }

    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                GameData data = DataLoader.ReadData<GameData>((DynamicData)i);
            }
            catch (FileNotFoundException ex)
            {
                slots[i] = new SlotDTO("", Resources.Load<Sprite>("Lobby/Sad"));
            }
            finally
            {
                slots[i] ??= new SlotDTO(
                        DataLoader.ReadData<GameData>((DynamicData)i).date.GetCurrentDate(),
                        Resources.Load<Sprite>("Lobby/Smile"));
                view.PrintSlot(i, slots[i]);
            }
        }
    }
    
    /// <summary>
    /// 슬롯 클릭 시 호출되는 메서드
    /// </summary>
    /// <param name="SlotID"> 클릭된 슬롯 번호 (0, 1, 2) </param>
    private void OnSlotClicked(int slotID)
    {
        SlotDTO selectedSlot = slots[slotID];

        // 슬롯이 비어있는지 확인
        if (string.IsNullOrEmpty(selectedSlot.date))
        {
            UnityEngine.Debug.Log($"Slot {slotID} is empty. Cannot start the game.");
            return;
        }

        // 슬롯 데이터를 게임 매니저에 저장
        GameManager.Instance.LoadData(slotID);

        // 게임 시작
        screen.MoveScene("Semester");
    }
}
using UnityEngine;
using System.Diagnostics;

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

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new SlotDTO($"Week {i + 1}");
        }
    }

    void Start()
    {
        // View에 슬롯 데이터를 출력
        for (int i = 0; i < slots.Length; i++)
        {
            view.PrintSlot(i, slots[i]);
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

        // SlotDTO를 CustomizingDTO로 변환
        CustomizingDTO customizingData = ConvertToCustomizingDTO(selectedSlot);

        // 슬롯 데이터를 게임 매니저에 저장
        GameManager.Instance.InitData(customizingData);

        // 게임 시작
        screen.MoveScene("Semester");
    }

    /// <summary>
    /// SlotDTO 데이터를 CustomizingDTO로 변환하는 메서드
    /// </summary>
    /// <returns>CustomizingDTO 객체</returns>
    public CustomizingDTO ConvertToCustomizingDTO(SlotDTO slot)
    {
        CustomizingDTO customizingDTO = new CustomizingDTO
        {
            stat = new StatDataDTO()
        };

        return customizingDTO;
    }
}
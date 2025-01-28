using System;

public interface IInventoryView
{
    /// <summary>
    /// 인벤토리 화면을 인벤토리 데이터에 맞게 업데이트
    /// </summary>
    public void UpdateView();

    /// <summary>
    /// 아이템 사용 시 발생하는 이벤트 <br/>
    /// int : 슬롯 ID
    /// </summary>
    public void OnClick(Action<int> click);
}

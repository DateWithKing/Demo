/// <summary>
/// 유한상태머신의 상태 클래스
/// </summary>
public class State
{
    /// <summary>
    /// 상태가 변경되었을 때 실행
    /// </summary>
    public virtual void Enter() { }
    /// <summary>
    /// 해당 상태일 때 실행할 Update 함수
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// 상태가 다른 상태로 변경되기 직전 실행
    /// </summary>
    public virtual void Exit() {}
}
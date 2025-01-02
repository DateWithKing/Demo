
using System;
using System.Collections.Generic;

/// <summary>
/// 유한상태머신 <br/>
/// 1. 상태 별로 State 클래스를 제작한다. <br/>
/// 2. 유한상태머신으로 자유롭게 관리한다. <br/>
/// </summary>
/// <typeparam name="T"></typeparam>
public class FiniteStateMachine<T> where T : Enum
{
    private Dictionary<T, State> states = new Dictionary<T, State>();
    private State currentState;

    /// <summary>
    /// 관리할 상태 추가
    /// </summary>
    /// <param name="index"> 접근할 상태의 인덱스(enum). Define에 정의 후 사용할 것. </param>
    /// <param name="state"> 타입으로 접근할 상태 객체</param>
    public void AddState(T index, State state)
    {
        states[index] = state;
    }

    /// <summary>
    /// 상태의
    /// </summary>
    /// <param name="index"></param>
    public void ChangeState(T index)
    {
        currentState?.Exit();
        states[index].Enter();
        currentState = states[index];
    }

    /// <summary>
    /// Update() 기능을 사용하려면 FSM을 사용하는 클래스의 Update()에 이 함수를 실행시켜야 함
    /// </summary>
    public void StateUpdate()
    {
        currentState?.Update();
    }

    public bool HasKey(T index)
    {
        return states.ContainsKey(index);
    }
}

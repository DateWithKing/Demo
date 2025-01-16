using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 상태를 정의하는 동적 데이터(Entity)<br/>
/// === 사용방법(데이터를 추가하는 방법) ===<br/>
/// 1. DTO 혹은 클래스를 통해 데이터를 추가했을 경우<br/>
/// 1-1. 생성한 DTO/클래스에 초기 값을 넣어주세요!!<br/>
/// 무슨 말이냐면, PlayerStatDTO를 열여보시면 알 수 있습니다!<br/>
/// 보시면 각 필드에 초기 값이 들어가있는 것을 볼 수 있어요(선언과 동시에 초기화 ex. int hp = 5)<br/>
/// 이 초기 값은 기획서에 잘 나와있습니다.<br/>
/// 1-2. GameData의 필드로 변수를 선언한 후에도 꼭 = new 클래스명() 으로 선언과 동시에 초기화를 해주세요!
/// 1-3. GameManager 클래스 안에 정의된 InitData 함수 내부의 data에 DTO/클래스를 new로 넣어주세요!<br/>
/// 2. int, string 같은 값 필드를 추가하실 경우 필드를 선언과 동시에 초기화해주세요!
/// </summary>
public class GameData : Entity
{
    //1-2
    public string name { get; set; }
    public Date date = new Date();
    public StatDataDTO stats { get; set; } = new StatDataDTO();
    public SettingDTO setting = new SettingDTO();

    public GameData()
    {

    }
}
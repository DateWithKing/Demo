using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Type;
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
    public string name { get; set; } = "손서희";
    public Date date = new Date();
    public Dictionary<string, Stat> stats = new Dictionary<string, Stat>
    {
        { "hp", new Stat(5, 5, 10) },
        { "str", new Stat(1, 1, 10) },
        { "wis", new Stat(1, 1, 10) },
        { "slv", new Stat(1, 1, 10) },
        { "otk", new Stat(1, 1, 10) },
        { "bonusHp", new Stat(0, -50, 50, 0) }, //다음날 Hp 변동 수치(ex. 10이면 다음날 원래 hp + 10된 값으로 시작
        { "lvSan", new Stat(0, -50, 100) },
        { "lvHyun", new Stat(0, -50, 100) },
        { "lvPyo", new Stat(0, -50, 100) },
        { "gold", new Stat(150, 0, Int32.MaxValue) },
        { "karma", new Stat(0, 0, 10)}
    };
    //외관 데이터
    public Dictionary<Appearance, string> appearance = new Dictionary<Appearance, string>();
    public Inventory inventory = new Inventory();
    //채팅 데이터(마지막으로 받은 채팅의 호감도)
    public Dictionary<Character, Chat> chatting = new Dictionary<Character, Chat>
    {
        { Character.양나현, new Chat() },
        { Character.신아산, new Chat() },
        { Character.서은표, new Chat() }
    };

    public Dictionary<Character, int> fuckNum = new Dictionary<Character, int>
    {
        { Character.양나현, 0 },
        { Character.신아산, 0 },
        { Character.서은표, 0 }
    };

    public bool isThereAnyoneBehindYou = false;
    
    public GameData()
    {
        //다회 실행 시 그만큼 구독 수가 늘어 중첩될 수 있음 주의
        foreach (var stat in stats)
        {
            stat.Value.StatChanged += () => { Debug.Log($"스탯 {stat.Key}이 변경되었습니다. 현재 값: {stat.Value.value}"); };
        }
    }
}
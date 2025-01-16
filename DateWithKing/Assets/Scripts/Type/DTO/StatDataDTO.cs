using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDataDTO
{
    public Stat hp = new Stat(5, 5, 10); // 초기 값 설정
    public Stat str = new Stat(1, 1, 10);
    public Stat intel = new Stat(1, 1, 10);
    public Stat slave = new Stat(1, 1, 10);
    public Stat otk = new Stat(1, 1, 10);
    public Stat bonusHp = new Stat(0, 0, 50); //다음날 Hp 변동 수치(ex. 10이면 다음날 원래 hp + 10된 값으로 시작
    public Stat lvSan = new Stat(0, -50, 100);
    public Stat lvHyun = new Stat(0, -50, 100);
    public Stat lvPyo = new Stat(0, -50, 100);
    public Stat gold = new Stat(150, 0, Int32.MaxValue);

    public StatDataDTO()
    {

    }
}
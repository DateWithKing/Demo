using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDataDTO
{
    public int hp = 5; // 초기 값 설정
    public int str = 1;
    public int intel = 1;
    public int slave = 1;
    public int otk = 1;

    public StatDataDTO()
    {

    }

    // 값을 받아와서 설정하는 생성자 메서드
    public StatDataDTO(int hp, int str, int intel, int slave, int otk)
    {
        this.hp = hp;
        this.str = str;
        this.intel = intel;
        this.slave = slave;
        this.otk = otk;
    }
}
using UnityEngine;

/// <summary>
/// SemesterScene에서 씬 로드 시 데이터가 초기화 되어야 하는 데이터를 싱글톤으로 관리
/// </summary>
public class SemesterSceneData : SceneSingleton<SemesterSceneData>
{
    public Clock clock { get; private set; }
    public Hp hp { get; private set; }

    void Awake()
    {
        clock = new Clock();
        hp = new Hp(50);
    }
}

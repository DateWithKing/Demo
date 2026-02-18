using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

/// <summary>
/// SemesterScene에서 씬 로드 시 데이터가 초기화 되어야 하는 데이터를 싱글톤으로 관리
/// </summary>
public class SemesterSceneData : SceneSingleton<SemesterSceneData>
{
    public Clock clock { get; private set; }
    public Hp hp { get; private set; }
    public DayDialogueData DayDialogue;
    public SpotData spot;
    private SpotData spot_en;
    
    void Awake()
    {
        clock = new Clock();
        //초기 값 GameManager에서 가져오도록 수정해야 함
        hp = new Hp(GameManager.Instance.data.stats["hp"].value * 10);
        spot = DataLoader.ReadLocalizationData<SpotData>("ko-KR");
        spot_en = DataLoader.ReadLocalizationData<SpotData>("en-US");
        DayDialogue = DataLoader.ReadData<DayDialogueData>();
    }

    public SpotData GetSpotData(Locale newLocale)
    {
        if(newLocale.Identifier.Code == "ko-KR")
        {
            return spot;
        }
        if (newLocale.Identifier.Code == "en-US")
        {
            return spot_en;
        }

        return spot;
    }
}

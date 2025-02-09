using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KarmaDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.data.stats["karma"].StatChanged -= SoundManager.Instance.AddHorrorEffect;
        GameManager.Instance.data.stats["karma"].StatChanged += SoundManager.Instance.AddHorrorEffect;
        SemesterSceneData.Instance.clock.TimeChanged -= Kidnap;
        SemesterSceneData.Instance.clock.TimeChanged += Kidnap;
    }

    void Kidnap()
    {
        if (SemesterSceneData.Instance.clock.GetCurrentWeekCycle() != WeekCycle.Night) return;
        
        if (GameManager.Instance.data.stats["lvPyo"].value >= 100 &&
            GameManager.Instance.data.stats["karma"].value >= 7)
        {
            Debug.Log("서은표 납치엔딩을 시작합니다. ");
            SceneManager.LoadScene("Ending");
        }
    }
}

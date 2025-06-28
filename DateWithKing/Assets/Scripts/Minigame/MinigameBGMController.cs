using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameBGMController : MonoBehaviour
{
    public string bgmName;

    void Start()
    {
        AudioSource bgmSource = SoundManager.Instance.transform.Find("BGMSource").GetComponent<AudioSource>();
        AudioClip currentClip = bgmSource.clip;

        if (currentClip != null && currentClip.name == bgmName)
        {
            Debug.Log("같은 BGM 재생 중: " + bgmName);
            return;
        }

        SoundManager.Instance.PlayBGM(bgmName);
    }
}

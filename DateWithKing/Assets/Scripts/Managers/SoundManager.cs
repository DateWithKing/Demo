using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 빈 게임 오브젝트에 SoundManager를 달고,
/// 그 밑에 빈 게임 오브젝트 만든 뒤 BGM AudioSource/SFX AudioSource 달면 사용 가능
/// </summary>
public class SoundManager : Singleton<SoundManager>
{
    private const string BGMPath = "Sound/BGM/";
    private const string SFXPath = "Sound/SFX/";
    private AudioSource bgmSounder;
    private AudioSource sfxSounder;
    private AudioReverbFilter reverb;
    void Awake()
    {
        base.Awake();
        bgmSounder = transform.Find("BGMSource").GetComponent<AudioSource>();
        sfxSounder = transform.Find("SFXSource").GetComponent<AudioSource>();
        reverb = bgmSounder.transform.GetComponent<AudioReverbFilter>();

        bgmSounder.loop = true;
        sfxSounder.loop = false;
    }
    
    /// <summary>
    /// Resources/Sound/BGM/ 아래 있는 오디오 클립을 루프로 재생
    /// </summary>
    /// <param name="source">오디오 클립 이름</param>
    public void PlayBGM(string source)
    {
        if(bgmSounder.isPlaying) bgmSounder.Stop();
        AudioClip clip = Resources.Load<AudioClip>(BGMPath + source);
        if (clip == null)
        {
            Debug.Log("음악 파일이 없습니다. " + clip);
            return;
        }
        bgmSounder.clip = clip;
        bgmSounder.Play();
    }

    /// <summary>
    /// BGM 중단
    /// </summary>
    public void PauseBGM()
    {
        bgmSounder.Stop();
    }

    /// <summary>
    /// BGM 다시 재생
    /// </summary>
    public void resumeBGM()
    {
        bgmSounder.Play();
    }

    /// <summary>
    /// Resources/Sound/SFX/ 아래 있는 오디오 클립을 단발적으로 재생
    /// </summary>
    /// <param name="source">오디오 클립 이름</param>
    public void PlaySFX(string source)
    {
        AudioClip clip = Resources.Load<AudioClip>(SFXPath + source);
        if (clip == null)
        {
            Debug.Log("음악 파일이 없습니다. " + clip);
            return;
        }
        sfxSounder.PlayOneShot(clip);
    }

    /// <summary>
    /// 0~1 사이의 값으로 BGM 볼륨의 크기를 조정
    /// </summary>
    /// <param name="ratio">0~1 사이의 값</param>
    public void ChangeBGMVolume(float ratio)
    {
        bgmSounder.volume = ratio;
    }
    
    /// <summary>
    /// 0~1 사이의 값으로 BGM 볼륨의 크기를 조정
    /// </summary>
    /// <param name="ratio">0~1 사이의 값</param>
    public void ChangeSFXVolume(float ratio)
    {
        sfxSounder.volume = ratio;
    }

    /// <summary>
    /// 호러 이펙트 적용
    /// </summary>
    public void AddHorrorEffect()
    {
        int amount = GameManager.Instance.data.stats["karma"].value;
        
        if (amount < 5)
        {
            reverb.dryLevel = 0f;
            reverb.decayTime = 1f;
            reverb.diffusion = 100f;
            reverb.density = 100f;
        }
        else if (amount < 8)
        {
            reverb.dryLevel = -10000f;
            reverb.decayTime = 2f;
        }
        else
        {
            reverb.dryLevel = -10000f;
            reverb.decayTime = 5f;
            reverb.diffusion = 0f;
            reverb.density = 0f;
        }
    }
}

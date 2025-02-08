using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingScreen : MonoBehaviour
{
    [SerializeField] private SoundToggle BGMToggle;
    [SerializeField] private SoundToggle SFXToggle;

    // Start is called before the first frame update
    void Start()
    {
        UpdateBGMSoundSetting(GameManager.Instance.data.setting.bgmVolume);
        UpdateSFXSoundSetting(GameManager.Instance.data.setting.effectVolume);
        BGMToggle.toggleChanged -= UpdateBGMSoundSetting;
        BGMToggle.toggleChanged += UpdateBGMSoundSetting;
        SFXToggle.toggleChanged -= UpdateSFXSoundSetting;
        SFXToggle.toggleChanged += UpdateSFXSoundSetting;
    }

    private void UpdateBGMSoundSetting(int volumn)
    {
        SoundManager.Instance.ChangeBGMVolume(volumn / (float)4);
        GameManager.Instance.data.setting.bgmVolume = volumn;
    }
    
    private void UpdateSFXSoundSetting(int volumn)
    {
        SoundManager.Instance.ChangeSFXVolume(volumn / (float)4);
        GameManager.Instance.data.setting.effectVolume = volumn;
    }
}

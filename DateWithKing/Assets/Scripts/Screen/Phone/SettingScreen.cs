using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingScreen : MonoBehaviour
{
    [SerializeField] private SoundToggle BGMToggle;
    [SerializeField] private SoundToggle SFXToggle;

    void Awake()
    {
        UpdateBGMSoundSetting(GameManager.Instance.PermanentData.setting.bgmVolume);
        UpdateSFXSoundSetting(GameManager.Instance.PermanentData.setting.effectVolume);
    }

    // Start is called before the first frame update
    void Start()
    {
        BGMToggle.toggleChanged -= UpdateBGMSoundSetting;
        BGMToggle.toggleChanged += UpdateBGMSoundSetting;
        SFXToggle.toggleChanged -= UpdateSFXSoundSetting;
        SFXToggle.toggleChanged += UpdateSFXSoundSetting;
        BGMToggle.ActivateToggle(GameManager.Instance.PermanentData.setting.bgmVolume);
        SFXToggle.ActivateToggle(GameManager.Instance.PermanentData.setting.effectVolume);
    }

    private void UpdateBGMSoundSetting(int volumn)
    {
        SoundManager.Instance.ChangeBGMVolume((volumn / (float)4) * 0.5f);
        GameManager.Instance.PermanentData.setting.bgmVolume = volumn; 
    }
    
    private void UpdateSFXSoundSetting(int volumn)
    {
        SoundManager.Instance.ChangeSFXVolume((volumn / (float)4) * 0.1f); 
        GameManager.Instance.PermanentData.setting.effectVolume = volumn;
    }
}

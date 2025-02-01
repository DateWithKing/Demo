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
        BGMToggle.toggleChanged -= UpdateBGMSoundSetting;
        BGMToggle.toggleChanged += UpdateBGMSoundSetting;
        SFXToggle.toggleChanged -= UpdateSFXSoundSetting;
        SFXToggle.toggleChanged += UpdateSFXSoundSetting;
    }

    private void UpdateBGMSoundSetting(int volumn)
    {
        GameManager.Instance.data.setting.bgmVolume = volumn;
    }
    
    private void UpdateSFXSoundSetting(int volumn)
    {
        GameManager.Instance.data.setting.effectVolume = volumn;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleChanger : MonoBehaviour
{
    public void ToggleLang()
    {
        string target = LocalizationSettings.SelectedLocale.Identifier.Code == "ko-KR" ? "en-US" : "ko-KR";
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(target);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileScreen : MonoBehaviour
{
    [SerializeField] private Image profile;
    [SerializeField] private Sprite defaultProfile;
    [SerializeField] private Sprite ndmProfile;
    void Start()
    {
        if (GameManager.Instance.data.appearance[Appearance.인상] == "NDM대상")
            profile.sprite = ndmProfile;
        else
            profile.sprite = defaultProfile;
    }
}

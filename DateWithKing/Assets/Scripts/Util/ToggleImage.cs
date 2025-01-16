using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 토글 On/Off 이미지를 지정하고 On 시 특정 게임 오브젝트를 활성화하는 기능 제공
/// </summary>
public class ToggleImage : MonoBehaviour
{
    [SerializeField] private Sprite offImage;
    [SerializeField] private Sprite onImage;
    [SerializeField] private GameObject onGameObject;
    private Toggle toggle;
    private Image image;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        image = GetComponent<Image>();
    }

    void Start()
    {
        toggle.onValueChanged.AddListener(ChangeImage);
        ChangeImage(toggle.isOn);
        if (!toggle.isOn) image.sprite = offImage;
    }

    void ChangeImage(bool isOn)
    {
        image.sprite = isOn ? onImage : offImage;
        onGameObject.SetActive(isOn);
    }
}

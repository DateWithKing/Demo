using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 배경 이미지를 변경하는 모듈
/// </summary>
public class BackgroundController : Singleton<BackgroundController>
{
    private const string imagePath = "Background/";
    [SerializeField]private Image _leftImage;
    [SerializeField]private Image _rightImage;

    [SerializeField] GameObject[] CharacterScreen; // 0:신 1:양 2:서
    [SerializeField] GameObject HorrorMask;


    public Background currentBackground;
    Background currentBackgroundtemp;
    GameObject characterScreen;

    /// <summary>
    /// 배경 이미지를 변경 <br/>
    /// 아래 조건이 만족되면 사용 가능 <br/>
    /// 1. 배경 이미지 이름이 enum Background에 존재 <br/>
    /// 2. 배경 이미지가 Resources/Background/에 존재
    /// </summary>
    /// <param name="background"> 배경 이미지 </param>
    public void ChangeImage(Background background)
    {
        currentBackground = background;
        _leftImage.sprite = Resources.Load<Sprite>(imagePath + background);
        _rightImage.sprite = Resources.Load<Sprite>(imagePath + background);
    }

    public Background GetBWBackground()
    {
        string background = currentBackground.ToString() + "_흑백";
        Background BWBackground = (Background)Enum.Parse(typeof(Background), background);
        return BWBackground;
    }

    public void OnLooking(string name)
    {
        int karma = GameManager.Instance.data.stats["karma"].value;

        if (karma >= 8)
        {
            HorrorMask.SetActive(true);
        }
        else
        {
            Background BWBackground = GetBWBackground();
            currentBackgroundtemp = currentBackground;
            ChangeImage(BWBackground);

            if (name == "신아산")
            {
                characterScreen = Instantiate(CharacterScreen[0], transform);
            }
            else if (name == "양나현")
            {
                characterScreen = Instantiate(CharacterScreen[1], transform);
            }
            else if (name == "서은표")
            {
                characterScreen = Instantiate(CharacterScreen[2], transform);
            }
            else { Debug.Log("name이 잘못됨"); }
        }
        

    }

    public void FinishLooking()
    {
        ChangeImage(currentBackgroundtemp);
        Destroy(characterScreen);
        HorrorMask.SetActive(false);
    }

}
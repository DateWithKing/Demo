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
    [SerializeField]private Image _image;
    
    /// <summary>
    /// 배경 이미지를 변경 <br/>
    /// 아래 조건이 만족되면 사용 가능 <br/>
    /// 1. 배경 이미지 이름이 enum Background에 존재 <br/>
    /// 2. 배경 이미지가 Resources/Background/에 존재
    /// </summary>
    /// <param name="background"> 배경 이미지 </param>
    public void ChangeImage(Background background)
    {
        _image.sprite = Resources.Load<Sprite>(imagePath + background);
    }
}
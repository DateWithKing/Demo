using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMinigameScreen : MonoBehaviour
{
    [SerializeField] private BlinkImage BlinkImage;
    [SerializeField] private Image RightButton;
    [SerializeField] private Image LeftButton;

    private void OnEnable()
    {
        BackgroundController.Instance.ChangeImage(Background.오락실_게임기);
        if (GameManager.Instance.ticket >= 30)
        {
            BlinkImage.StartBlinking();
        }
    }

    public void ClickSound()
    {
        SoundManager.Instance.PlaySFX("띠롱띠롱");
    }

    public void DisableRight()
    {
        RightButton.enabled = false;
        LeftButton.enabled = true;
    }
    public void DisableLeft()
    {
        LeftButton.enabled = false;
        RightButton.enabled = true;
    }

}

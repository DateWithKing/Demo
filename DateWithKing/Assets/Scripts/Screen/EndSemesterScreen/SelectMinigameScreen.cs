using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMinigameScreen : MonoBehaviour
{
    [SerializeField] private BlinkImage BlinkImage;

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

}

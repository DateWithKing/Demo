using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMinigameScreen : MonoBehaviour
{
    private void OnEnable()
    {
        BackgroundController.Instance.ChangeImage(Background.MiniGame);
    }

}

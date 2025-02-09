using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMinigameScreen : MonoBehaviour
{
    private void OnEnable()
    {
        BackgroundController.Instance.ChangeImage(Background.오락실_게임기);
    }

}

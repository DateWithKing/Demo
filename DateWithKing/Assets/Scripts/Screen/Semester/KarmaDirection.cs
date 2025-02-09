using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.data.stats["karma"].StatChanged -= SoundManager.Instance.AddHorrorEffect;
        GameManager.Instance.data.stats["karma"].StatChanged += SoundManager.Instance.AddHorrorEffect;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShootingGameManager.Instance.StartGame += () => {gameObject.SetActive(true);};
        ShootingGameManager.Instance.EndGame += () => {gameObject.SetActive(false);};
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            SoundManager.Instance.PlaySFX("사격_총소리");
    }
}

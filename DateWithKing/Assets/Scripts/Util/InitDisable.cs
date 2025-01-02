using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitDisable : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetActivator : MonoBehaviour
{
    public GameObject[] targets;

    private void OnEnable() 
    {
        foreach (GameObject gameObject in targets) 
        {
            gameObject.SetActive(true);
        }
    }

    private void OnDisable() 
    {
        foreach (GameObject gameObject in targets) 
        {
            gameObject.SetActive(false);
        }
    }
}

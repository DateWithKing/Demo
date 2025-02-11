using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPhone : MonoBehaviour
{
    public GameObject phone;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!phone.activeSelf) phone.SetActive(true);
            else phone.SetActive(false);
        }
    }

    private void OnDisable()
    {
        phone.SetActive(false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickBySpacebar : MonoBehaviour
{
    Button button;
    void Start ()
    { 
        button = GetComponent<Button>(); 
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            button.onClick.Invoke();
        }
    }
}

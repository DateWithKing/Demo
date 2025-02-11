using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsideScreen : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject prefabObject;
    [SerializeField] private Sprite pyo;
    private GameObject beforeObject;

    void Start()
    {
        prefabObject.GetComponent<Image>().sprite = pyo;
    }

    void OnEnable()
    {
        if (GameManager.Instance.data.date.GetPassedDays() == 4)
        {
            beforeObject = Instantiate(prefabObject, parent);
        }
    }

    private void OnDisable()
    {
        if(beforeObject != null)
            Destroy(beforeObject);
    }
}

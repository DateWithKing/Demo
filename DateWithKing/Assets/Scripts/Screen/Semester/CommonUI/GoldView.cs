using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldView : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Start()
    {
        ChangeTime();
        GameManager.Instance.data.stats["gold"].StatChanged -= ChangeTime;
        GameManager.Instance.data.stats["gold"].StatChanged += ChangeTime;
    }

    void ChangeTime()
    {
        text.text = GameManager.Instance.data.stats["gold"].value.ToString();
    }
}

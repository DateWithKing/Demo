using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour
{
    [SerializeField] private string stat;
    private TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
   
    void Start()
    {
        ChangeStat();
        GameManager.Instance.data.stats[stat].StatChanged -= ChangeStat;
        GameManager.Instance.data.stats[stat].StatChanged += ChangeStat;
    }

    void ChangeStat()
    {
        text.text = GameManager.Instance.data.stats[stat].value.ToString();
    }
}

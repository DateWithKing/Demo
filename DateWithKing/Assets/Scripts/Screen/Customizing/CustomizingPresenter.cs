using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomizingPresenter : Presenter
{
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_InputField name;
    private Screen screen;
    private StatPanel stat;
    private AppearancePanel style;

    private Dictionary<string, lvDTO> attributes;
    
    void Awake()
    {
        attributes = DataLoader.ReadData<AppearanceData>().attributes;
        screen = GetComponent<Screen>();
        stat = GetComponentInChildren<StatPanel>();
        style = GetComponentInChildren<AppearancePanel>();
    }

    void Start()
    {
        startButton.onClick.AddListener(GameStart);
    }

    private void GameStart()
    {
        GameManager.Instance.InitData();
        GameManager.Instance.data.name = name.text;
        foreach (var selector in stat.GetDeltaStat())
        {
            GameManager.Instance.data.stats[selector.Key].ChangeStat(selector.Value);
        }

        lvDTO lv = new lvDTO();

        foreach (var selector in style.GetStyle())
        {
            lv += attributes[selector.Value];
        }
        
        GameManager.Instance.data.stats["lvSan"].ChangeStat(lv.lvSan);
        GameManager.Instance.data.stats["lvHyun"].ChangeStat(lv.lvHyun);
        GameManager.Instance.data.stats["lvPyo"].ChangeStat(lv.lvPyo);
        
        Debug.Log(GameManager.Instance.data.stats["lvSan"].value);
        
        screen.MoveScene("Semester");
    }
}

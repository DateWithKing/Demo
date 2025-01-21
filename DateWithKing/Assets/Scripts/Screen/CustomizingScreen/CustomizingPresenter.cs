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
    
    private Dictionary<string, lvDTO> attributes = new Dictionary<string, lvDTO>
    {
        { "흑색", new lvDTO { lvSan = 5, lvHyun = 0, lvPyo = 0 } },
        { "갈색", new lvDTO { lvSan = 0, lvHyun = 0, lvPyo = 0 } },
        { "금색", new lvDTO { lvSan = -10, lvHyun = 0, lvPyo = 0 } },
        { "숏컷", new lvDTO { lvSan = 5, lvHyun = -10, lvPyo = 0 } },
        { "단발", new lvDTO { lvSan = 0, lvHyun = 0, lvPyo = 5 } },
        { "중단발", new lvDTO { lvSan = 0, lvHyun = 0, lvPyo = 0 } },
        { "장발", new lvDTO { lvSan = 0, lvHyun = 5, lvPyo = -10 } },
        { "150", new lvDTO { lvSan = 5, lvHyun = -10, lvPyo = 0 } },
        { "160", new lvDTO { lvSan = 0, lvHyun = 0, lvPyo = 0 } },
        { "170", new lvDTO { lvSan = -10, lvHyun = -10, lvPyo = 5 } },
        { "귀염", new lvDTO { lvSan = 5, lvHyun = -10, lvPyo = 5 } },
        { "청순", new lvDTO { lvSan = 0, lvHyun = 5, lvPyo = -10 } },
        { "섹시", new lvDTO { lvSan = 0, lvHyun = 0, lvPyo = 0 } }
    };
    
    void Awake()
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightActivityPresenter : Presenter
{
    private Screen _screen;

    private Screen screen
    {
        get
        {
            if (_screen == null) _screen = GetComponent<Screen>();
            return _screen;
        }
        set => _screen = value;
    }

    void Awake()
    {
        screen = GetComponent<Screen>();
    }
    public void DoActivity(string activity)
    {
        screen.ShowScreen();
        string dialogue = $"밤_{activity}";
        YarnManager.Instance.RunDialogue(dialogue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizingPresenter : Presenter
{
    private ICustomizingView view;
    private Screen screen;
    void Awake()
    {
        view = GetComponent<ICustomizingView>();
        screen = GetComponent<Screen>();
    }

    void Start()
    {
        view.StartGame -= GameStart;
        view.StartGame += GameStart;
    }

    private void GameStart()
    {
        GameManager.Instance.InitData(view.GetCustomizingData());
        screen.MoveScene("Game");
    }
}

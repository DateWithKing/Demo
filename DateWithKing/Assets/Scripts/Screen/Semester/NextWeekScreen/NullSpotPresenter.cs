using System;
using UnityEngine;

public class NullSpotPresenter : Presenter
{
    private void OnEnable()
    {
        CursorHandler.ChangeCursor();
    }

    private void OnDisable()
    {
        CursorHandler.ReturnCursor();
        SemesterSceneData.Instance.clock.NextTime();
    }
}

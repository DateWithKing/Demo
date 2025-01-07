using UnityEngine;

public class NullSpotPresenter : Presenter
{
    private void OnDisable()
    {
        SemesterSceneData.Instance.clock.NextTime();
    }
}

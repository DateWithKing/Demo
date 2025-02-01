using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 캐릭터가 등장하는 장소 표시
/// </summary>
public class SpotCharacter : MonoBehaviour
{
    public GameObject prefabObject;
    private GameObject beforeObject;
    private const string path = "Sprites/SpotCharacter/";
    private string spot;

    // Start is called before the first frame update
    void Start()
    {
        spot = name;
        SetSpotCharacter();
        SemesterSceneData.Instance.clock.TimeChanged -= SetSpotCharacter;
        SemesterSceneData.Instance.clock.TimeChanged += SetSpotCharacter;
    }

    private void SetSpotCharacter()
    {
        if(beforeObject != null)
            Destroy(beforeObject);
        
        if (!SemesterSceneData.Instance.DayDialogue.spotCharacters.ContainsKey(DayDialogueData.GetSpotIndex(spot)))
            return;

        beforeObject = Instantiate(prefabObject, transform);
        beforeObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(path +
                                                                           SemesterSceneData.Instance.DayDialogue.spotCharacters[
                                                                               DayDialogueData.GetSpotIndex(spot)]);
    }
}
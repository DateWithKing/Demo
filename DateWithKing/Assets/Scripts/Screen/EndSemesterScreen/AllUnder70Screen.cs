using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnder70Screen : MonoBehaviour
{
    
    void OnEnable()
    {
        YarnManager.Instance.RunDialogue("종강총회_비활성");
    }

    
}

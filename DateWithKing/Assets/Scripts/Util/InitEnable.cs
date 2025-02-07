using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트 활성화 시 다른 오브젝트들도 같이 활성화
/// </summary>
public class InitEnable : MonoBehaviour
{
    //특정 오브젝트
    [SerializeField] private List<GameObject> objects;
    
    void OnEnable()
    {
        foreach(var obj in objects) obj.SetActive(true);
    }
}

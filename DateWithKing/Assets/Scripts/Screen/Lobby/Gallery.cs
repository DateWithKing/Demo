using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//GameData - ending에 추가하면 갤러리에 추가됨
//단, 이미지가 Resources/Sprite/Background에 있어야 함(ending string 이름으로)
public class Gallery : MonoBehaviour
{
    private Transform parent;
    public GameObject imagePrefab;
    public Image EnlargeImage;
    public Button EnlargeButton;
    public List<GalleryImage> Hidden = new List<GalleryImage>();
    void Awake()
    {
        parent = transform;
    }

    void Start()
    {
        foreach (var ending in GameManager.Instance.PermanentData.gallery)
        {
            //만약에 추가되거나 할 일 생기면 리팩토링
            if (ending.Key == "서은표_납치엔딩") continue;
            Instantiate(imagePrefab, parent).GetComponent<GalleryImage>().SetImage(ending.Key, !ending.Value, EnlargeImage, EnlargeButton);
        }
        foreach(var hidden in Hidden)
        {
            hidden.SetImage(hidden.name, !GameManager.Instance.PermanentData.gallery[hidden.name], EnlargeImage, EnlargeButton);
        }
    }
}

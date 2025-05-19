using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryImage : MonoBehaviour
{
    private static Image resizeImage;
    private Button button;
    private Image image;
    private GameObject blur;
    
    void Awake()
    {
        button = transform.GetChild(0).GetComponent<Button>();
        image = transform.GetChild(0).GetComponent<Image>();
        blur = transform.GetChild(1).gameObject;

        button.interactable = false;
        button.onClick.AddListener(EnlargeImage);
    }

    public void SetImage(string ending, bool isBlured, Image enlargeImage, Button enlargeButton)
    {
        if (resizeImage is null)
        {
            resizeImage = enlargeImage;
            resizeImage.gameObject.SetActive(false);
        }
        enlargeButton.onClick.AddListener(ShrinkImage);
        image.sprite = Resources.Load<Sprite>($"Sprites/Background/{ending}");
        blur.SetActive(isBlured);
        button.interactable = true;
    }

    private void EnlargeImage()
    {
        resizeImage.gameObject.SetActive(true);
        resizeImage.sprite = image.sprite;
    }

    private void ShrinkImage()
    {
        resizeImage.gameObject.SetActive(false);
    }
}

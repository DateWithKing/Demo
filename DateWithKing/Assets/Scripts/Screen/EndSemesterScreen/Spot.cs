using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spot : MonoBehaviour
{
    [SerializeField] private int index; // 0: 신아산, 1: 양나현, 2: 서은표
    [SerializeField] Button SpotButton;
    [SerializeField] private GameObject[] chooseScreen;

    private int affection;
    // 자식 이미지 데이터
    [System.Serializable]
    public class ImageData
    {
        public Image image; // 이미지 컴포넌트
        public Sprite defaultSprite; // 기본 스프라이트
        public Sprite highAffectionSprite; // 호감도 조건 만족 시 스프라이트
        public int affectionThreshold = 70; // 호감도 조건
    }
    private int ticketLimit = 30;

    // 이미지 데이터 배열
    public ImageData[] imageDatas;



    // 씬 활성화 시 초기화
    private void OnEnable()
    {
        if (index == 0) { affection = GameManager.Instance.data.stats["lvSan"].value; }
        if (index == 1) { affection = GameManager.Instance.data.stats["lvHyun"].value; }
        if (index == 2) { affection = GameManager.Instance.data.stats["lvPyo"].value; }

        UpdateSprites(); // 씬 활성화 시 스프라이트 업데이트
    }

    // 모든 스프라이트 업데이트
    private void UpdateSprites()
    {
        foreach (ImageData data in imageDatas)
        {
            if (affection >= data.affectionThreshold)
            {
                SpotButton.interactable = true;
                data.image.sprite = data.highAffectionSprite;
            }
            else
            {
                SpotButton.interactable = false;
                data.image.sprite = data.defaultSprite;
            }
        }

        
    }

    public void ChooseScreen()
    {

        if (GameManager.Instance.ticket >= ticketLimit)
        {
            chooseScreen[1].SetActive(true);
        }
        else
        {
            YarnManager.Instance.RunDialogue("종강총회_티켓부족");
        }
    }
}

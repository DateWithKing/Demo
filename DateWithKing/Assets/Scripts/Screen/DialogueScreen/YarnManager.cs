using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class YarnManager : SceneSingleton<YarnManager>
{
    DialogueRunner runner;

    public Screen dialogueScreen;

    [SerializeField]
    private Image CharacterImage;

    [SerializeField]
    private AudioSource SoundEffectAS;

    [SerializeField]
    private TMP_Text NoticeText;

    [SerializeField]
    private GameObject PosNegPanel;  // openCV 도입하면 바꿀부분

    void Start()
    {
        Init();
    }

    void Init(){
        runner = GameObject.FindAnyObjectByType<DialogueRunner>();
        runner.AddCommandHandler("end", EndDialogue);
        runner.AddCommandHandler<string>("notice", Notice);
        runner.AddCommandHandler<string>("show", ShowImage);
        runner.AddCommandHandler<string>("play", SoundEffect);
        runner.AddCommandHandler<string, string>("choice", StartChoice);
    }

    /// <summary>
    /// 타이틀이 <see cref="nodeName"/>인 다이얼로그를 찾아 실행함
    /// </summary>
    public void RunDialogue(string nodeName)
    {
        if (runner == null)
        {
            Init();
        }
        runner.StartDialogue(nodeName);
        dialogueScreen.ShowScreen();
    }

    /// <summary>
    /// 얀 스크립트에서 대화 종료시 호출해야함. <br/>
    /// 기능: <br/>
    /// 캐릭터 이미지 비활성화 <br/>
    /// 다이얼로그 씬 비활성화 <br/>
    /// </summary>
    void EndDialogue()
    {
        dialogueScreen.HideScreen();
        CharacterImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// <see cref="text"/>를 화면 좌측 상단에 2초간 띄웠다 사라지게함
    /// </summary>
    /// <param name="text">띄어쓰기 포함 시 큰따옴표로 묶어서 쓸 것</param>
    void Notice(string text){
        NoticeText.text = text;
        NoticeText.gameObject.SetActive(true);
    }

    /// <summary>
    /// <see cref="spriteName"/>에 해당하는 이미지를 보이게 함 (캐릭터 스프라이트 띄울 때 사용)
    /// </summary>
    /// <param name="spriteName">Resorces/Sprites 폴더에 있는 스프라이트여야 함</param>
    void ShowImage(string spriteName){
        CharacterImage.sprite = Resources.Load<Sprite>("Sprites/"+spriteName);
        CharacterImage.SetNativeSize();
        CharacterImage.gameObject.SetActive(true);
    }

    void SoundEffect(string audioName){
        SoundEffectAS.clip = Resources.Load<AudioClip>("Audio/"+audioName);
        SoundEffectAS.Play();
    }

    /// <summary>
    /// 긍정 선택 시 <see cref="posNode"/>실행, 
    /// 부정 선택 시 <see cref="negNode"/>실행
    /// </summary>
    /// <param name="posNode"></param>
    /// <param name="negNode"></param>
    void StartChoice(string posNode, string negNode){

        // openCV 도입하면 바꿀부분
        PosNegPanel.SetActive(true);

        PosNegPanel.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        PosNegPanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(()=>{
            PosNegPanel.SetActive(false);
            RunDialogue(posNode);});

        PosNegPanel.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        PosNegPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(()=>{
            PosNegPanel.SetActive(false);
            RunDialogue(negNode);});

    }

}

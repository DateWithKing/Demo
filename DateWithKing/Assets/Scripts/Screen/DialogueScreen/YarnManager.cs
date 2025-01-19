using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;


public class YarnManager : SceneSingleton<YarnManager>
{

    [SerializeField]
    private DialogueRunner runner;

    [SerializeField]
    private Screen dialogueScreen;

    [SerializeField]
    private Image CharacterImage;

    [SerializeField]
    private Image BackgroundImage;

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
        runner.AddCommandHandler<string>("dislike", (name)=>Notice(name+"이(/가) 싫어합니다."));
        runner.AddCommandHandler<string>("like", (name)=>Notice(name+"이(/가) 좋아합니다."));
        runner.AddCommandHandler<string>("show", ShowCharactor);
        runner.AddCommandHandler<string>("bg", ShowBackground);
        runner.AddCommandHandler<string>("play", SoundEffect);
        runner.AddCommandHandler<string, string, string, string>("choice", StartChoice);
        runner.AddCommandHandler<string, int>("change", SetStat);
        runner.AddCommandHandler<int>("recover_hp", RecoverHp);
        runner.AddCommandHandler<int>("use_hp", UseHp);
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
    /// 배경경 이미지 비활성화 <br/>
    /// 다이얼로그 씬 비활성화 <br/>
    /// </summary>
    void EndDialogue()
    {
        dialogueScreen.HideScreen();
        CharacterImage.gameObject.SetActive(false);
        BackgroundImage.gameObject.SetActive(false);
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
    /// <see cref="spriteName"/>에 해당하는 캐릭터 이미지를 보이게 함 (변경 포함)
    /// </summary>
    /// <param name="spriteName">Resorces/Sprites 폴더에 있는 스프라이트여야 함</param>
    void ShowCharactor(string spriteName){
        CharacterImage.sprite = Resources.Load<Sprite>("Sprites/Charactor/"+spriteName);
        CharacterImage.SetNativeSize();
        CharacterImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// <see cref="spriteName"/>에 해당하는 배경 이미지를 보이게 함 (변경 포함)
    /// </summary>
    /// <param name="spriteName"></param>
    void ShowBackground(string spriteName){
        BackgroundImage.sprite = Resources.Load<Sprite>("Sprites/Background/"+spriteName);
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
    void StartChoice(string posNode, string posText, string negNode, string negText){

        BackgroundController.Instance.ChangeImage(Background.Looking);

        // openCV 도입하면 바꿀부분
        PosNegPanel.SetActive(true);

        PosNegPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = posText;
        PosNegPanel.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        PosNegPanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(()=>{
            PosNegPanel.SetActive(false);
            RunDialogue(posNode);
            BackgroundController.Instance.ChangeImage(Background.Day);});

        PosNegPanel.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = negText;
        PosNegPanel.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        PosNegPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(()=>{
            PosNegPanel.SetActive(false);
            RunDialogue(negNode);
            BackgroundController.Instance.ChangeImage(Background.Day);});

    }

    /// <summary>
    /// 유저 이름 반환
    /// </summary>
    /// <param name="text"></param>
    [YarnFunction("name")]
    public static string GetName(){
        return GameManager.Instance.data.name;
    }

    /// <summary>
    /// <see cref="statName"/>에 해당하는 스탯 수치 반환
    /// </summary>
    /// <param name="statName"></param>
    [YarnFunction("get")]
    public static int GetStat(string statName){
        return GameManager.Instance.data.stats[statName].value;
    }

    /// <summary>
    /// 최댓값 100기준의 현재 hp 반환
    /// </summary>
    [YarnFunction("get_hp")]
    public static int GetHp(){
        return SemesterSceneData.Instance.hp.GetHp();
    }

    /// <summary>
    /// val만큼 현재 hp를 감소시킴
    /// </summary>
    /// <param name="val"></param>
    void UseHp(int val){
        if(SemesterSceneData.Instance.hp is not null){
            SemesterSceneData.Instance.hp.UseHp(val);
        }
        else{
            Debug.LogError("SemesterSceneData.Instance.hp가 존재하지 않음");
        }
    }

    /// <summary>
    /// val만큼 현재 hp를 증가시킴킴
    /// </summary>
    /// <param name="val"></param>
    void RecoverHp(int val){
        if(SemesterSceneData.Instance.hp is not null){
            SemesterSceneData.Instance.hp.RecoverHp(val);
        }
        else{
            Debug.LogError("SemesterSceneData.Instance.hp가 존재하지 않음");
        }
    }


    /// <summary>
    /// <see cref="statName"/>에 해당하는 스탯 수치 <see cref="val"/>만큼 조정
    /// </summary>
    /// <param name="statName"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    void SetStat(string statName, int val){
        GameManager.Instance.data.stats[statName].ChangeStat(val);
    }

}

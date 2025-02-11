using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;



public class YarnManager : SceneSingleton<YarnManager>
{

    [SerializeField]
    private DialogueRunner runner;

    [SerializeField] 
    private LineView lineView;

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
    private TMP_Text NoticeText2;

    [SerializeField]
    private GameObject PosNegPanel;

    [SerializeField]
    private GameObject contiuneButton;  // 다이얼로그 진행 버튼

    [SerializeField] 
    private GameObject fakeDialogue;
    
    [SerializeField] 
    private TextMeshProUGUI fakeDialogueText;

    [SerializeField] 
    private TextMeshProUGUI fakeDialogueCharacterName;

    private event Action dialogEnded;
    private event Action prevChoice;
    private string prevChoiceDialogue;
    private string prevChoiceDialogueCharacter;
    GameObject posButton;
    GameObject negButton;

    [SerializeField]
    Sprite selected;

    [SerializeField]
    Sprite unselected;

    [SerializeField]
    Sprite nomal;

    
    void Start()
    {
        Init();
    }

    void Init(){
        runner = GameObject.FindAnyObjectByType<DialogueRunner>();
        runner.AddCommandHandler("end", EndDialogue);
        runner.AddCommandHandler("hide", HideCharactor);
        runner.AddCommandHandler("bgm_stop", SoundManager.Instance.PauseBGM);
        runner.AddCommandHandler("bgm_resume", SoundManager.Instance.resumeBGM);
        runner.AddCommandHandler("choice_again", ChoiceAgain);
        runner.AddCommandHandler<string>("notice", Notice2);
        runner.AddCommandHandler<string>("dislike", (name)=>Notice(name+"이(/가) 싫어합니다."));
        runner.AddCommandHandler<string>("like", (name)=>Notice(name+"이(/가) 좋아합니다."));
        runner.AddCommandHandler<string>("show", ShowCharactor);
        runner.AddCommandHandler<string>("bg", ShowBackground);
        runner.AddCommandHandler<string>("play", SoundEffect);
        runner.AddCommandHandler<string>("cheese", cheese);
        runner.AddCommandHandler<string, string, string, string, bool, bool>("choice", StartChoice);
        runner.AddCommandHandler<string, int>("change", SetStat);
        runner.AddCommandHandler<int>("recover_hp", RecoverHp);
        runner.AddCommandHandler<int>("use_hp", UseHp);
        posButton = PosNegPanel.transform.GetChild(0).gameObject;
        negButton = PosNegPanel.transform.GetChild(1).gameObject;
    }

    /// <summary>
    /// 타이틀이 <see cref="nodeName"/>인 다이얼로그를 찾아 실행<br/>
    /// 해당 대화 완전 종료시 <see cref="callback"/> 실행
    /// </summary>
    public void RunDialogue(string nodeName, Action callback = null)
    {
        if (runner == null)
        {
            Init();
        }
        runner.Stop();
        runner.StartDialogue(nodeName);
        dialogueScreen.ShowScreen();
        dialogEnded = callback;
    }

    /// <summary>
    /// 얀 스크립트에서 대화 종료시 호출해야함. <br/>
    /// 기능: <br/>
    /// 캐릭터 이미지 비활성화 <br/>
    /// 배경 이미지 비활성화 <br/>
    /// notice, like/dislike 텍스트 비활성화 <br/>
    /// 다이얼로그 씬 비활성화 <br/>
    /// 대화 완전 종료 시 실행되는 callback 호출 <br/>
    /// </summary>
    void EndDialogue()
    {
        dialogueScreen.HideScreen();
        CharacterImage.gameObject.SetActive(false);
        BackgroundImage.gameObject.SetActive(false);
        NoticeText.gameObject.GetComponent<FadeAndMoveUp>().StopAllCoroutines();
        NoticeText2.gameObject.GetComponent<FadeAndMoveUp>().StopAllCoroutines();
        NoticeText.gameObject.SetActive(false);
        NoticeText2.gameObject.SetActive(false);
        dialogEnded?.Invoke();
        dialogEnded = null;
    }

    /// <summary>
    /// <see cref="text"/>를 화면 좌측 상단에 2초간 띄웠다 사라지게함
    /// </summary>
    /// <param name="text">띄어쓰기 포함 시 큰따옴표로 묶어서 쓸 것</param>
    void Notice(string text){
        NoticeText.text = text;
        NoticeText.gameObject.SetActive(true);
    }
    void Notice2(string text){
        NoticeText2.text = text;
        NoticeText2.gameObject.SetActive(true);
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
    /// 캐릭터 이미지를 숨김
    /// </summary>
    void HideCharactor(){
        CharacterImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// <see cref="spriteName"/>에 해당하는 배경 이미지를 보이게 함 (변경 포함)
    /// </summary>
    /// <param name="spriteName"></param>
    void ShowBackground(string spriteName){
        try{
            BackgroundImage.sprite = Resources.Load<Sprite>("Sprites/Background/"+spriteName);
            BackgroundImage.gameObject.SetActive(true);
        }
        catch(Exception e){
            Debug.LogWarning("배경 스프라이트가 존재하지 않음: "+spriteName+"\n"+e.Message);
        }
    }

    /// <summary>
    /// audioName에 해당하는 소리를 한 번 재생
    /// </summary>
    /// <param name="audioName"></param>
    void SoundEffect(string audioName){
        SoundManager.Instance.PlaySFX(audioName);
    }

    /// <summary>
    /// 현재 대화 중인 캐릭터의 이름을 반환
    /// </summary>
    /// <returns></returns>
    public string GetOpponentCharacter()
    {
        foreach(string character in Enum.GetNames(typeof(Character))){
            if(!runner.CurrentNodeName.Contains(character)) continue;
            return character;
        }
        Debug.LogError("노드 타이틀에 상대 캐릭터 이름이 없습니다.: "+runner.CurrentNodeName);
        return null;
    }

    /// <summary>
    /// 긍정 선택 시 <see cref="posNode"/>실행, 
    /// 부정 선택 시 <see cref="negNode"/>실행
    /// </summary>
    /// <param name="posNode"></param>
    /// <param name="negNode"></param>
    void StartChoice(string posNode, string posText, string negNode, string negText, bool timeLimit=false, bool isAgain=false)
    {
        contiuneButton.SetActive(false);

        string opponentCharacter = GetOpponentCharacter();
        BackgroundController.Instance.OnLooking(opponentCharacter);
        
        prevChoice = () =>
        {
            StartChoice(posNode,posText,negNode,negText,timeLimit,true);
        };

        posButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "긍정";
        negButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "부정";
        posButton.transform.GetChild(0).GetComponent<TMP_Text>().fontStyle = FontStyles.Normal;
        negButton.transform.GetChild(0).GetComponent<TMP_Text>().fontStyle = FontStyles.Normal;

        posButton.GetComponent<Image>().sprite = nomal;
        posButton.GetComponent<Image>().SetNativeSize();
        negButton.GetComponent<Image>().sprite = nomal;
        negButton.GetComponent<Image>().SetNativeSize();

        StartCoroutine(LateStartChoice(posNode, posText, negNode, negText, opponentCharacter, timeLimit, isAgain));
    }
    IEnumerator LateStartChoice(string posNode, string posText, string negNode, string negText, string opponentCharacter, bool timeLimit=false, bool isAgain=false)
    {
        yield return new WaitForSeconds(1f);
        if(!isAgain)
        {
            prevChoiceDialogue = lineView.lineText.text;
            prevChoiceDialogueCharacter = lineView.characterNameText.text;
        }

        PosNegPanel.SetActive(true);
        if(timeLimit) TimeBarController.Instance.StartTimer();
        

        if(GameManager.Instance.data.isThereAnyoneBehindYou)
        {
            OpenCVController.Instance.InvokeDetector("Dialogue", (string answer)=>{
                CheckDialogueCV(answer, posNode, posText, negNode, negText, opponentCharacter);
                }, timeLimit);
        }
        else
        {
            OpenCVController.Instance.InvokeDetector("DialogueWithMultiface", (string answer)=>{
                CheckDialogueCV(answer, posNode, posText, negNode, negText, opponentCharacter);
                }, timeLimit);
        }
    }
    
    private void CheckDialogueCV(string answer, string posNode, string posText, string negNode, string negText, string opponentCharacter)
    {
        BackgroundController.Instance.FinishLooking();
        TimeBarController.Instance.HideTimer();
        
        switch(answer){
            case "Positive":
                SoundEffect("선택_긍정");
                posButton.transform.GetChild(0).GetComponent<TMP_Text>().text = posText;
                posButton.transform.GetChild(0).GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;
                posButton.GetComponent<Image>().sprite = selected;
                posButton.GetComponent<Image>().SetNativeSize();
                negButton.GetComponent<Image>().sprite = unselected;
                StartCoroutine(RunDialogueLate(posNode, dialogEnded));
                break;
            case "Negative":
                SoundEffect("선택_부정");
                negButton.transform.GetChild(0).GetComponent<TMP_Text>().text = negText;
                negButton.transform.GetChild(0).GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;
                negButton.GetComponent<Image>().sprite = selected;
                negButton.GetComponent<Image>().SetNativeSize();
                posButton.GetComponent<Image>().sprite = unselected;
                StartCoroutine(RunDialogueLate(negNode, dialogEnded));
                break;
            case "Fuck":
                GameManager.Instance.data.fuckNum[opponentCharacter.ToEnum<Character>()]++;
                EndChoice(opponentCharacter+"_엿");
                break;
            case "MultipleFace":
                if(GameManager.Instance.data.isThereAnyoneBehindYou) Debug.LogError("얼굴두개 두번째 인식됨");
                GameManager.Instance.data.isThereAnyoneBehindYou = true;
                EndChoice(opponentCharacter+"_두명");
                break;
            case "Timeout":
                EndChoice(opponentCharacter+"_느려");
                break;
            default: Debug.LogError("OpenCV Answer is wrong: "+answer); break;
        }
    }

    /// <summary>
    /// 다이얼로그 한 줄을 출력
    /// </summary>
    /// <param name="dialogue">대사</param>
    /// <param name="character">캐릭터 이름</param>
    public void PrintDialogue(string dialogue, string character)
    {
        fakeDialogueText.text = dialogue;
        fakeDialogueCharacterName.text = character;
        fakeDialogue.GetComponent<CanvasGroupFader>().EnableCanvasGroup();
    }

    /// <summary>
    /// 다이얼로그 2초 늦게 실행, 진행중인 다이얼로그가 있어도 강제실행
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public IEnumerator RunDialogueLate(string node, Action callback = null){
        yield return new WaitForSeconds(3f);
        this.dialogEnded = callback;
        EndChoice(node);
    }
    void EndChoice(string node){
        runner.Stop();
        fakeDialogue.GetComponent<CanvasGroupFader>().DisableCanvasGroup();
        contiuneButton.SetActive(true);
        RunDialogue(node, dialogEnded);
        PosNegPanel.SetActive(false);
    }
    
    /// <summary>
    /// 이전 choice 단계를 다시 실행함
    /// </summary>
    void ChoiceAgain(){
        PrintDialogue(prevChoiceDialogue, prevChoiceDialogueCharacter);
        prevChoice?.Invoke();
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

    [YarnFunction("get_fuck")]
    public static int GetFuck(){
        return GameManager.Instance.data.fuckNum[Instance.GetOpponentCharacter().ToEnum<Character>()];
    }

    /// <summary>
    /// 외관 종류를 매개변수로 받아 그 값을 문자열로 반환함
    /// </summary>
    [YarnFunction("get_appearance")]
    public static string GetAppearance(string appearence){
        if(Enum.TryParse(appearence, out Appearance app)){
            return GameManager.Instance.data.appearance[app];
        }
        else{
            Debug.LogError("존재하지 않는 외관 종류입니다: " + appearence);
            return "";
        }
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

    /// <summary>
    /// 브이~ 인식 후 node 다이얼로그 실행
    /// </summary>
    /// <param name="node"></param>
    void cheese(string node){
        OpenCVController.Instance.InvokeDetector("Picture", (string s)=>{
            SoundEffect("브이_찰칵");
            RunDialogue(node);});
    }
    
}

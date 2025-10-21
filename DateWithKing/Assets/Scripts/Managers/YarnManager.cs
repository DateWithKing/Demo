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
    private GameObject autoToggle;  // 오토 진행 버튼

    [SerializeField] 
    private GameObject fakeDialogue;
    
    [SerializeField] 
    private TextMeshProUGUI fakeDialogueText;

    [SerializeField] 
    private TextMeshProUGUI fakeDialogueCharacterName;

    // ADDED: A boolean to enable/disable keyboard choices. You can toggle this in the Unity Inspector.
    [SerializeField]
    private bool allowKeyboardChoices = false;

    private event Action dialogEnded;
    private event Action prevChoice;
    private string prevChoiceDialogue;
    private string prevChoiceDialogueCharacter;
    private string prevChoiceDialogueName = "";
    private bool isAuto = false; // 오토 진행 여부
    GameObject posButton;
    GameObject negButton;

    [SerializeField]
    Sprite selected;

    [SerializeField]
    Sprite unselected;

    [SerializeField]
    Sprite normal;

    // ADDED: Variables to store the current choice data for the Update loop to use.
    private string currentPosNode;
    private string currentPosText;
    private string currentNegNode;
    private string currentNegText;
    private string currentOpponentCharacter;
    
    private bool choiceHasBeenMade = false;
    
    void Start()
    {
        Init();
    }

    // ADDED: The Update method to listen for keyboard input.
    void Update()
    {
        // We only check for input if keyboard choices are allowed AND the choice panel is active.
        if (allowKeyboardChoices && PosNegPanel.activeInHierarchy)
        {
            // '1' key for Positive
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                // We pass "Positive" to a new handler function
                HandleChoiceSelection("Positive");
            }
            // '2' key for Negative
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                // We pass "Negative" to a new handler function
                HandleChoiceSelection("Negative");
            }
        }
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
        runner.AddCommandHandler<string, string>("print", PrintDialogue);
        posButton = PosNegPanel.transform.GetChild(0).gameObject;
        negButton = PosNegPanel.transform.GetChild(1).gameObject;
        lineView.holdTime = 1.5f; // 오토 진행 시 대사 출력 후 1.5초 후에 다음 대사로 넘어감
    }

    public void AutoAdvance()
    {
        isAuto = !isAuto;
        lineView.autoAdvance = isAuto;
        if(lineView.autoAdvance) lineView.OnContinueClicked();
    }

    public void SpeedUpDown()
    {
        lineView.typewriterEffectSpeed = lineView.typewriterEffectSpeed == 40f ? 160f : 40f;
        lineView.holdTime = lineView.holdTime == 1.5f ? 0.2f : 1.5f;
    }

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

    void Notice(string text){
        NoticeText.text = text;
        NoticeText.gameObject.SetActive(true);
    }
    void Notice2(string text){
        NoticeText2.text = text;
        NoticeText2.gameObject.SetActive(true);
    }

    void ShowCharactor(string spriteName){
        if(isNight() && !spriteName.Contains("_ver2") 
        && !prevChoiceDialogueName.Contains("편의점_서은표") && !runner.CurrentNodeName.Contains("편의점_서은표")){  // 주4 밤 편의점 서은표 예외처리
            spriteName += "_ver2";
        }
        CharacterImage.sprite = Resources.Load<Sprite>("Sprites/Charactor/"+spriteName);
        if(CharacterImage.sprite == null){
            Debug.LogError("이미지가 존재하지 않음: "+spriteName);
            return;
        }
        CharacterImage.SetNativeSize();
        CharacterImage.gameObject.SetActive(true);
    }

    bool isNight(){
        return SemesterSceneData.Instance.clock.GetCurrentWeekCycle() == WeekCycle.Night;
    }

    void HideCharactor(){
        CharacterImage.gameObject.SetActive(false);
    }

    void ShowBackground(string spriteName){
        try{
            BackgroundImage.sprite = Resources.Load<Sprite>("Sprites/Background/"+spriteName);
            BackgroundImage.gameObject.SetActive(true);
            GameManager.Instance.PermanentData.UpdateGallery(spriteName);
        }
        catch(Exception e){
            Debug.LogWarning("배경 스프라이트가 존재하지 않음: "+spriteName+"\n"+e.Message);
        }
    }

    void SoundEffect(string audioName){
        SoundManager.Instance.PlaySFX(audioName);
    }

    public string GetOpponentCharacter()
    {
        foreach(string character in Enum.GetNames(typeof(Character))){
            if(!runner.CurrentNodeName.Contains(character)) continue;
            return character;
        }
        Debug.LogError("노드 타이틀에 상대 캐릭터 이름이 없습니다.: "+runner.CurrentNodeName);
        return null;
    }

    void StartChoice(string posNode, string posText, string negNode, string negText, bool timeLimit=false, bool isAgain=false)
    {
        contiuneButton.SetActive(false);
        lineView.autoAdvance = false;  
        autoToggle.SetActive(false);  

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

        posButton.GetComponent<Image>().sprite = normal;
        posButton.GetComponent<Image>().SetNativeSize();
        negButton.GetComponent<Image>().sprite = normal;
        negButton.GetComponent<Image>().SetNativeSize();

        StartCoroutine(LateStartChoice(posNode, posText, negNode, negText, opponentCharacter, timeLimit, isAgain));
    }
    IEnumerator LateStartChoice(string posNode, string posText, string negNode, string negText, string opponentCharacter, bool timeLimit=false, bool isAgain=false)
    {
        yield return new WaitForSeconds(1f);
        if(!isAgain)
        {
            prevChoiceDialogue = lineView.lineText.text;
            prevChoiceDialogueName = runner.CurrentNodeName;
            prevChoiceDialogueCharacter = lineView.characterNameText.text;
        }

        choiceHasBeenMade = false;

        PosNegPanel.SetActive(true);
        if(timeLimit) TimeBarController.Instance.StartTimer();

        // ADDED: Store the choice data in class-level variables for the keyboard input to access.
        currentPosNode = posNode;
        currentPosText = posText;
        currentNegNode = negNode;
        currentNegText = negText;
        currentOpponentCharacter = opponentCharacter;
        

        
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
        // If keyboard input is enabled, we just wait. The Update() method will handle the rest.


    // ADDED: A new function to handle making the choice, whether from OpenCV or Keyboard.
    // This reduces code duplication.
    private void HandleChoiceSelection(string answer)
    {
        // IMPORTANT: If you are using keyboard, you might want to cancel the active OpenCV detection
        // to prevent it from firing after a key is pressed. You may need to add a "Cancel" function
        // to your OpenCVController.
        // For example: OpenCVController.Instance.CancelCurrentDetection();

        if (choiceHasBeenMade) return;

        choiceHasBeenMade = true;

        BackgroundController.Instance.FinishLooking();
        TimeBarController.Instance.HideTimer();
        
        Debug.Log("Input Answer: "+answer);

        switch(answer){
            case "Positive":
            case "Thumbs_Up":
                SoundEffect("선택_긍정");
                posButton.transform.GetChild(0).GetComponent<TMP_Text>().text = currentPosText;
                posButton.transform.GetChild(0).GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;
                posButton.GetComponent<Image>().sprite = selected;
                posButton.GetComponent<Image>().SetNativeSize();
                negButton.GetComponent<Image>().sprite = unselected;
                StartCoroutine(RunDialogueLate(currentPosNode, dialogEnded));
                break;
            case "Negative":
            case "Thumbs_Down":
                SoundEffect("선택_부정");
                negButton.transform.GetChild(0).GetComponent<TMP_Text>().text = currentNegText;
                negButton.transform.GetChild(0).GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;
                negButton.GetComponent<Image>().sprite = selected;
                negButton.GetComponent<Image>().SetNativeSize();
                posButton.GetComponent<Image>().sprite = unselected;
                StartCoroutine(RunDialogueLate(currentNegNode, dialogEnded));
                break;
            case "Fuck":
                if (currentOpponentCharacter is not null)
                {
                    GameManager.Instance.data.fuckNum[currentOpponentCharacter.ToEnum<Character>()]++;
                    EndChoice(currentOpponentCharacter+"_엿");
                }
                else
                {
                    // Fallback if there's no character context
                    PosNegPanel.SetActive(false); // Hide panel to prevent multiple inputs
                    OpenCVController.Instance.InvokeDetector("Dialogue", (string ans)=>{
                        CheckDialogueCV(ans, currentPosNode, currentPosText, currentNegNode, currentNegText, currentOpponentCharacter);
                });}
                break;
            case "MultipleFace":
                if(GameManager.Instance.data.isThereAnyoneBehindYou) Debug.LogError("얼굴두개 두번째 인식됨");
                GameManager.Instance.data.isThereAnyoneBehindYou = true;
                if(currentOpponentCharacter is not null) EndChoice(currentOpponentCharacter+"_두명");
                else {
                    PosNegPanel.SetActive(false); // Hide panel
                    OpenCVController.Instance.InvokeDetector("Dialogue", (string ans)=>{
                        CheckDialogueCV(ans, currentPosNode, currentPosText, currentNegNode, currentNegText, currentOpponentCharacter);
                });}
                break;
            case "Timeout":
                EndChoice(currentOpponentCharacter+"_느려");
                break;
            case "FingerHeart":
                if(currentOpponentCharacter is not null) EndChoice(currentOpponentCharacter+"_K하트");
                else {
                    PosNegPanel.SetActive(false); // Hide panel
                    OpenCVController.Instance.InvokeDetector("Dialogue", (string ans)=>{
                        CheckDialogueCV(ans, currentPosNode, currentPosText, currentNegNode, currentNegText, currentOpponentCharacter);
                });}
                break;
            case "Slap":
                if(currentOpponentCharacter is not null) EndChoice(currentOpponentCharacter+"_주먹");
                else {
                    PosNegPanel.SetActive(false); // Hide panel
                    OpenCVController.Instance.InvokeDetector("Dialogue", (string ans)=>{
                        CheckDialogueCV(ans, currentPosNode, currentPosText, currentNegNode, currentNegText, currentOpponentCharacter);
                });}
                break;
            case "Shh":
                if(currentOpponentCharacter is not null) EndChoice(currentOpponentCharacter+"_쉿");
                else {
                    PosNegPanel.SetActive(false); // Hide panel
                    OpenCVController.Instance.InvokeDetector("Dialogue", (string ans)=>{
                        CheckDialogueCV(ans, currentPosNode, currentPosText, currentNegNode, currentNegText, currentOpponentCharacter);
                });}
                break;
            default: Debug.LogError("Input Answer is wrong: "+answer); break;
        }
    }
    
    // MODIFIED: This function now just calls the new handler function.
    private void CheckDialogueCV(string answer, string posNode, string posText, string negNode, string negText, string opponentCharacter)
    {
        // Store current values just in case they're needed by a fallback
        currentPosNode = posNode;
        currentPosText = posText;
        currentNegNode = negNode;
        currentNegText = negText;
        currentOpponentCharacter = opponentCharacter;
        
        HandleChoiceSelection(answer);
    }

    public void PrintDialogue(string dialogue, string character)
    {
        fakeDialogueText.text = dialogue;
        fakeDialogueCharacterName.text = character;
        fakeDialogue.GetComponent<CanvasGroupFader>().EnableCanvasGroup();
    }

    public IEnumerator RunDialogueLate(string node, Action callback = null){
        yield return new WaitForSeconds(3f);
        this.dialogEnded = callback;
        EndChoice(node);
    }
    void EndChoice(string node){
        runner.Stop();
        fakeDialogue.GetComponent<CanvasGroupFader>().DisableCanvasGroup();
        contiuneButton.SetActive(true);
        autoToggle.SetActive(true);  
        if(isAuto) lineView.autoAdvance = true; 
        RunDialogue(node, dialogEnded);
        PosNegPanel.SetActive(false);
    }
    
    void ChoiceAgain(){
        PrintDialogue(prevChoiceDialogue, prevChoiceDialogueCharacter);
        prevChoice?.Invoke();
    }

    [YarnFunction("name")]
    public static string GetName(){
        return GameManager.Instance.data.name;
    }

    [YarnFunction("get")]
    public static int GetStat(string statName){
        return GameManager.Instance.data.stats[statName].value;
    }

    [YarnFunction("get_hp")]
    public static int GetHp(){
        return SemesterSceneData.Instance.hp.GetHp();
    }

    [YarnFunction("get_fuck")]
    public static int GetFuck(){
        return GameManager.Instance.data.fuckNum[Instance.GetOpponentCharacter().ToEnum<Character>()];
    }

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

    void UseHp(int val){
        if(SemesterSceneData.Instance.hp is not null){
            SemesterSceneData.Instance.hp.UseHp(val);
        }
        else{
            Debug.LogError("SemesterSceneData.Instance.hp가 존재하지 않음");
        }
    }

    void RecoverHp(int val){
        if(SemesterSceneData.Instance.hp is not null){
            SemesterSceneData.Instance.hp.RecoverHp(val);
        }
        else{
            Debug.LogError("SemesterSceneData.Instance.hp가 존재하지 않음");
        }
    }

    void SetStat(string statName, int val){
        GameManager.Instance.data.stats[statName].ChangeStat(val);
    }

    void cheese(string node){
        OpenCVController.Instance.InvokeDetector("Picture", (string s)=>{
            SoundEffect("브이_찰칵");
            fakeDialogue.SetActive(false);
            RunDialogue(node);});
    }
    
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;
using UnityEngine.EventSystems;
using System.Reflection;

public class ChatManager : Singleton<ChatManager> // ToDo: 싱글톤 상속해야함
{ 
    const string spritePath = "Sprites/NightPhone/";
    public string[] profileSpriteName = {"양나현", "서은표", "신아산"};  // 스프라이트 이름
    public Dictionary<string, Sprite> profileImage = new Dictionary<string, Sprite>();  // 프로필 이미지 딕셔너리
    public Dictionary<string, Chatting> chattingDict = new Dictionary<string, Chatting>();  // 채팅목록 딕셔너리

    [SerializeField] private GameObject PhoneScreen;

    [SerializeField] private Image proImage;
    [SerializeField] private TextMeshProUGUI opponentName;

    [SerializeField]
    private GameObject ChatBox_Me;  // 나의 채팅박스 프리팹

    [SerializeField]
    private GameObject ChatBox_Opponent;  // 상대방 채팅박스 프리팹
    [SerializeField]
    private GameObject ChatBox_Opponent_Image;

    [SerializeField] Button BlackScreen;
    [SerializeField] Button PhoneBase;
    [SerializeField] Button Emoticon1;
    [SerializeField] Button Emoticon2;
    [SerializeField] Button Emoticon3;
    [SerializeField] GameObject BlockScreen;

    [SerializeField] private Sprite[] Emoticon;

    Chatting chatting;  // 지금 출력중인 채팅
    [SerializeField]
    ScrollRect Chatroom;  // 지금 채팅을 출력중인 채팅방
    Action endMessage;  // 채팅 끝나면 실행할 함수
    [SerializeField] private SlideUpUI slideUPDown;

    private const string ImageSpritePath = "Sprites/NightPhone/";
    private string meName = GameManager.Instance.data.name;
    private bool isPlaying = false;

    private void Start()
    {
        foreach(var sprite in profileSpriteName)
            profileImage.Add(sprite, Resources.Load<Sprite>(spritePath+sprite));

        BlackScreen.interactable = false;
        PhoneBase.interactable = false;
        Emoticon1.interactable = false;
        Emoticon2.interactable = false;
        Emoticon3.interactable = false;

        LateStart();
        //StartChat("신아산_80");
        //slideUPDown.SlideUp();
        //StartChat();
    }

    public struct chat{
        public bool me;
        public string text; // 대사
        //public string emoticon;  // 이모티콘
        public string image; // 사진
        public chat (bool me,string text = null, string image = null)
        {
            this.me = me;
            this.text = text;
            //this.emoticon = emoticon;
            this.image = image;
        }
    }
    public struct Chatting
    {
        public string name;  // 누구와 채팅하는지 (채팅방 이름)
        public List<chat> chatList;  // 대사 리스트 
        public bool chooseImoticon;  // 이모티콘 선택이 나와야 하는지?
        public string[] nextChatting;

        

        public Chatting(string name, bool chooseImoticon, string[] nextChating = null){
            this.name = name;
            chatList = new List<chat>();
            this.chooseImoticon = chooseImoticon;
            this.nextChatting = nextChating;
        }
    }


    [YarnCommand("StartPhoneChat")]
    public void StartChat(string chattingTitle, Action endMessage = null)
    {
        BlockScreen.SetActive(true);
        BlackScreen.interactable = false;
        PhoneBase.interactable = false;
        Emoticon1.interactable = false;
        Emoticon2.interactable = false;
        Emoticon3.interactable = false;

        SoundManager.Instance.PlaySFX("띠롱띠롱");
        this.endMessage = endMessage;
        StartCoroutine(StartChatting(chattingTitle));

    } 

    public IEnumerator StartChatting(string chattingTitle)
    {
        PhoneScreen.SetActive(true);
        Debug.Log("Start Chat: " + chattingTitle);

        foreach (Transform child in Chatroom.content.transform)
        {
            Destroy(child.gameObject);
        }


        yield return new WaitForSeconds(2f);
        SetChat(chattingTitle);
        slideUPDown.SlideUp(); // ToDo: 폰 키는 코드로 바꾸기

        /* ToDo: 폰에다 채팅방 생성해서 띄우고 Chatroom에 채팅방의 ScrollRect 넣기 */

        
        StartCoroutine("UpdatingChat");
    }
    
    public void StartChat(string chattingTitle)
    {
        PhoneScreen.SetActive(true);
        BlockScreen.SetActive(true);
        Debug.Log("Start Chat: " + chattingTitle);
        SetChat(chattingTitle);
        slideUPDown.SlideUp(); // ToDo: 폰 키는 코드로 바꾸기

        /* ToDo: 폰에다 채팅방 생성해서 띄우고 Chatroom에 채팅방의 ScrollRect 넣기 */
        
        StartCoroutine("UpdatingChat");
    } 

    public IEnumerator UpdatingChat(){
        Debug.Log("updating chat");
        Debug.Log(chatting.chatList.Count);
        foreach(chat c in chatting.chatList)
        {
            Debug.Log("generate chat");
            GenerateChat(c);
            yield return new WaitForSeconds(2f);  // 채팅 생성 속도
        }

        if(chatting.chooseImoticon){
            // ToDo: 이모티콘 선택 단계 추가
            // 각 이모티콘 선택 시 StartChat으로 해당하는 채팅 시작하도록 하기
            // 막아놓는거 풀기
            BlockScreen.SetActive(false);
            Emoticon1.interactable = true;
            Emoticon2.interactable = true;
            Emoticon3.interactable = true;
        }
        else{   // 이모티콘 선택 단계가 아니라면 (대화가 끝났다면) 
            
            BlackScreen.interactable = true;
            PhoneBase.interactable = true;
            BlockScreen.SetActive(false);
        }
    }

    public void EndChat()
    {
        StartCoroutine(EndingChat());
    }

    public IEnumerator EndingChat()
    {
        yield return new WaitForSeconds(1f);
        endMessage?.Invoke();
    }

    private void SetChat(string chattingTitle)
    {
        chatting = chattingDict[chattingTitle];

        proImage.sprite = profileImage[chatting.name];
        opponentName.text = chatting.name;

        //ChatBox_Opponent.transform.GetChild(1).GetComponent<Image>().sprite = profileImage[chatting.name];
        ChatBox_Opponent.transform.GetChild(1).GetComponent<TMP_Text>().text = chatting.name;

        //ChatBox_Opponent_Image.transform.GetChild(1).GetComponent<Image>().sprite = profileImage[chatting.name];
        ChatBox_Opponent_Image.transform.GetChild(1).GetComponent<TMP_Text>().text = chatting.name;
    }

    private void GenerateChat(chat c){
        GameObject ChatBox;
        Sprite opponentImage;


        if (c.image is not null){
            ChatBox = Instantiate(ChatBox_Opponent_Image, Chatroom.content.transform);
            opponentImage = Resources.Load<Sprite>(ImageSpritePath + c.image);
            Debug.Log(opponentImage.name);
            ChatBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = opponentImage;
        }
        else{
            ChatBox = Instantiate(ChatBox_Opponent, Chatroom.content.transform);
            Debug.Log(c.text);
            ChatBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = c.text;
            //ChatBox.GetComponent<AudioSource>().Play();  // 상대방 채팅 생성 시 알람
        }


        
        LayoutRebuilder.ForceRebuildLayoutImmediate(ChatBox.GetComponent<RectTransform>());

        // 채팅방의 content size fitter 동작 보장
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);

        Chatroom.verticalNormalizedPosition = 0f;  // 스크롤 내리기
    }

    public void ChooseEmoticon(int index)
    {
        GameObject Chatbox;
        Chatbox = Instantiate(ChatBox_Me, Chatroom.content.transform);
        Debug.Log(Chatroom.content.transform);

        Chatbox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Emoticon[index];
        Instantiate(Emoticon[index], Chatroom.content.transform);
        StartCoroutine(StartNextChat(index));
        PhoneBase.interactable = false;
        Emoticon1.interactable = false;
        Emoticon2.interactable = false;
        Emoticon3.interactable = false;
    }

    public IEnumerator StartNextChat(int index)
    {
        BlockScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartChat(chatting.nextChatting[index]);
    }

    private void LateStart()
    {
        // 신아산 호감도 20
        chattingDict.Add("신아산_20", new Chatting("신아산", false));
        chattingDict["신아산_20"].chatList.Add(new chat(false, "혹시 몰라서 카톡하는데"));
        chattingDict["신아산_20"].chatList.Add(new chat(false, "너 내일까지 블록체인응용 과제 제출인건 알고 있는거지?"));
        chattingDict["신아산_20"] = new Chatting("신아산", true, new string[] { "신아산_20_0", "신아산_20_1", "신아산_20_2" })
        {
            chatList = chattingDict["신아산_20"].chatList
        };

        chattingDict["신아산_20_0"] = new Chatting("신아산", false);
        chattingDict["신아산_20_0"].chatList.Add(new chat(false, "칠칠맞긴."));
        chattingDict["신아산_20_0"].chatList.Add(new chat(false, "그럴줄 알았어."));

        chattingDict["신아산_20_1"] = new Chatting("신아산", false);
        chattingDict["신아산_20_1"].chatList.Add(new chat(false, "칠칠맞긴."));
        chattingDict["신아산_20_1"].chatList.Add(new chat(false, "그럴줄 알았어."));

        chattingDict["신아산_20_2"] = new Chatting("신아산", false);
        chattingDict["신아산_20_2"].chatList.Add(new chat(false, "칠칠맞긴."));
        chattingDict["신아산_20_2"].chatList.Add(new chat(false, "그럴줄 알았어."));


        // 신아산 호감도 40
        chattingDict.Add("신아산_40", new Chatting("신아산", false));
        chattingDict["신아산_40"].chatList.Add(new chat(false, "내가 또 인터넷에서 웃긴 걸 봤는데, 그거 알아?"));
        chattingDict["신아산_40"].chatList.Add(new chat(false, "아칼리가 유명해지면 칼리스타래 ㅋㅋㅋㅋㅋㅋㅋㅋㅋ 엄청 웃겨서 지금 알바하는데도 자꾸 생각나ㅋㅋㅋㅋㅋㅋ"));
        chattingDict["신아산_40"] = new Chatting("신아산", true, new string[] { "신아산_40_0", "신아산_40_1", "신아산_40_2" })
        {
            chatList = chattingDict["신아산_40"].chatList
        };

        chattingDict.Add("신아산_40_0", new Chatting("신아산", false));
        chattingDict["신아산_40_0"].chatList.Add(new chat(false, "뭐야 그 반응."));
        chattingDict["신아산_40_0"].chatList.Add(new chat(false, "그래 나 아재개그 좋아한다 어쩔래? ㅡㅡ"));

        chattingDict.Add("신아산_40_1", new Chatting("신아산", false));
        chattingDict["신아산_40_1"].chatList.Add(new chat(false, "뭐야 그 반응."));
        chattingDict["신아산_40_1"].chatList.Add(new chat(false, "그래 나 아재개그 좋아한다 어쩔래? ㅡㅡ"));

        chattingDict.Add("신아산_40_2", new Chatting("신아산", false));
        chattingDict["신아산_40_2"].chatList.Add(new chat(false, "뭐야 그 반응."));
        chattingDict["신아산_40_2"].chatList.Add(new chat(false, "그래 나 아재개그 좋아한다 어쩔래? ㅡㅡ"));


        // 신아산 호감도 60
        chattingDict.Add("신아산_50", new Chatting("신아산", false));
        chattingDict["신아산_50"].chatList.Add(new chat(false, "음악하는 사람은 어떤거 같애?"));
        chattingDict["신아산_50"].chatList.Add(new chat(false, "아니 그게, 뭐랄까 누구는 그저 겉멋에 취해있다고도 하고 누구는 멋있다고들 그러잖아."));
        chattingDict["신아산_50"].chatList.Add(new chat(false, "별건 아니고 간만에 공연 준비를 하니까 머리가 복잡해서…"));
        chattingDict["신아산_50"] = new Chatting("신아산", true, new string[] { "신아산_50_0", "신아산_50_1", "신아산_50_2" })
        {
            chatList = chattingDict["신아산_50"].chatList
        };

        chattingDict.Add("신아산_50_0", new Chatting("신아산", false));
        chattingDict["신아산_50_0"].chatList.Add(new chat(false, "그러니까…"));
        chattingDict["신아산_50_0"].chatList.Add(new chat(false, "니 말은 좋다는거지?"));

        chattingDict.Add("신아산_50_1", new Chatting("신아산", false));
        chattingDict["신아산_50_1"].chatList.Add(new chat(false, "그러니까…"));
        chattingDict["신아산_50_1"].chatList.Add(new chat(false, "니 말은 좋다는거지?"));

        chattingDict.Add("신아산_50_2", new Chatting("신아산", false));
        chattingDict["신아산_50_2"].chatList.Add(new chat(false, "그러니까…"));
        chattingDict["신아산_50_2"].chatList.Add(new chat(false, "니 말은 좋다는거지?"));


        // 신아산 호감도 80
        chattingDict.Add("신아산_60", new Chatting("신아산", false));
        chattingDict["신아산_60"].chatList.Add(new chat(false, "이번에 공연에서 준비하는 노래야. 어때?"));
        chattingDict["신아산_60"].chatList.Add(new chat(false, "아직 미완성 단계긴 한데... 특별히 너한테만 들려주는거니까 너만 들어."));
        chattingDict["신아산_60"].chatList.Add(new chat(false, null, "문자_신아산_음성메시지"));
        chattingDict["신아산_60"] = new Chatting("신아산", true, new string[] { "신아산_60_0", "신아산_60_1", "신아산_60_2" })
        {
            chatList = chattingDict["신아산_60"].chatList
        };

        chattingDict.Add("신아산_60_0", new Chatting("신아산", false));
        chattingDict["신아산_60_0"].chatList.Add(new chat(false, "뭐야, 진짜 들어줬네."));
        chattingDict["신아산_60_0"].chatList.Add(new chat(false, "좋아해줘서 고마워. ㅡㅅㅡ"));

        chattingDict.Add("신아산_60_1", new Chatting("신아산", false));
        chattingDict["신아산_60_1"].chatList.Add(new chat(false, "뭐야, 진짜 들어줬네."));
        chattingDict["신아산_60_1"].chatList.Add(new chat(false, "좋아해줘서 고마워. ㅡㅅㅡ"));

        chattingDict.Add("신아산_60_2", new Chatting("신아산", false));
        chattingDict["신아산_60_2"].chatList.Add(new chat(false, "뭐야, 진짜 들어줬네."));
        chattingDict["신아산_60_2"].chatList.Add(new chat(false, "좋아해줘서 고마워. ㅡㅅㅡ"));


        // 신아산 호감도 100
        chattingDict.Add("신아산_70", new Chatting("신아산", false));
        chattingDict["신아산_70"].chatList.Add(new chat(false, "내가 계속 말했던 공연 있잖아... 그게 다음주에 하거든?"));
        chattingDict["신아산_70"].chatList.Add(new chat(false, "꼭 보러오라는 소리는 아니고, 그냥 표가 남아서 너한테 주는거야."));
        chattingDict["신아산_70"].chatList.Add(new chat(false, "자리는 바로 내 앞이니까 내가 베이스 치는거 보러오든가 말든가....", "문자_신아산_티켓"));
        chattingDict["신아산_70"] = new Chatting("신아산", true, new string[] { "신아산_70_0", "신아산_70_1", "신아산_70_2" })
        {
            chatList = chattingDict["신아산_70"].chatList
        };

        chattingDict.Add("신아산_70_0", new Chatting("신아산", false));
        chattingDict["신아산_70_0"].chatList.Add(new chat(false, "진짜 올거야??????????????"));
        chattingDict["신아산_70_0"].chatList.Add(new chat(false, "............."));
        chattingDict["신아산_70_0"].chatList.Add(new chat(false, "어….."));
        chattingDict["신아산_70_0"].chatList.Add(new chat(false, "나도 사......."));
        chattingDict["신아산_70_0"].chatList.Add(new chat(false, "사랑해."));

        chattingDict.Add("신아산_70_1", new Chatting("신아산", false));
        chattingDict["신아산_70_1"].chatList.Add(new chat(false, "진짜 올거야??????????????"));
        chattingDict["신아산_70_1"].chatList.Add(new chat(false, "............."));
        chattingDict["신아산_70_1"].chatList.Add(new chat(false, "어….."));
        chattingDict["신아산_70_1"].chatList.Add(new chat(false, "나도 사......."));
        chattingDict["신아산_70_1"].chatList.Add(new chat(false, "사랑해."));

        chattingDict.Add("신아산_70_2", new Chatting("신아산", false));
        chattingDict["신아산_70_2"].chatList.Add(new chat(false, "진짜 올거야??????????????"));
        chattingDict["신아산_70_2"].chatList.Add(new chat(false, "............."));
        chattingDict["신아산_70_2"].chatList.Add(new chat(false, "어….."));
        chattingDict["신아산_70_2"].chatList.Add(new chat(false, "나도 사......."));
        chattingDict["신아산_70_2"].chatList.Add(new chat(false, "사랑해."));


        // 양나현 호감도 20
        chattingDict.Add("양나현_20", new Chatting("양나현", false));
        chattingDict["양나현_20"].chatList.Add(new chat(false, "이거 봐봐. 바닷가에서 서로 폭죽 쏘고 노는 영상인데 너무 웃겨.", "문자_양나현_폭죽동영상"));
        chattingDict["양나현_20"] = new Chatting("양나현", true, new string[] { "양나현_20_0", "양나현_20_1", "양나현_20_2" })
        {
            chatList = chattingDict["양나현_20"].chatList
        };

        chattingDict["양나현_20_0"] = new Chatting("양나현", false);
        chattingDict["양나현_20_0"].chatList.Add(new chat(false, "나도 해보고 싶다. 잘못하면 불 나려나…"));

        chattingDict["양나현_20_1"] = new Chatting("양나현", false);
        chattingDict["양나현_20_1"].chatList.Add(new chat(false, "나도 해보고 싶다. 잘못하면 불 나려나…"));

        chattingDict["양나현_20_2"] = new Chatting("양나현", false);
        chattingDict["양나현_20_2"].chatList.Add(new chat(false, "나도 해보고 싶다. 잘못하면 불 나려나…"));


        // 양나현 호감도 40
        chattingDict.Add("양나현_40", new Chatting("양나현", false));
        chattingDict["양나현_40"].chatList.Add(new chat(false, "내일 머리 묶고 학교 갈까, 말까? 뭐가 나을 것 같아? 골라줘."));
        chattingDict["양나현_40"] = new Chatting("양나현", true, new string[] { "양나현_40_0", "양나현_40_1", "양나현_40_2" })
        {
            chatList = chattingDict["양나현_40"].chatList
        };

        chattingDict["양나현_40_0"] = new Chatting("양나현", false);
        chattingDict["양나현_40_0"].chatList.Add(new chat(false, "그냥 푸르는 게 낫겠다. 학교 끝나고 네컷 사진 찍으러 가기로 했는데 예쁘게 나오면 너도 보여줄게.<(*^U^))>"));

        chattingDict["양나현_40_1"] = new Chatting("양나현", false);
        chattingDict["양나현_40_1"].chatList.Add(new chat(false, "그냥 푸르는 게 낫겠다. 학교 끝나고 네컷 사진 찍으러 가기로 했는데 예쁘게 나오면 너도 보여줄게.<(*^U^))>"));

        chattingDict["양나현_40_2"] = new Chatting("양나현", false);
        chattingDict["양나현_40_2"].chatList.Add(new chat(false, "그냥 푸르는 게 낫겠다. 학교 끝나고 네컷 사진 찍으러 가기로 했는데 예쁘게 나오면 너도 보여줄게.<(*^U^))>"));


        // 양나현 호감도 60
        chattingDict.Add("양나현_50", new Chatting("양나현", false));
        chattingDict["양나현_50"].chatList.Add(new chat(false, "좀 뜬금없는데… 나는 다시 태어날 수 있으면 고양이로 살고 싶어."));
        chattingDict["양나현_50"].chatList.Add(new chat(false, "일단 귀엽고, 유연하고, 음… 어쨌든 귀엽잖아."));
        chattingDict["양나현_50"].chatList.Add(new chat(false, "너는 이런 생각 해본 적 없어?"));
        chattingDict["양나현_50"] = new Chatting("양나현", true, new string[] { "양나현_50_0", "양나현_50_1", "양나현_50_2" })
        {
            chatList = chattingDict["양나현_50"].chatList
        };

        chattingDict["양나현_50_0"] = new Chatting("양나현", false);
        chattingDict["양나현_50_0"].chatList.Add(new chat(false, "뭐? 내가 더 귀엽다고?"));
        chattingDict["양나현_50_0"].chatList.Add(new chat(false, "알긴 알아… ^-ㅅ-^"));

        chattingDict["양나현_50_1"] = new Chatting("양나현", false);
        chattingDict["양나현_50_1"].chatList.Add(new chat(false, "뭐? 내가 더 귀엽다고?"));
        chattingDict["양나현_50_1"].chatList.Add(new chat(false, "알긴 알아… ^-ㅅ-^"));

        chattingDict["양나현_50_2"] = new Chatting("양나현", false);
        chattingDict["양나현_50_2"].chatList.Add(new chat(false, "뭐? 내가 더 귀엽다고?"));
        chattingDict["양나현_50_2"].chatList.Add(new chat(false, "알긴 알아… ^-ㅅ-^"));


        // 양나현 호감도 80
        chattingDict.Add("양나현_60", new Chatting("양나현", false));
        chattingDict["양나현_60"].chatList.Add(new chat(false, "아, 내일 학교 가기 싫다."));
        chattingDict["양나현_60"].chatList.Add(new chat(false, "그래도 가야겠지..."));
        chattingDict["양나현_60"] = new Chatting("양나현", true, new string[] { "양나현_60_0", "양나현_60_1", "양나현_60_2" })
        {
            chatList = chattingDict["양나현_60"].chatList
        };

        chattingDict["양나현_60_0"] = new Chatting("양나현", false);
        chattingDict["양나현_60_0"].chatList.Add(new chat(false, "요즘 학교 가는 거 좀 재미있다?"));
        chattingDict["양나현_60_0"].chatList.Add(new chat(false, "원래 같았으면 이미 여섯 번은 결석했을 텐데."));
        chattingDict["양나현_60_0"].chatList.Add(new chat(false, "딱히 너 때문은 아니야. ~(^o^~)(~^o^)~"));
        chattingDict["양나현_60_0"].chatList.Add(new chat(false, "내일 봐."));

        chattingDict["양나현_60_1"] = new Chatting("양나현", false);
        chattingDict["양나현_60_1"].chatList.Add(new chat(false, "요즘 학교 가는 거 좀 재미있다?"));
        chattingDict["양나현_60_1"].chatList.Add(new chat(false, "원래 같았으면 이미 여섯 번은 결석했을 텐데."));
        chattingDict["양나현_60_1"].chatList.Add(new chat(false, "딱히 너 때문은 아니야. ~(^o^~)(~^o^)~"));
        chattingDict["양나현_60_1"].chatList.Add(new chat(false, "내일 봐."));

        chattingDict["양나현_60_2"] = new Chatting("양나현", false);
        chattingDict["양나현_60_2"].chatList.Add(new chat(false, "요즘 학교 가는 거 좀 재미있다?"));
        chattingDict["양나현_60_2"].chatList.Add(new chat(false, "원래 같았으면 이미 여섯 번은 결석했을 텐데."));
        chattingDict["양나현_60_2"].chatList.Add(new chat(false, "딱히 너 때문은 아니야. ~(^o^~)(~^o^)~"));
        chattingDict["양나현_60_2"].chatList.Add(new chat(false, "내일 봐."));


        // 양나현 호감도 100
        chattingDict.Add("양나현_70", new Chatting("양나현", false));
        chattingDict["양나현_70"].chatList.Add(new chat(false, "나 요즘 생각이 좀 많아."));
        chattingDict["양나현_70"].chatList.Add(new chat(false, "근데 대부분은 너에 대한 생각인 것 같아."));
        chattingDict["양나현_70"] = new Chatting("양나현", true, new string[] { "양나현_70_0", "양나현_70_1", "양나현_70_2" })
        {
            chatList = chattingDict["양나현_70"].chatList
        };

        chattingDict["양나현_70_0"] = new Chatting("양나현", false);
        chattingDict["양나현_70_0"].chatList.Add(new chat(false, "그동안 솔직히 가볍게만 누굴 만나왔는데,"));
        chattingDict["양나현_70_0"].chatList.Add(new chat(false, "그때랑은 느낌이 좀 달라."));
        chattingDict["양나현_70_0"].chatList.Add(new chat(false, "몰라, 이런 적 처음이야..."));
        chattingDict["양나현_70_0"].chatList.Add(new chat(false, "부끄러워서 그만 말하고 싶어."));
        chattingDict["양나현_70_0"] = new Chatting("양나현", true, new string[] { "양나현_70_0_0", "양나현_70_0_1", "양나현_70_0_2" })
        {
            chatList = chattingDict["양나현_70_0"].chatList
        };

        chattingDict["양나현_70_0_0"] = new Chatting("양나현", false);
        chattingDict["양나현_70_0_0"].chatList.Add(new chat(false, "나도 네가 좋아."));
        chattingDict["양나현_70_0_0"].chatList.Add(new chat(false, "목소리 듣고 싶어."));
        chattingDict["양나현_70_0_0"].chatList.Add(new chat(false, "전화할래?"));

        chattingDict["양나현_70_0_1"] = new Chatting("양나현", false);
        chattingDict["양나현_70_0_1"].chatList.Add(new chat(false, "나도 네가 좋아."));
        chattingDict["양나현_70_0_1"].chatList.Add(new chat(false, "목소리 듣고 싶어."));
        chattingDict["양나현_70_0_1"].chatList.Add(new chat(false, "전화할래?"));

        chattingDict["양나현_70_0_2"] = new Chatting("양나현", false);
        chattingDict["양나현_70_0_2"].chatList.Add(new chat(false, "나도 네가 좋아."));
        chattingDict["양나현_70_0_2"].chatList.Add(new chat(false, "목소리 듣고 싶어."));
        chattingDict["양나현_70_0_2"].chatList.Add(new chat(false, "전화할래?"));


        chattingDict["양나현_70_1"] = new Chatting("양나현", false);
        chattingDict["양나현_70_1"].chatList.Add(new chat(false, "그동안 솔직히 가볍게만 누굴 만나왔는데,"));
        chattingDict["양나현_70_1"].chatList.Add(new chat(false, "그때랑은 느낌이 좀 달라."));
        chattingDict["양나현_70_1"].chatList.Add(new chat(false, "몰라, 이런 적 처음이야..."));
        chattingDict["양나현_70_1"].chatList.Add(new chat(false, "부끄러워서 그만 말하고 싶어."));
        chattingDict["양나현_70_1"] = new Chatting("양나현", true, new string[] { "양나현_70_1_0", "양나현_70_1_1", "양나현_70_1_2" })
        {
            chatList = chattingDict["양나현_70_1"].chatList
        };

        chattingDict["양나현_70_1_0"] = new Chatting("양나현", false);
        chattingDict["양나현_70_1_0"].chatList.Add(new chat(false, "나도 네가 좋아."));
        chattingDict["양나현_70_1_0"].chatList.Add(new chat(false, "목소리 듣고 싶어."));
        chattingDict["양나현_70_1_0"].chatList.Add(new chat(false, "전화할래?"));

        chattingDict["양나현_70_1_1"] = new Chatting("양나현", false);
        chattingDict["양나현_70_1_1"].chatList.Add(new chat(false, "나도 네가 좋아."));
        chattingDict["양나현_70_1_1"].chatList.Add(new chat(false, "목소리 듣고 싶어."));
        chattingDict["양나현_70_1_1"].chatList.Add(new chat(false, "전화할래?"));

        chattingDict["양나현_70_1_2"] = new Chatting("양나현", false);
        chattingDict["양나현_70_1_2"].chatList.Add(new chat(false, "나도 네가 좋아."));
        chattingDict["양나현_70_1_2"].chatList.Add(new chat(false, "목소리 듣고 싶어."));
        chattingDict["양나현_70_1_2"].chatList.Add(new chat(false, "전화할래?"));


        chattingDict["양나현_70_2"] = new Chatting("양나현", false);
        chattingDict["양나현_70_2"].chatList.Add(new chat(false, "그동안 솔직히 가볍게만 누굴 만나왔는데,"));
        chattingDict["양나현_70_2"].chatList.Add(new chat(false, "그때랑은 느낌이 좀 달라."));
        chattingDict["양나현_70_2"].chatList.Add(new chat(false, "몰라, 이런 적 처음이야..."));
        chattingDict["양나현_70_2"].chatList.Add(new chat(false, "부끄러워서 그만 말하고 싶어."));
        chattingDict["양나현_70_2"] = new Chatting("양나현", true, new string[] { "양나현_70_2_0", "양나현_70_2_1", "양나현_70_2_2" })
        {
            chatList = chattingDict["양나현_70_2"].chatList
        };

        chattingDict["양나현_70_2_0"] = new Chatting("양나현", false);
        chattingDict["양나현_70_2_0"].chatList.Add(new chat(false, "나도 네가 좋아."));
        chattingDict["양나현_70_2_0"].chatList.Add(new chat(false, "목소리 듣고 싶어."));
        chattingDict["양나현_70_2_0"].chatList.Add(new chat(false, "전화할래?"));

        chattingDict["양나현_70_2_1"] = new Chatting("양나현", false);
        chattingDict["양나현_70_2_1"].chatList.Add(new chat(false, "나도 네가 좋아."));
        chattingDict["양나현_70_2_1"].chatList.Add(new chat(false, "목소리 듣고 싶어."));
        chattingDict["양나현_70_2_1"].chatList.Add(new chat(false, "전화할래?"));

        chattingDict["양나현_70_2_2"] = new Chatting("양나현", false);
        chattingDict["양나현_70_2_2"].chatList.Add(new chat(false, "나도 네가 좋아."));
        chattingDict["양나현_70_2_2"].chatList.Add(new chat(false, "목소리 듣고 싶어."));
        chattingDict["양나현_70_2_2"].chatList.Add(new chat(false, "전화할래?"));

        // 서은표 호감도 20
        chattingDict.Add("서은표_20", new Chatting("서은표", false));
        chattingDict["서은표_20"].chatList.Add(new chat(false, $"{meName} ~~ 뭐하고 있어?"));
        chattingDict["서은표_20"] = new Chatting("서은표", true, new string[] { "서은표_20_0", "서은표_20_1", "서은표_20_2" })
        {
            chatList = chattingDict["서은표_20"].chatList
        };

        chattingDict["서은표_20_0"] = new Chatting("서은표", false);
        chattingDict["서은표_20_0"].chatList.Add(new chat(false, "나는 축구 연습 갔다가 집 와서 씻고…… 애니 보고 있어."));
        chattingDict["서은표_20_0"].chatList.Add(new chat(false, "(// ^^ //)"));
        chattingDict["서은표_20_0"].chatList.Add(new chat(false, "오늘 하루 중 최고의 시간이야!!"));

        chattingDict["서은표_20_1"] = new Chatting("서은표", false);
        chattingDict["서은표_20_1"].chatList.Add(new chat(false, "나는 축구 연습 갔다가 집 와서 씻고…… 애니 보고 있어."));
        chattingDict["서은표_20_1"].chatList.Add(new chat(false, "(// ^^ //)"));
        chattingDict["서은표_20_1"].chatList.Add(new chat(false, "오늘 하루 중 최고의 시간이야!!"));

        chattingDict["서은표_20_2"] = new Chatting("서은표", false);
        chattingDict["서은표_20_2"].chatList.Add(new chat(false, "나는 축구 연습 갔다가 집 와서 씻고…… 애니 보고 있어."));
        chattingDict["서은표_20_2"].chatList.Add(new chat(false, "(// ^^ //)"));
        chattingDict["서은표_20_2"].chatList.Add(new chat(false, "오늘 하루 중 최고의 시간이야!!"));

        // 서은표 호감도 40
        chattingDict.Add("서은표_40", new Chatting("서은표", false));
        chattingDict["서은표_40"].chatList.Add(new chat(false, $"{meName}아, 요즘 너랑 학교 다니는 거 너무 좋아."));
        chattingDict["서은표_40"] = new Chatting("서은표", true, new string[] { "서은표_40_0", "서은표_40_1", "서은표_40_2" })
        {
            chatList = chattingDict["서은표_40"].chatList
        };

        chattingDict["서은표_40_0"] = new Chatting("서은표", false);
        chattingDict["서은표_40_0"].chatList.Add(new chat(false, "보고 싶어… (// ^^ //)"));
        chattingDict["서은표_40_0"].chatList.Add(new chat(false, "이런 말 좀 그런가??! 무지 부끄럽네…"));
        chattingDict["서은표_40_0"].chatList.Add(new chat(false, "근데 진짜야…"));

        chattingDict["서은표_40_1"] = new Chatting("서은표", false);
        chattingDict["서은표_40_1"].chatList.Add(new chat(false, "보고 싶어… (// ^^ //)"));
        chattingDict["서은표_40_1"].chatList.Add(new chat(false, "이런 말 좀 그런가??! 무지 부끄럽네…"));
        chattingDict["서은표_40_1"].chatList.Add(new chat(false, "근데 진짜야…"));

        chattingDict["서은표_40_2"] = new Chatting("서은표", false);
        chattingDict["서은표_40_2"].chatList.Add(new chat(false, "보고 싶어… (// ^^ //)"));
        chattingDict["서은표_40_2"].chatList.Add(new chat(false, "이런 말 좀 그런가??! 무지 부끄럽네…"));
        chattingDict["서은표_40_2"].chatList.Add(new chat(false, "근데 진짜야…"));

        // 서은표 호감도 60
        chattingDict.Add("서은표_50", new Chatting("서은표", false));
        chattingDict["서은표_50"].chatList.Add(new chat(false, $"{meName}, 내일 학교 몇 시에 올 거야?"));
        chattingDict["서은표_50"] = new Chatting("서은표", true, new string[] { "서은표_50_0", "서은표_50_1", "서은표_50_2" })
        {
            chatList = chattingDict["서은표_50"].chatList
        };

        chattingDict["서은표_50_0"] = new Chatting("서은표", false);
        chattingDict["서은표_50_0"].chatList.Add(new chat(false, "내일 후문으로 등교할 거지? ㅎㅎㅎ"));
        chattingDict["서은표_50_0"] = new Chatting("서은표", true, new string[] { "서은표_50_0_0", "서은표_50_0_1", "서은표_50_0_2" })
        {
            chatList = chattingDict["서은표_50_0"].chatList
        };
        chattingDict["서은표_50_0_0"] = new Chatting("서은표", false);
        chattingDict["서은표_50_0_0"].chatList.Add(new chat(false, "(// ^^ //) 좋아좋아~~ 얼른 보고 싶당."));
        chattingDict["서은표_50_0_0"].chatList.Add(new chat(false, "나도 네 시간에 맞춰서 등교할까 봐~~ 잘 자!"));

        chattingDict["서은표_50_0_1"] = new Chatting("서은표", false);
        chattingDict["서은표_50_0_1"].chatList.Add(new chat(false, "(// ^^ //) 좋아좋아~~ 얼른 보고 싶당."));
        chattingDict["서은표_50_0_1"].chatList.Add(new chat(false, "나도 네 시간에 맞춰서 등교할까 봐~~ 잘 자!"));

        chattingDict["서은표_50_0_2"] = new Chatting("서은표", false);
        chattingDict["서은표_50_0_2"].chatList.Add(new chat(false, "(// ^^ //) 좋아좋아~~ 얼른 보고 싶당."));
        chattingDict["서은표_50_0_2"].chatList.Add(new chat(false, "나도 네 시간에 맞춰서 등교할까 봐~~ 잘 자!"));

        chattingDict["서은표_50_1"] = new Chatting("서은표", false);
        chattingDict["서은표_50_1"].chatList.Add(new chat(false, "내일 후문으로 등교할 거지? ㅎㅎㅎ"));
        chattingDict["서은표_50_1"] = new Chatting("서은표", true, new string[] { "서은표_50_1_0", "서은표_50_1_1", "서은표_50_1_2" })
        {
            chatList = chattingDict["서은표_50_1"].chatList
        };
        chattingDict["서은표_50_1_0"] = new Chatting("서은표", false);
        chattingDict["서은표_50_1_0"].chatList.Add(new chat(false, "(// ^^ //) 좋아좋아~~ 얼른 보고 싶당."));
        chattingDict["서은표_50_1_0"].chatList.Add(new chat(false, "나도 네 시간에 맞춰서 등교할까 봐~~ 잘 자!"));

        chattingDict["서은표_50_1_1"] = new Chatting("서은표", false);
        chattingDict["서은표_50_1_1"].chatList.Add(new chat(false, "(// ^^ //) 좋아좋아~~ 얼른 보고 싶당."));
        chattingDict["서은표_50_1_1"].chatList.Add(new chat(false, "나도 네 시간에 맞춰서 등교할까 봐~~ 잘 자!"));

        chattingDict["서은표_50_1_2"] = new Chatting("서은표", false);
        chattingDict["서은표_50_1_2"].chatList.Add(new chat(false, "(// ^^ //) 좋아좋아~~ 얼른 보고 싶당."));
        chattingDict["서은표_50_1_2"].chatList.Add(new chat(false, "나도 네 시간에 맞춰서 등교할까 봐~~ 잘 자!"));

        chattingDict["서은표_50_2"] = new Chatting("서은표", false);
        chattingDict["서은표_50_2"].chatList.Add(new chat(false, "내일 후문으로 등교할 거지? ㅎㅎㅎ"));
        chattingDict["서은표_50_2"] = new Chatting("서은표", true, new string[] { "서은표_50_2_0", "서은표_50_2_1", "서은표_50_2_2" })
        {
            chatList = chattingDict["서은표_50_2"].chatList
        };
        chattingDict["서은표_50_2_0"] = new Chatting("서은표", false);
        chattingDict["서은표_50_2_0"].chatList.Add(new chat(false, "(// ^^ //) 좋아좋아~~ 얼른 보고 싶당."));
        chattingDict["서은표_50_2_0"].chatList.Add(new chat(false, "나도 네 시간에 맞춰서 등교할까 봐~~ 잘 자!"));

        chattingDict["서은표_50_2_1"] = new Chatting("서은표", false);
        chattingDict["서은표_50_2_1"].chatList.Add(new chat(false, "(// ^^ //) 좋아좋아~~ 얼른 보고 싶당."));
        chattingDict["서은표_50_2_1"].chatList.Add(new chat(false, "나도 네 시간에 맞춰서 등교할까 봐~~ 잘 자!"));

        chattingDict["서은표_50_2_2"] = new Chatting("서은표", false);
        chattingDict["서은표_50_2_2"].chatList.Add(new chat(false, "(// ^^ //) 좋아좋아~~ 얼른 보고 싶당."));
        chattingDict["서은표_50_2_2"].chatList.Add(new chat(false, "나도 네 시간에 맞춰서 등교할까 봐~~ 잘 자!"));

        // 서은표 호감도 80
        chattingDict.Add("서은표_60", new Chatting("서은표", false));
        chattingDict["서은표_60"].chatList.Add(new chat(false, $"{meName}, 학교에 손수건 두고 간 거 알아???", "문자_서은표_손수건"));
        chattingDict["서은표_60"].chatList.Add(new chat(false, $"{meName} 거 맞지? 다행히 내가 챙겨 왔어."));
        chattingDict["서은표_60"] = new Chatting("서은표", true, new string[] { "서은표_60_0", "서은표_60_1", "서은표_60_2" })
        {
            chatList = chattingDict["서은표_60"].chatList
        };

        chattingDict["서은표_60_0"] = new Chatting("서은표", false);
        chattingDict["서은표_60_0"].chatList.Add(new chat(false, "돌려줄까, 말까~ ㅎㅎㅎ (// ^^ //)"));
        chattingDict["서은표_60_0"].chatList.Add(new chat(false, "손수건에서 네 냄새 나서, 꼭 같이 있는 것 같아."));
        chattingDict["서은표_60_0"].chatList.Add(new chat(false, "좋다……"));

        chattingDict["서은표_60_1"] = new Chatting("서은표", false);
        chattingDict["서은표_60_1"].chatList.Add(new chat(false, "돌려줄까, 말까~ ㅎㅎㅎ (// ^^ //)"));
        chattingDict["서은표_60_1"].chatList.Add(new chat(false, "손수건에서 네 냄새 나서, 꼭 같이 있는 것 같아."));
        chattingDict["서은표_60_1"].chatList.Add(new chat(false, "좋다……"));

        chattingDict["서은표_60_2"] = new Chatting("서은표", false);
        chattingDict["서은표_60_2"].chatList.Add(new chat(false, "돌려줄까, 말까~ ㅎㅎㅎ (// ^^ //)"));
        chattingDict["서은표_60_2"].chatList.Add(new chat(false, "손수건에서 네 냄새 나서, 꼭 같이 있는 것 같아."));
        chattingDict["서은표_60_2"].chatList.Add(new chat(false, "좋다……"));

        // 서은표 호감도 100
        chattingDict.Add("서은표_70", new Chatting("서은표", false));
        chattingDict["서은표_70"].chatList.Add(new chat(false, $"{meName}, 아까 잠깐 낮잠을 잤는데, 네가 꿈에 나왔어."));
        chattingDict["서은표_70"] = new Chatting("서은표", true, new string[] { "서은표_70_0", "서은표_70_1", "서은표_70_2" })
        {
            chatList = chattingDict["서은표_70"].chatList
        };

        chattingDict["서은표_70_0"] = new Chatting("서은표", false);
        chattingDict["서은표_70_0"].chatList.Add(new chat(false, "꿈에서… (// ^^ //)…"));
        chattingDict["서은표_70_0"].chatList.Add(new chat(false, "아 말하기 좀 그런데?!?"));
        chattingDict["서은표_70_0"].chatList.Add(new chat(false, $"{meName}이랑 같이 사는 꿈을 꿨어…"));
        chattingDict["서은표_70_0"].chatList.Add(new chat(false, "말 나온 김에, 그냥 우리 집 와서 살래?"));
        chattingDict["서은표_70_0"] = new Chatting("서은표", true, new string[] { "서은표_70_0_0", "서은표_70_0_1", "서은표_70_0_2" })
        {
            chatList = chattingDict["서은표_70_0"].chatList
        };

        chattingDict["서은표_70_0_0"] = new Chatting("서은표", false);
        chattingDict["서은표_70_0_0"].chatList.Add(new chat(false, $"{meName}이랑 떨어져 있는 시간들이 너무 힘들어. 그냥 같이 살고 싶다… 조금 진지할지도…"));
        chattingDict["서은표_70_0_0"].chatList.Add(new chat(false, "꿈에서 너무 행복했지 뭐야……"));
        chattingDict["서은표_70_0_0"].chatList.Add(new chat(false, "진짜 같이 살래? 너만 좋다고 하면 당장이라도 좋아… 보고 싶어."));
        chattingDict["서은표_70_0_0"].chatList.Add(new chat(false, $"내일이 얼른 왔으면 좋겠다… ㅎㅎㅎ 사랑해 {meName}"));

        chattingDict["서은표_70_0_1"] = new Chatting("서은표", false);
        chattingDict["서은표_70_0_1"].chatList.Add(new chat(false, $"{meName}이랑 떨어져 있는 시간들이 너무 힘들어. 그냥 같이 살고 싶다… 조금 진지할지도…"));
        chattingDict["서은표_70_0_1"].chatList.Add(new chat(false, "꿈에서 너무 행복했지 뭐야……"));
        chattingDict["서은표_70_0_1"].chatList.Add(new chat(false, "진짜 같이 살래? 너만 좋다고 하면 당장이라도 좋아… 보고 싶어."));
        chattingDict["서은표_70_0_1"].chatList.Add(new chat(false, $"내일이 얼른 왔으면 좋겠다… ㅎㅎㅎ 사랑해 {meName}"));

        chattingDict["서은표_70_0_2"] = new Chatting("서은표", false);
        chattingDict["서은표_70_0_2"].chatList.Add(new chat(false, $"{meName}이랑 떨어져 있는 시간들이 너무 힘들어. 그냥 같이 살고 싶다… 조금 진지할지도…"));
        chattingDict["서은표_70_0_2"].chatList.Add(new chat(false, "꿈에서 너무 행복했지 뭐야……"));
        chattingDict["서은표_70_0_2"].chatList.Add(new chat(false, "진짜 같이 살래? 너만 좋다고 하면 당장이라도 좋아… 보고 싶어."));
        chattingDict["서은표_70_0_2"].chatList.Add(new chat(false, $"내일이 얼른 왔으면 좋겠다… ㅎㅎㅎ 사랑해 {meName}"));


        chattingDict["서은표_70_1"] = new Chatting("서은표", false);
        chattingDict["서은표_70_1"].chatList.Add(new chat(false, "꿈에서… (// ^^ //)…"));
        chattingDict["서은표_70_1"].chatList.Add(new chat(false, "아 말하기 좀 그런데?!?"));
        chattingDict["서은표_70_1"].chatList.Add(new chat(false, $"{meName}이랑 같이 사는 꿈을 꿨어…"));
        chattingDict["서은표_70_1"].chatList.Add(new chat(false, "말 나온 김에, 그냥 우리 집 와서 살래?"));
        chattingDict["서은표_70_1"] = new Chatting("서은표", true, new string[] { "서은표_70_1_0", "서은표_70_1_1", "서은표_70_1_2" })
        {
            chatList = chattingDict["서은표_70_1"].chatList
        };

        chattingDict["서은표_70_1_0"] = new Chatting("서은표", false);
        chattingDict["서은표_70_1_0"].chatList.Add(new chat(false, $"{meName}이랑 떨어져 있는 시간들이 너무 힘들어. 그냥 같이 살고 싶다… 조금 진지할지도…"));
        chattingDict["서은표_70_1_0"].chatList.Add(new chat(false, "꿈에서 너무 행복했지 뭐야……"));
        chattingDict["서은표_70_1_0"].chatList.Add(new chat(false, "진짜 같이 살래? 너만 좋다고 하면 당장이라도 좋아… 보고 싶어."));
        chattingDict["서은표_70_1_0"].chatList.Add(new chat(false, $"내일이 얼른 왔으면 좋겠다… ㅎㅎㅎ 사랑해 {meName}"));

        chattingDict["서은표_70_1_1"] = new Chatting("서은표", false);
        chattingDict["서은표_70_1_1"].chatList.Add(new chat(false, $"{meName}이랑 떨어져 있는 시간들이 너무 힘들어. 그냥 같이 살고 싶다… 조금 진지할지도…"));
        chattingDict["서은표_70_1_1"].chatList.Add(new chat(false, "꿈에서 너무 행복했지 뭐야……"));
        chattingDict["서은표_70_1_1"].chatList.Add(new chat(false, "진짜 같이 살래? 너만 좋다고 하면 당장이라도 좋아… 보고 싶어."));
        chattingDict["서은표_70_1_1"].chatList.Add(new chat(false, $"내일이 얼른 왔으면 좋겠다… ㅎㅎㅎ 사랑해 {meName}"));

        chattingDict["서은표_70_1_2"] = new Chatting("서은표", false);
        chattingDict["서은표_70_1_2"].chatList.Add(new chat(false, $"{meName}이랑 떨어져 있는 시간들이 너무 힘들어. 그냥 같이 살고 싶다… 조금 진지할지도…"));
        chattingDict["서은표_70_1_2"].chatList.Add(new chat(false, "꿈에서 너무 행복했지 뭐야……"));
        chattingDict["서은표_70_1_2"].chatList.Add(new chat(false, "진짜 같이 살래? 너만 좋다고 하면 당장이라도 좋아… 보고 싶어."));
        chattingDict["서은표_70_1_2"].chatList.Add(new chat(false, $"내일이 얼른 왔으면 좋겠다… ㅎㅎㅎ 사랑해 {meName}"));


        chattingDict["서은표_70_2"] = new Chatting("서은표", false);
        chattingDict["서은표_70_2"].chatList.Add(new chat(false, "꿈에서… (// ^^ //)…"));
        chattingDict["서은표_70_2"].chatList.Add(new chat(false, "아 말하기 좀 그런데?!?"));
        chattingDict["서은표_70_2"].chatList.Add(new chat(false, $"{meName}이랑 같이 사는 꿈을 꿨어…"));
        chattingDict["서은표_70_2"].chatList.Add(new chat(false, "말 나온 김에, 그냥 우리 집 와서 살래?"));
        chattingDict["서은표_70_2"] = new Chatting("서은표", true, new string[] { "서은표_70_2_0", "서은표_70_2_1", "서은표_70_2_2" })
        {
            chatList = chattingDict["서은표_70_2"].chatList
        };

        chattingDict["서은표_70_2_0"] = new Chatting("서은표", false);
        chattingDict["서은표_70_2_0"].chatList.Add(new chat(false, $"{meName}이랑 떨어져 있는 시간들이 너무 힘들어. 그냥 같이 살고 싶다… 조금 진지할지도…"));
        chattingDict["서은표_70_2_0"].chatList.Add(new chat(false, "꿈에서 너무 행복했지 뭐야……"));
        chattingDict["서은표_70_2_0"].chatList.Add(new chat(false, "진짜 같이 살래? 너만 좋다고 하면 당장이라도 좋아… 보고 싶어."));
        chattingDict["서은표_70_2_0"].chatList.Add(new chat(false, $"내일이 얼른 왔으면 좋겠다… ㅎㅎㅎ 사랑해 {meName}"));

        chattingDict["서은표_70_2_1"] = new Chatting("서은표", false);
        chattingDict["서은표_70_2_1"].chatList.Add(new chat(false, $"{meName}이랑 떨어져 있는 시간들이 너무 힘들어. 그냥 같이 살고 싶다… 조금 진지할지도…"));
        chattingDict["서은표_70_2_1"].chatList.Add(new chat(false, "꿈에서 너무 행복했지 뭐야……"));
        chattingDict["서은표_70_2_1"].chatList.Add(new chat(false, "진짜 같이 살래? 너만 좋다고 하면 당장이라도 좋아… 보고 싶어."));
        chattingDict["서은표_70_2_1"].chatList.Add(new chat(false, $"내일이 얼른 왔으면 좋겠다… ㅎㅎㅎ 사랑해 {meName}"));

        chattingDict["서은표_70_2_2"] = new Chatting("서은표", false);
        chattingDict["서은표_70_2_2"].chatList.Add(new chat(false, $"{meName}이랑 떨어져 있는 시간들이 너무 힘들어. 그냥 같이 살고 싶다… 조금 진지할지도…"));
        chattingDict["서은표_70_2_2"].chatList.Add(new chat(false, "꿈에서 너무 행복했지 뭐야……"));
        chattingDict["서은표_70_2_2"].chatList.Add(new chat(false, "진짜 같이 살래? 너만 좋다고 하면 당장이라도 좋아… 보고 싶어."));
        chattingDict["서은표_70_2_2"].chatList.Add(new chat(false, $"내일이 얼른 왔으면 좋겠다… ㅎㅎㅎ 사랑해 {meName}"));

    }
}

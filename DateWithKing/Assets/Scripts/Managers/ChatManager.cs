using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;
using UnityEngine.EventSystems;

public class ChatManager : Singleton<ChatManager> // ToDo: 싱글톤 상속해야함
{ 
    const string spritePath = "";
    public string[] profileSpriteName = {"양나현", "서은표", "신아산"};  // 스프라이트 이름
    public Dictionary<string, Sprite> profileImage = new Dictionary<string, Sprite>();  // 프로필 이미지 딕셔너리
    public Dictionary<string, Chatting> chattingDict = new Dictionary<string, Chatting>();  // 채팅목록 딕셔너리

    [SerializeField]
    private GameObject ChatBox_Me;  // 나의 채팅박스 프리팹

    [SerializeField]
    private GameObject ChatBox_Opponent;  // 상대방 채팅박스 프리팹

    Chatting chatting;  // 지금 출력중인 채팅
    ScrollRect Chatroom;  // 지금 채팅을 출력중인 채팅방
    Action endMessage;  // 채팅 끝나면 실행할 함수
    SlideUpUI slideUPDown;

    private void Start()
    {
        foreach(var sprite in profileSpriteName)
            profileImage.Add(sprite, Resources.Load<Sprite>(spritePath+sprite));

        slideUPDown.SlideUp();
        //StartChat();
    }

    public struct chat{
        public bool me;
        public string text; // 대사
        public string emoticon;  // 이모티콘
        public chat (bool me,string text = null, string emoticon = null)
        {
            this.me = me;
            this.text = text;
            this.emoticon = emoticon;
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
        Debug.Log("Start Chat: " + chattingTitle);
        SetChat(chattingTitle);
        slideUPDown.SlideUp(); // ToDo: 폰 키는 코드로 바꾸기

        /* ToDo: 폰에다 채팅방 생성해서 띄우고 Chatroom에 채팅방의 ScrollRect 넣기 */
        
        this.endMessage = endMessage;
        StartCoroutine("UpdatingChat");
    } 

    public IEnumerator UpdatingChat(){
        
        foreach(chat c in chatting.chatList)
        {   
            GenerateChat(c);
            yield return new WaitForSeconds(2f);  // 채팅 생성 속도
        }

        if(chatting.chooseImoticon){
            // ToDo: 이모티콘 선택 단계 추가
            // 각 이모티콘 선택 시 StartChat으로 해당하는 채팅 시작하도록 하기
            // 막아놓는거 풀기

        }
        else{   // 이모티콘 선택 단계가 아니라면 (대화가 끝났다면) 
            endMessage?.Invoke();
        }
    }

    private void SetChat(string chattingTitle)
    {
        chatting = chattingDict[chattingTitle];

        ChatBox_Opponent.transform.GetChild(1).GetComponent<Image>().sprite = profileImage[chatting.name];
        ChatBox_Opponent.transform.GetChild(2).GetComponent<TMP_Text>().text = chatting.name;
    }

    private void GenerateChat(chat c){
        GameObject ChatBox;

        if(c.me){
            ChatBox = Instantiate(ChatBox_Me, Chatroom.content.transform);
            Instantiate(Resources.Load<GameObject>(c.emoticon), Chatroom.content.transform);
        }
        else{
            ChatBox = Instantiate(ChatBox_Opponent, Chatroom.content.transform);
            ChatBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = c.text;
            ChatBox.GetComponent<AudioSource>().Play();  // 상대방 채팅 생성 시 알람
        }

        

        LayoutRebuilder.ForceRebuildLayoutImmediate(ChatBox.GetComponent<RectTransform>());

        // 채팅방의 content size fitter 동작 보장
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);

        Chatroom.verticalNormalizedPosition = 0f;  // 스크롤 내리기
    }

    public void ChooseEmoticon(int index)
    {
        StartChat(chatting.nextChatting[index]);
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;
using UnityEngine.EventSystems;
using System.Reflection;
using UnityEngine.Localization.Settings;

public class ChatManager : SceneSingleton<ChatManager> // ToDo: 싱글톤 상속해야함
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

    [SerializeField] GameObject BackButton;

    [SerializeField] private Sprite[] Emoticon;

    Chatting chatting;  // 지금 출력중인 채팅
    [SerializeField]
    ScrollRect Chatroom;  // 지금 채팅을 출력중인 채팅방
    Action endMessage;  // 채팅 끝나면 실행할 함수
    [SerializeField] private SlideUpUI slideUPDown;

    private const string ImageSpritePath = "Sprites/NightPhone/";
    
    private bool isPlaying = false;

    private IEnumerator Start()
    {
        foreach(var sprite in profileSpriteName)
            profileImage.Add(sprite, Resources.Load<Sprite>(spritePath+sprite));

        BlackScreen.interactable = false;
        PhoneBase.interactable = false;
        Emoticon1.interactable = false;
        Emoticon2.interactable = false;
        Emoticon3.interactable = false;
        BlockScreen.SetActive(true);
        BackButton.SetActive(false);

        string meName = GameManager.Instance.data.name;
        
        yield return LocalizationSettings.InitializationOperation;

        // 3. 현재 설정된 언어 코드를 가져옵니다.
        string languageCode = LocalizationSettings.SelectedLocale.Identifier.Code;
        
        if (languageCode == "ko")
        {
            // 한국어일 때 처리
            LoadChatDataFromTSV("ChatData", meName); // 한국어 파일명으로 변경하세요
            Debug.Log("한국어 채팅 데이터를 로드합니다.");
        }
        else if (languageCode.StartsWith("en")) // en-US, en-GB 등 모두 포함
        {
            // 영어일 때 처리
            LoadChatDataFromTSV("ChatData_Eng", meName); // 영어 파일명으로 변경하세요
            Debug.Log("영어 채팅 데이터를 로드합니다.");
        }
        
        //StartChat("신아산_80");
        //slideUPDown.SlideUp();
        //StartChat();
    }

    [System.Serializable]
    public class chat // struct -> class
    {
        public bool me;
        public string text;
        public string image; 

        public chat(bool me, string text = null, string image = null)
        {
            this.me = me;
            this.text = text;
            this.image = image;
        }
    }

    [System.Serializable]
    public class Chatting // struct -> class
    {
        public string name;
        public List<chat> chatList;
        public bool chooseImoticon;
        public string[] nextChatting;

        public Chatting(string name, bool chooseImoticon, string[] nextChating = null)
        {
            this.name = name;
            this.chatList = new List<chat>();
            this.chooseImoticon = chooseImoticon;
            this.nextChatting = nextChating ?? new string[3];
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
        BackButton.SetActive(false);

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


        yield return new WaitForSeconds(1f);
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
        foreach(chat c in chatting.chatList)
        {
            Debug.Log("generate chat");
            GenerateChat(c);
            yield return new WaitForSeconds(1f);  // 채팅 생성 속도
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
            BackButton.SetActive(true);
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
        
        string imageKey = chattingTitle.Split('_')[0]; 

        // 💡 [수정됨] 딕셔너리에서 추출한 고유 키("신아산")로 프로필 이미지를 가져옵니다. 안전을 위해 예외 처리도 추가했습니다.
        if (profileImage.ContainsKey(imageKey))
        {
            proImage.sprite = profileImage[imageKey];
        }
        else
        {
            Debug.LogWarning($"[경고] '{imageKey}'에 해당하는 프로필 이미지가 profileImage 딕셔너리에 없습니다.");
        }
        
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

        Debug.Log(Chatroom.verticalNormalizedPosition);
        Chatroom.verticalNormalizedPosition = 0f;  // 스크롤 내리기
    }

    public void ChooseEmoticon(int index)
    {
        GameObject Chatbox;
        Chatbox = Instantiate(ChatBox_Me, Chatroom.content.transform);
        Debug.Log(Chatroom.content.transform);

        Chatbox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Emoticon[index];
        Instantiate(Emoticon[index], Chatroom.content.transform);

        StartCoroutine(UpdateScrollPosition());

        StartCoroutine(StartNextChat(index));
        PhoneBase.interactable = false;
        Emoticon1.interactable = false;
        Emoticon2.interactable = false;
        Emoticon3.interactable = false;
        BlockScreen.SetActive(true);
    }

    private IEnumerator UpdateScrollPosition()
    {
        yield return null; // 한 프레임 대기
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);
        yield return null; // 또 한 프레임 대기
        Chatroom.verticalNormalizedPosition = 0f;
    }

    public IEnumerator StartNextChat(int index)
    {
        yield return new WaitForSeconds(1f);
        StartChat(chatting.nextChatting[index]);
    }
    
    /// <summary>
    /// 엑셀 파일에서 채팅 내용 불러오기
    /// </summary>
    /// <param name="fileName"></param>
    private void LoadChatDataFromTSV(string fileName, string meName)
    {
        chattingDict.Clear();

        // Resources 폴더에서 TextAsset을 로드합니다.
        TextAsset tsvFile = Resources.Load<TextAsset>(fileName);
        if (tsvFile == null)
        {
            Debug.LogError($"채팅 데이터 파일을 찾을 수 없습니다: Resources/{fileName}");
            return;
        }

        // 엔터(\n)를 기준으로 줄(Row)을 나눕니다.
        string[] lines = tsvFile.text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        // 첫 번째 줄(index 0)은 헤더이므로 index 1부터 반복합니다.
        for (int i = 1; i < lines.Length; i++)
        {
            // 탭(\t)을 기준으로 열(Column)을 나눕니다.
            string[] row = lines[i].Split('\t');

            if (row.Length < 3) continue; // ID, Name, Text 최소 3개 열은 있어야 함

            string id = row[0].Trim();
            string name = row[1].Trim();
        
            // 텍스트나 이미지가 비어있으면 null로 처리
            string text = string.IsNullOrWhiteSpace(row[2]) ? null : row[2].Replace("\\n", "\n").Replace("{meName}", meName); 
            string image = (row.Length > 3 && !string.IsNullOrWhiteSpace(row[3])) ? row[3] : null;

            // 딕셔너리에 이 ID의 채팅방이 없다면 새로 생성해줍니다.
            if (!chattingDict.ContainsKey(id))
            {
                chattingDict[id] = new Chatting(name, false);
            }

            // 현재 대사 줄을 리스트에 추가합니다 (상대방 대사이므로 me = false)
            Chatting currentChat = chattingDict[id];
            currentChat.chatList.Add(new chat(false, text, image));

            // 이모티콘 선택 및 분기 데이터가 있다면 세팅합니다.
            if (row.Length > 4 && bool.TryParse(row[4], out bool chooseEmoticon) && chooseEmoticon)
            {
                currentChat.chooseImoticon = true;
                if (row.Length > 7)
                {
                    currentChat.nextChatting = new string[] { row[5], row[6], row[7] };
                }
            }
        }

        Debug.Log("채팅 데이터 파싱 완료! 총 채팅방 개수: " + chattingDict.Count);
    }
}

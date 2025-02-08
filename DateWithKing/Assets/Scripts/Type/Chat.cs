using Newtonsoft.Json;

namespace Type
{
    public class Chat
    {
        private static readonly int[] messagePoint = { 20, 40, 50, 60, 70 };
        //전에 확인한 문자 인덱스
        [JsonProperty] private int beforeChat = -1;

        /// <summary>
        /// 채팅이 오는지 여부 확인 <br/>
        /// 채팅이 오지 않을 시 0 반환, 올 시 호감도 반환
        /// </summary>
        public int CheckChat(int lv)
        {
            int currentChat = beforeChat + 1;
            if (messagePoint.Length <= currentChat) return 0;
            if (messagePoint[currentChat] > lv) return 0;

            beforeChat = currentChat;
            
            return messagePoint[currentChat];
        }
    }
}
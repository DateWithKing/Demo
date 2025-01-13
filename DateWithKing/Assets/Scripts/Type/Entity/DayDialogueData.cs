
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class DayDialogueData : Entity
    {
        /// <summary>
        /// key - 요일_교시_장소
        /// value - 해당 요일-교시-장소에 고정적으로 등장하는 캐릭터 이름
        /// </summary>
        [JsonProperty]
        public Dictionary<string, Character> spotCharacters { get; private set; }
    }

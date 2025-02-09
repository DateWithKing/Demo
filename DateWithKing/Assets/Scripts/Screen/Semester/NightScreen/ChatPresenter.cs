using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 채팅 오는 권한 담당
/// </summary>
public class ChatPresenter : MonoBehaviour
{
    public void TextingAtFirst()
    {
        if (SemesterSceneData.Instance.clock.GetCurrentWeekCycle() != WeekCycle.Night) return;

        var todayChat = new Dictionary<Character, int>();

        int san = GameManager.Instance.data.chatting[Character.신아산].CheckChat(
            GameManager.Instance.data.stats["lvSan"].value);
        int hyun = GameManager.Instance.data.chatting[Character.양나현].CheckChat(
            GameManager.Instance.data.stats["lvHyun"].value);
        int pyo = GameManager.Instance.data.chatting[Character.서은표].CheckChat(
            GameManager.Instance.data.stats["lvPyo"].value);

        if (san > 0) todayChat.Add(Character.신아산, san);
        if (hyun > 0) todayChat.Add(Character.양나현, hyun);
        if (pyo > 0) todayChat.Add(Character.서은표, pyo);

        var chatList = todayChat.OrderByDescending(pair => pair.Value).ToList();

        StartNextChat(chatList, 0);
    }
    
    private void StartNextChat(List<KeyValuePair<Character, int>> chatList, int index)
    {
        if (index >= chatList.Count) return;
        Debug.Log("채팅이 출력되었습니다.");
        ChatManager.Instance.StartChat($"{chatList[index].Key.ToString()}_{chatList[index].Value.ToString()}",
            () => StartNextChat(chatList, index + 1));
    }

}

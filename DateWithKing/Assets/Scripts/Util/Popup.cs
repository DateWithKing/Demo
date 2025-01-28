
using UnityEngine;

public class Popup : Singleton<Popup>
{
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextBubble bubble;
    
    public void PopPanel(string text)
    {
        bubble.SetText(text);
        popupPanel.SetActive(true);
    }
}

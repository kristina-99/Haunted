using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ChatManager : MonoBehaviour
{
    public Transform messagePanel;
    public TMP_Text messageTemplate;
    private const int MaxMessages = 3; 

    void OnEnable()
    {
        GameEvents.OnMessageReceived += DisplayMessage;
    }

    void OnDisable()
    {
        GameEvents.OnMessageReceived -= DisplayMessage;
    }

    private void DisplayMessage(ChatMessage message)
    {
        if (messageTemplate == null) return;
        RemoveOldMessages();

        TMP_Text newTextComponent = Instantiate(messageTemplate, messagePanel);
        
        newTextComponent.gameObject.SetActive(true);
        
        newTextComponent.text = $"<b>{message.SenderName}:</b> {message.MessageText}";
    }
    
    private void RemoveOldMessages()
    {
        while (messagePanel.childCount >= MaxMessages)
        {
            Transform oldestMessage = messagePanel.GetChild(0);
            
            Destroy(oldestMessage.gameObject);
            
            oldestMessage.SetParent(null);
        }
    }
}

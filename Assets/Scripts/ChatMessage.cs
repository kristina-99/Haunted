public class ChatMessage
{
    public string SenderName { get; private set; }
    public string MessageText { get; private set; }

    public ChatMessage(string senderName, string messageText)
    {
        SenderName = senderName;
        MessageText = messageText;
    }
}

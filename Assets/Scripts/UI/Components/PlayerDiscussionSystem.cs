using UnityEngine;
using UnityEngine.UI;

public class PlayerDiscussionSystem : MonoBehaviour
{
    public Button blameButton;
    public Button defendButton;
    public GameObject[] characterButtonObjects;
    private string saveKey = "SelectedCharacter";
    private string targetName;
    private BaseCharacter player;

    void OnEnable()
    {
        GameEvents.OnArcadeMapLoaded += FindPlayer;
    }

    void OnDisable()
    {
        GameEvents.OnArcadeMapLoaded -= FindPlayer;
    }
    void Start()
    {
        SetCharacterButtons();

        blameButton.onClick.AddListener(() => OnBlameButtonClick());
        defendButton.onClick.AddListener(() => OnDefendButtonClick());
    }

    private void FindPlayer()
    {
        player = Extensions.GetAlivePlayers().Find(player => player is PlayerController);
    }

    private void SetCharacterButtons()
    {
        foreach(GameObject characterButton in characterButtonObjects)
        {
            Button button = characterButton.GetComponent<Button>();

            if (button != null)
            {
                string buttonName = characterButton.name;

                button.onClick.AddListener(() => OnCharacterButtonClick(buttonName));
            }
        }

    }
    
    public void OnCharacterButtonClick(string selectedButttonName)
    {
        PlayerPrefs.SetString(saveKey, selectedButttonName);
        PlayerPrefs.Save();
        targetName = PlayerPrefs.GetString(saveKey);
    }

    public void OnBlameButtonClick()
    {
        string blameMessage = $"I think {targetName} is acting incredibly suspicious right now";
        SendMessage(player.gameObject.name,blameMessage);
    }

    public void OnDefendButtonClick()
    {
        string defendMessage = $"Leave {targetName} alone, I'm certain they're innocent";
        SendMessage(player.gameObject.name,defendMessage);
    }

    private void SendMessage(string name, string message)
    {
        ChatMessage chatMessage = new ChatMessage(name,message);
        GameEvents.MessageReceived(chatMessage);
    }
}

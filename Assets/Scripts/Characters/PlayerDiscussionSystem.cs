using UnityEngine;
using UnityEngine.UI;

public class PlayerDiscussionSystem : MonoBehaviour
{
    private GameObject[] characterButtonObjects;
    private string saveKey = "SelectedCharacter";

    void Start()
    {
        characterButtonObjects = GameObject.FindGameObjectsWithTag("CharacterButton");

        foreach(GameObject characterButton in characterButtonObjects)
        {
            Button button = characterButton.GetComponent<Button>();

            if(button != null)
            {
                string buttonName = characterButton.name;

                button.onClick.AddListener(() => OnCharacterButtonClick(buttonName));
            }
        }

        Button blameButton = GameObject.FindGameObjectWithTag("BlameButton").GetComponent<Button>();
        blameButton.onClick.AddListener(() => OnBlameButtonClick());

        Button defendButton = GameObject.FindGameObjectWithTag("DefendButton").GetComponent<Button>();
        defendButton.onClick.AddListener(() => OnDefendButtonClick());
    }

    public void OnCharacterButtonClick(string selectedButttonName)
    {
        PlayerPrefs.SetString(saveKey, selectedButttonName);
        PlayerPrefs.Save();
    }

    public void OnBlameButtonClick()
    {
        //get selected character
        string targetName = PlayerPrefs.GetString(saveKey);

        //create message/string "Player blames Bot..."
        string blameMessage = $"I think {targetName} is acting incredibly suspicious right now";

        //create a ChatMessageObject with player as sender and the above message
        ChatMessage chatMessage = new ChatMessage(gameObject.name, blameMessage);
        GameEvents.MessageReceived(chatMessage);
    }

    public void OnDefendButtonClick()
    {
        //get selected character
        string targetName = PlayerPrefs.GetString(saveKey);

        //create message/string "Player blames Bot..."
        string defendMessage = $"Leave {targetName} alone, I'm certain they're innocent";

        //create a ChatMessageObject with player as sender and the above message
        ChatMessage chatMessage = new ChatMessage(gameObject.name, defendMessage);
        GameEvents.MessageReceived(chatMessage);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PlayerDiscussionSystem : MonoBehaviour
{
    private GameObject[] characterButtonObjects;
    private string saveKey = "SelectedCharacter";
    private string targetName;

    void Start()
    {
        characterButtonObjects = GameObject.FindGameObjectsWithTag("CharacterButton");

        SetCharacterButtons();

        Button blameButton = GameObject.FindGameObjectWithTag("BlameButton").GetComponent<Button>();
        blameButton.onClick.AddListener(() => OnBlameButtonClick());

        Button defendButton = GameObject.FindGameObjectWithTag("DefendButton").GetComponent<Button>();
        defendButton.onClick.AddListener(() => OnDefendButtonClick());
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
        SendMessage(gameObject.name,blameMessage);
    }

    public void OnDefendButtonClick()
    {
        string defendMessage = $"Leave {targetName} alone, I'm certain they're innocent";
        SendMessage(gameObject.name,defendMessage);
    }

    private void SendMessage(string name, string message)
    {
        ChatMessage chatMessage = new ChatMessage(name,message);
        GameEvents.MessageReceived(chatMessage);
    }
}

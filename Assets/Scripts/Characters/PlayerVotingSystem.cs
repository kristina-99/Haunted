using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVotingSystem : MonoBehaviour
{
    private GameObject[] characterButtonObjects;
    private string saveKey = "SelectedCharacter";

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
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

        Button voteButton = GameObject.FindGameObjectWithTag("VoteButton").GetComponent<Button>();
        voteButton.onClick.AddListener(() => OnVoteButtonClick());
    }

    public void OnCharacterButtonClick(string selectedButttonName)
    {
        PlayerPrefs.SetString(saveKey, selectedButttonName);
        PlayerPrefs.Save();
    }

    public void OnVoteButtonClick()
    {
        string characterName = PlayerPrefs.GetString(saveKey);
        List<BaseCharacter> alivePlayers = GameManager.Instance.gameStateModel.GetAlivePlayers();

        BaseCharacter target = alivePlayers.Find(p => p.gameObject.name == characterName);

        if(target ==null)
        {
            Debug.Log("Target is null");
        }

        if (target != null)
        {
            Debug.Log("Target is not null");
            playerController.OnVoteCast(target);
        }
        else
        {
            Debug.Log($"Could not find an alive BaseCharacter component named {characterName}!");
        }
    }
}

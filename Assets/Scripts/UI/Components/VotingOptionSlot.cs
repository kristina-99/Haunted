using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VotingOptionSlot : MonoBehaviour
{
    private BaseCharacter associatedCharacter; 
    
    [SerializeField] private TMPro.TextMeshProUGUI voteCountText; 

    public BaseCharacter AssociatedCharacter => associatedCharacter;

    void OnEnable()
    {
        GameEvents.OnArcadeMapLoaded += AttachCharacter;
        GameEvents.OnVotingFinished += RouteVotingFinished;
        GameEvents.OnNightStarted += ClearDisplay;
        GameEvents.OnDayStarted += DisableCharacterButton;
    }

    void OnDisable()
    {
        GameEvents.OnArcadeMapLoaded -= AttachCharacter;
        GameEvents.OnVotingFinished -= RouteVotingFinished;
        GameEvents.OnNightStarted -= ClearDisplay;
        GameEvents.OnDayStarted -= DisableCharacterButton;
    }

    private void RouteVotingFinished(BaseCharacter votedOut, bool isTie)
    => UpdateVoteDisplay();

    public void UpdateVoteDisplay()
    {
        int votesCount = GameManager.Instance.gameStateModel.GetCharacterVotes(associatedCharacter);
        voteCountText.text = $"Votes: {votesCount}";
    }

    private void ClearDisplay(int round)
    {
        StartCoroutine(ClearDisplayAfterDelay());
    }

    private IEnumerator ClearDisplayAfterDelay()
    {
        yield return new WaitForSeconds(4f);
        voteCountText.text = "Votes: 0";
    }

    private void AttachCharacter()
    {
        List<BaseCharacter> alivePlayers = GameManager.Instance.gameStateModel.GetAlivePlayers();
        foreach(BaseCharacter character in alivePlayers)
        {
            if(character.gameObject.name == this.gameObject.name)
            {
                associatedCharacter = character;
            }
        }
    }

    private void DisableCharacterButton()
    {
        if(associatedCharacter == null)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
    }
}

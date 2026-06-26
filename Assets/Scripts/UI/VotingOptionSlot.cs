using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VotingOptionSlot : MonoBehaviour
{
    private BaseCharacter associatedCharacter; 
    
    [SerializeField] private TMPro.TextMeshProUGUI voteCountText; 

    public BaseCharacter AssociatedCharacter => associatedCharacter;

    void OnEnable()
    {
        GameEvents.OnArcadeMapLoaded += AttachCharacter;
        GameEvents.OnVotingFinished += UpdateVoteDisplay;
        GameEvents.OnNightStarted += ClearDisplay;
    }

    void OnDisable()
    {
        GameEvents.OnArcadeMapLoaded -= AttachCharacter;
        GameEvents.OnVotingFinished -= UpdateVoteDisplay;
        GameEvents.OnNightStarted -= ClearDisplay;
    }

    public void UpdateVoteDisplay()
    {
        int votesCount = GameManager.Instance.gameStateModel.GetCharacterVotes(associatedCharacter);
        voteCountText.text = $"Votes: {votesCount}";
    }

    public void ClearDisplay(int round)
    {
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
}

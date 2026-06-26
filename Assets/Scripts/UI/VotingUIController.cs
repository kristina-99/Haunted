using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VotingUIController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<TextMeshPro> votingResultTextBoxes;
    //vzimame text boxes
    //vzimame voting result
    //za vseki voting result go slagame v podhodqshtata textbox kutiika

    void OnEnable()
    {
        GameEvents.OnVotingFinished += DisplayResults;
    }

    void OnDisable()
    {
        GameEvents.OnVotingFinished += DisplayResults;
    }

    private void DisplayResults()
    {
        // foreach()
        // GameManager.Instance.gameStateModel.GetCharacterVotes()
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VotingUIController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CanvasGroup votingCanvas;
    private List<TextMeshPro> votingResultTextBoxes;
    //vzimame text boxes
    //vzimame voting result
    //za vseki voting result go slagame v podhodqshtata textbox kutiika

    void Start()
    {
        votingCanvas.alpha = 0f;
        votingCanvas.interactable = false;
        votingCanvas.blocksRaycasts = false;
    }

    void OnEnable()
    {
        GameEvents.OnNightStarted += HideVotingPanel;
        GameEvents.OnDayStarted += DisplayVotingPanel;
    }

    void OnDisable()
    {
        GameEvents.OnNightStarted += HideVotingPanel;
        GameEvents.OnDayStarted -= DisplayVotingPanel;
    }

    // private void DisplayResults()
    // {
    //     // foreach()
    //     // GameManager.Instance.gameStateModel.GetCharacterVotes()
    // }

    private void HideVotingPanel(int round)
    {
        votingCanvas.interactable = false;
        votingCanvas.blocksRaycasts = false;

        votingCanvas.DOFade(0f, 1f);
    }

    private void DisplayVotingPanel()
    {
        votingCanvas.interactable = true;
        votingCanvas.blocksRaycasts = true;
        votingCanvas.DOFade(1f, 0.5f).SetEase(Ease.OutQuad);
    }
}

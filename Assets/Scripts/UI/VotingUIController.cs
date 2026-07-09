using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VotingUIController : MonoBehaviour
{
    public CanvasGroup votingCanvas;
    private List<TextMeshPro> votingResultTextBoxes;

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

    private void HideVotingPanel(int round)
    {
        votingCanvas.interactable = false;
        votingCanvas.blocksRaycasts = false;
        votingCanvas.alpha = 1f;

        Sequence hideSequence = DOTween.Sequence();
        hideSequence.AppendInterval(2f);
        hideSequence.Append(votingCanvas.DOFade(0f, 2f));
    }

    private void DisplayVotingPanel()
    {
        votingCanvas.interactable = true;
        votingCanvas.blocksRaycasts = true;
        votingCanvas.DOFade(1f, 0.5f).SetEase(Ease.OutQuad);
    }
}

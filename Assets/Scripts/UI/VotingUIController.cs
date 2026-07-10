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
        GameEvents.OnVotingFinished += HideVotingPanel;
        GameEvents.OnDayStarted += DisplayVotingPanel;
    }

    void OnDisable()
    {
        GameEvents.OnVotingFinished -= HideVotingPanel;
        GameEvents.OnDayStarted -= DisplayVotingPanel;

        votingCanvas.DOKill();
    }

    private void HideVotingPanel(BaseCharacter target, bool isTie)
    {
        votingCanvas.interactable = false;
        votingCanvas.blocksRaycasts = false;
        votingCanvas.alpha = 1f;

        DOTween.Sequence()
            .AppendInterval(2f)
            .Append(votingCanvas.DOFade(0f, 2f))
            .SetLink(gameObject);
    }

    private void DisplayVotingPanel()
    {
        votingCanvas.DOKill();

        votingCanvas.interactable = true;
        votingCanvas.blocksRaycasts = true;
        votingCanvas.DOFade(1f, 0.5f).SetEase(Ease.OutQuad);
    }
}

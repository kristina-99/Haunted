using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class VotingPanel : UIPanel
{

    private void Start()
    {
        ResetUI();
    }

    private void OnEnable()
    {
        GameEvents.OnVotingFinished += HandleVotingFinished;
        GameEvents.OnDayStarted += DisplayVotingPanel;
    }

    private void OnDisable()
    {
        GameEvents.OnVotingFinished -= HandleVotingFinished;
        GameEvents.OnDayStarted -= DisplayVotingPanel;
    }

    private void DisplayVotingPanel()
    {
        Show(0.5f).SetEase(Ease.OutQuad);
    }

    private void HandleVotingFinished(BaseCharacter target, bool isTie)
    {
        Hide(2f).SetDelay(2f);
    }
}

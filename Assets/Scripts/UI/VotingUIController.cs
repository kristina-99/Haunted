using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class VotingUIController : UIPanel
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
        KillActiveTransition();
    }

    private void DisplayVotingPanel()
    {
        Show(0.5f, Ease.OutQuad);
    }

    private void HandleVotingFinished(BaseCharacter target, bool isTie)
    {
        DOTween.Sequence()
            .AppendInterval(2f)
            .AppendCallback(() => Hide(2f))
            .SetLink(gameObject);
    }
}

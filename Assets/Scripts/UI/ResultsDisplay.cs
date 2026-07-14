using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;
using static GameConstants;

public class ResultsDisplay : UIPanel
{
    public Image background;
    public TMP_Text votedOutCharacter;
    public TMP_Text isHauntedReveal;
    private Sequence _animationSequence;

    private void Start()
    {
        ResetUI();
    }

    private void OnEnable()
    {
        GameEvents.OnVotingFinished += HandleResults; 
    }

    private void OnDisable()
    {
        GameEvents.OnVotingFinished -= HandleResults;
        KillActiveTweens();
    }

    public override void ResetUI()
    {
        base.ResetUI(); // Resets canvasGroup smoothly

        // Handle the nested elements
        SetAlpha(background, 0f);
        SetAlpha(votedOutCharacter, 0f);
        SetAlpha(isHauntedReveal, 0f);
    }

    private void HandleResults(BaseCharacter votedOut, bool isTie)
    {
        if (isTie || votedOut == null)
        {
            votedOutCharacter.text = "No one was voted out (Tie vote).";
            isHauntedReveal.text = string.Empty;
        }
        else
        {
            votedOutCharacter.text = $"{votedOut.name} was voted out.";
            isHauntedReveal.text = votedOut.Role == CharacterRole.Haunted 
                ? "They were the Haunted!" 
                : "They were NOT the Haunted.";
        }

        StartCoroutine(RevealResultsRoutine());
    }

    private IEnumerator RevealResultsRoutine()
    {
        KillActiveTweens();
        ResetUI();
        
        canvasGroup.alpha = 1f; // Make parent group visible

        _animationSequence = DOTween.Sequence();
        _animationSequence
            .AppendInterval(3.0f)
            .Append(background.DOFade(1f, 2.0f).SetEase(Ease.InOutQuad))
            .Append(votedOutCharacter.DOFade(1f, 1.5f))
            .AppendInterval(0.5f)
            .Append(isHauntedReveal.DOFade(1f, 1.5f))
            .AppendInterval(2.0f);

        yield return _animationSequence.WaitForCompletion();

        // Let the base class handle the smooth fading out of the parent CanvasGroup!
        Hide(3.0f, Ease.Linear, ResetUI);
    }

    private void KillActiveTweens()
    {
        KillActiveTransition(); // Clears base class tweens

        if (_animationSequence != null && _animationSequence.IsActive())
        {
            _animationSequence.Kill();
        }
        
        background.DOKill();
        votedOutCharacter.DOKill();
        isHauntedReveal.DOKill();
    }

    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}
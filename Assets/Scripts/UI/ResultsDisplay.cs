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
    

    private void OnEnable() 
    {
        GameEvents.OnVotingFinished += HandleResults;
    }

    private void OnDisable()
    {
        GameEvents.OnVotingFinished -= HandleResults;
    }

    public override void ResetUI()
    {
        base.ResetUI();

        background.DOKill();
        votedOutCharacter.DOKill();
        isHauntedReveal.DOKill();

        background.color = SetAlpha(background.color, 0f);
        votedOutCharacter.color = SetAlpha(votedOutCharacter.color, 0f);
        isHauntedReveal.color = SetAlpha(isHauntedReveal.color, 0f);
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
        // Resets the elements and kills any lingering tweens from previous rounds
        ResetUI();
        
        // Show the panel's canvas immediately so nested tweens are visible
        gameObject.SetActive(true);
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        // Build and play the nested reveal sequence
        _animationSequence = DOTween.Sequence()
            .AppendInterval(3.0f)
            .Append(background.DOFade(1f, 2.0f).SetEase(Ease.InOutQuad))
            .Append(votedOutCharacter.DOFade(1f, 1.5f))
            .AppendInterval(0.5f)
            .Append(isHauntedReveal.DOFade(1f, 1.5f))
            .AppendInterval(2.0f);

        yield return _animationSequence.WaitForCompletion();

        yield return Hide(3.0f).SetEase(Ease.Linear).WaitForCompletion();
    }

    private void OnDestroy()
    {
        _animationSequence?.Kill();
    }

    private Color SetAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}
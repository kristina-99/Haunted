using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using static GameConstants;
using System.Collections;

public class ResultsDisplay : MonoBehaviour
{
    public CanvasGroup resultsCanvas;
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

    private void ResetUI()
    {
        resultsCanvas.alpha = 0f;
        resultsCanvas.blocksRaycasts = false;
        resultsCanvas.interactable = false;

        SetAlpha(background, 0f);
        SetAlpha(votedOutCharacter, 0f);
        SetAlpha(isHauntedReveal, 0f);
    }

    private void HandleResults(BaseCharacter votedOut, bool isTie)
    {
        // 1. Set text values
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

        // 2. Play the display cycle
        StartCoroutine(RevealResultsRoutine());
    }

    private IEnumerator RevealResultsRoutine()
    {
        // Ensure clean state before starting new animations
        KillActiveTweens();
        ResetUI();
        
        resultsCanvas.alpha = 1f;

        // Construct the timeline using DOTween sequences instead of messy while loops
        _animationSequence = DOTween.Sequence();
        
        _animationSequence
            .AppendInterval(3.0f) // Initial delay before reveal starts
            .Append(background.DOFade(1f, 2.0f).SetEase(Ease.InOutQuad))
            .Append(votedOutCharacter.DOFade(1f, 1.5f))
            .AppendInterval(0.5f)
            .Append(isHauntedReveal.DOFade(1f, 1.5f))
            .AppendInterval(2.0f); // Screen stay duration

        // Wait for the entire sequence to naturally finish playing
        yield return _animationSequence.WaitForCompletion();

        HideScreen();
    }

    private void HideScreen()
    {
        resultsCanvas.interactable = false;
        resultsCanvas.blocksRaycasts = false;

        // Fade out everything smoothly, then safely reset back to zero
        _animationSequence = DOTween.Sequence();
        _animationSequence.Append(resultsCanvas.DOFade(0f, 3.0f))
                          .OnComplete(ResetUI);
    }

    private void KillActiveTweens()
    {
        if (_animationSequence != null && _animationSequence.IsActive())
        {
            _animationSequence.Kill();
        }
        
        // Safety catch-all for individual elements
        background.DOKill();
        votedOutCharacter.DOKill();
        isHauntedReveal.DOKill();
        resultsCanvas.DOKill();
    }

    #region Helper Methods
    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
    #endregion
}
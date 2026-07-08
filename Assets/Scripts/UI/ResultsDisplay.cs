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

    void Start()
    {
        resultsCanvas.blocksRaycasts = false;
        Color backgroundColor = background.color;
        backgroundColor.a = 0f;
        background.color = backgroundColor;

        votedOutCharacter.color = new Color(votedOutCharacter.color.r, votedOutCharacter.color.g, votedOutCharacter.color.b, 0f);
        isHauntedReveal.color = new Color(isHauntedReveal.color.r, isHauntedReveal.color.g, isHauntedReveal.color.b, 0f);
    }

    void OnEnable()
    {
        GameEvents.OnVotingFinished += HandleResults; 
    }

    void OnDisable()
    {
        GameEvents.OnVotingFinished -= HandleResults;
    }

    IEnumerator RevealResults()
    {
        resultsCanvas.blocksRaycasts = false;
        float waitTime = Time.time + 3f;
        while (Time.time < waitTime)
        {
            yield return null; 
        }

        Sequence animationSequence = DOTween.Sequence();
        
        animationSequence.Append(background.DOFade(1f, 2.0f).SetEase(Ease.InOutQuad))
                         .Append(votedOutCharacter.DOFade(1f,1.5f))
                         .AppendInterval(0.5f)
                         .Append(isHauntedReveal.DOFade(1f,1.5f));
    }

    // The event now hands the UI exactly what it needs to display
    private void HandleResults(BaseCharacter votedOut, bool isTie)
    {
        if (isTie || votedOut == null)
        {
            votedOutCharacter.text = "No one was voted out (Tie vote).";
            isHauntedReveal.text = ""; // Keeps the second text blank
        }
        else
        {
            votedOutCharacter.text = $"{votedOut.name} was voted out.";

            // Check the role of the passed character directly
            if (votedOut.Role == CharacterRole.Haunted)
            {
                isHauntedReveal.text = "They were the Haunted!";
            }
            else
            {
                isHauntedReveal.text = "They were NOT the Haunted.";
            }
        }

        StartCoroutine(RevealResults());
    }
}
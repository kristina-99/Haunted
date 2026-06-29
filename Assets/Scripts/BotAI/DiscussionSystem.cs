using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Extensions;

public class DiscussionSystem : MonoBehaviour
{
    private List<BaseCharacter> aliveCharacters;

    [Header("Timing Settings")]
    private const float MinWaitTime = 2.0f; 
    private const float MaxWaitTime = 7.0f;

    // void Start()
    // {
    //     aliveCharacters = GetAlivePlayers();
    // }

    private void OnEnable()
    {
        GameEvents.OnDayStarted += StartBotDiscussion;
        GameEvents.OnVotingFinished += StopBotDiscussion;
    }

    private void OnDisable()
    {
        GameEvents.OnDayStarted -= StartBotDiscussion;
        GameEvents.OnVotingFinished -= StopBotDiscussion;

    }

    private void StartBotDiscussion()
    {
        aliveCharacters = GetAlivePlayers();
        StartCoroutine(DelayedBotMessageRoutine());
    }

    private void StopBotDiscussion()
    {
        if(DelayedBotMessageRoutine() != null)
        {
            StopCoroutine(DelayedBotMessageRoutine());
        }
    }

    private IEnumerator DelayedBotMessageRoutine()
    {
        while(true)
        {
            float randomDelay = Random.Range(MinWaitTime, MaxWaitTime);
        
            yield return new WaitForSeconds(randomDelay);

            GenerateBotMessage();
        }
    }

    private void GenerateBotMessage()
    {
        int randomSenderIndex = Random.Range(0, aliveCharacters.Count);
        while(aliveCharacters[randomSenderIndex] is PlayerController)
        {
            randomSenderIndex = Random.Range(0, aliveCharacters.Count);
        }
        BaseCharacter botSender = aliveCharacters[randomSenderIndex];

        List<BaseCharacter> potentialTargets = new List<BaseCharacter>(aliveCharacters);
        potentialTargets.Remove(botSender);

        int randomTargetIndex = Random.Range(0, potentialTargets.Count);
        BaseCharacter targetCharacter = potentialTargets[randomTargetIndex];

        int randomMessageIndex = Random.Range(0,2);

        string message;

        if(randomMessageIndex == 0)
        {
            message = $"Leave {targetCharacter} alone, I'm certain they're innocent";
        }
        else
        {
            message = $"I think {targetCharacter} is acting incredibly suspicious right now";
        }

        ChatMessage chatMessage = new ChatMessage(botSender.name, message);
        GameEvents.MessageReceived(chatMessage);
    }

}

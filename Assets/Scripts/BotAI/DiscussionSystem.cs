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
    private const int MessageOptions = 2;
    private const int MinAliveCharactersCount = 2;
    private Coroutine discussionCoroutine;

    private void OnEnable()
    {
        GameEvents.OnDayStarted += StartBotDiscussion;
        GameEvents.OnVotingFinished += RouteVotingFinished;
    }

    private void OnDisable()
    {
        GameEvents.OnDayStarted -= StartBotDiscussion;
        GameEvents.OnVotingFinished -= RouteVotingFinished;
    }

    private void RouteVotingFinished(BaseCharacter votedOut, bool isTie)
    => StopBotDiscussion();

    private void StartBotDiscussion()
    {
        aliveCharacters = GetAlivePlayers();
        StopBotDiscussion(); 
        
        discussionCoroutine = StartCoroutine(DelayedBotMessageRoutine());
    }

    private void StopBotDiscussion()
    {
        if (discussionCoroutine != null)
        {
            StopCoroutine(discussionCoroutine);
            discussionCoroutine = null;
        }
    }

    private IEnumerator DelayedBotMessageRoutine()
    {
        while (true)
        {
            float randomDelay = Random.Range(MinWaitTime, MaxWaitTime);
            yield return new WaitForSeconds(randomDelay);

            GenerateBotMessage();
        }
    }

    private void GenerateBotMessage()
    {
        if (aliveCharacters == null || aliveCharacters.Count < MinAliveCharactersCount) 
        {
            return;
        }

        BaseCharacter player = aliveCharacters.FindLast(x => x is PlayerController);

        BaseCharacter botSender = PickRandomCharacter(player);
        if (botSender == null)
        {
            return;
        }

        BaseCharacter targetCharacter = PickRandomCharacter(botSender);
        if (targetCharacter == null) 
        {
            return;
        }

        int randomMessageIndex = Random.Range(0, MessageOptions);
        string message = randomMessageIndex == 0
        ? $"Leave {targetCharacter.name} alone, I'm certain they're innocent"
        : $"I think {targetCharacter.name} is acting incredibly suspicious right now";

        ChatMessage chatMessage = new ChatMessage(botSender.name, message);
        GameEvents.MessageReceived(chatMessage);
    }

    private BaseCharacter PickRandomCharacter(BaseCharacter characterToExclude)
    {
        int count = aliveCharacters.Count;
        if (count == 0)
        {
            return null;
        }

        int startIndex = Random.Range(0, count);

        for (int i = 0; i < count; i++)
        {
            BaseCharacter candidate = aliveCharacters[(startIndex + i) % count];
            if (candidate != characterToExclude)
            {
                return candidate;
            }
        }
        
        return null;
    }
}
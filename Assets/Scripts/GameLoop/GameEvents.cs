using System;
using static GameConstants;

public static class GameEvents
{
// Raised with the victim as payload
public static event Action<BaseCharacter> OnPlayerKilled;
public static event Action<int> OnNightStarted; // round number
public static event Action OnDayStarted;
public static event Action OnTransitionStarted;
public static event Action<BaseCharacter> OnTaskCompleted; // completer
public static event Action<BaseCharacter, BaseCharacter> OnVoteCast; // voter,target
public static event Action OnGameStarted;
public static event Action OnStartScenesFinished;
public static event Action<GameResult> OnGameEnded;
public static event Action<BaseCharacter> OnBodyReported;
public static event Action<BaseCharacter, AbilityType> OnAbilityUsed;
public static event Action OnHauntedStunned;
public static event Action OnArcadeMapLoaded;
public static event Action<BaseCharacter,bool> OnVotingFinished;
public static event Action<ChatMessage> OnMessageReceived;
// Raise helpers — null-check built in
public static void PlayerKilled(BaseCharacter v) =>
OnPlayerKilled?.Invoke(v);
public static void NightStarted(int round) =>
OnNightStarted?.Invoke(round);
public static void DayStarted() =>
OnDayStarted?.Invoke();
public static void TransitionStarted() =>
OnTransitionStarted?.Invoke();
public static void TaskCompleted(BaseCharacter c) =>
GameEvents.OnTaskCompleted?.Invoke(c);
public static void VoteCast(BaseCharacter voter, BaseCharacter target) =>
OnVoteCast?.Invoke(voter, target);
public static void GameStarted() =>
OnGameStarted.Invoke();
public static void StartScenesFinished() =>
OnStartScenesFinished.Invoke();
public static void GameEnded(GameResult r) =>
OnGameEnded?.Invoke(r);
public static void BodyReported(BaseCharacter reporter) =>
OnBodyReported?.Invoke(reporter);
public static void AbilityUsed(BaseCharacter u, AbilityType t) =>
OnAbilityUsed?.Invoke(u,t);
public static void HauntedStunned() =>
OnHauntedStunned?.Invoke();
public static void ArcadeMapLoaded()=>
OnArcadeMapLoaded?.Invoke();
public static void VotingFinished(BaseCharacter votedOut, bool isTie)=>
OnVotingFinished?.Invoke(votedOut,isTie);
public static void MessageReceived(ChatMessage message)=>
OnMessageReceived.Invoke(message);
}
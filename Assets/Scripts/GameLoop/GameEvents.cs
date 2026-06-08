using System;
using static GameConstants;

public static class GameEvents
{
// Raised with the victim as payload
public static event Action<BaseCharacter> OnPlayerKilled;
public static event Action<int> OnNightStarted; // round number
public static event Action OnDayStarted;
public static event Action<BaseCharacter> OnTaskCompleted; // completer
public static event Action<BaseCharacter, BaseCharacter> OnVoteCast; // voter,target
public static event Action<GameResult> OnGameEnded;
public static event Action<BaseCharacter> OnBodyReported;
public static event Action<BaseCharacter, AbilityType> OnAbilityUsed;
// Raise helpers — null-check built in
public static void PlayerKilled(BaseCharacter v) =>
OnPlayerKilled?.Invoke(v);
public static void NightStarted(int round) =>
OnNightStarted?.Invoke(round);
public static void DayStarted() =>
OnDayStarted?.Invoke();
public static void TaskCompleted(BaseCharacter c) =>
GameEvents.OnTaskCompleted?.Invoke(c);
public static void VoteCast(BaseCharacter voter, BaseCharacter target) =>
OnVoteCast?.Invoke(voter, target);
public static void GameEnded(GameResult r) =>
OnGameEnded?.Invoke(r);
public static void BodyReported(BaseCharacter reporter) =>
OnBodyReported?.Invoke(reporter);
public static void AbilityUsed(BaseCharacter u, AbilityType t) =>
OnAbilityUsed?.Invoke(u,t);
}
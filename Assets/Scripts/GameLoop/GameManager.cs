using UnityEngine;
using static GameConstants;

public class GameManager : MonoBehaviour
{
    private GameStateModel stateModel;

    private void Awake()
    {
        stateModel = new GameStateModel();
    }
    private void OnEnable()
    {
        GameEvents.OnNightStarted += RouteNightStart;
        GameEvents.OnDayStarted += RouteDayStart;
        GameEvents.OnBodyReported += RouteBodyReported;
        GameEvents.OnGameEnded += RouteGameEnded;
        GameEvents.OnPlayerKilled += RegisterKill;
        GameEvents.OnVoteCast += RegisterVote;
        GameEvents.OnTaskCompleted += CompleteTask;
    }

    private void OnDisable()
    {
        GameEvents.OnNightStarted -= RouteNightStart;
        GameEvents.OnDayStarted -= RouteDayStart;
        GameEvents.OnBodyReported -= RouteBodyReported;
        GameEvents.OnGameEnded -= RouteGameEnded;
        GameEvents.OnPlayerKilled -= RegisterKill;
        GameEvents.OnVoteCast -= RegisterVote;
        GameEvents.OnTaskCompleted -= CompleteTask;
    }
private void RouteNightStart(int round)
    {
        stateModel.SetPhase(GamePhase.Night);
    }

    private void RouteDayStart()
    {
        stateModel.SetPhase(GamePhase.Day);
        stateModel.ClearVotes(); 
    }

    private void RouteBodyReported(BaseCharacter victim)
    {
        stateModel.SetPhase(GamePhase.Voting);
    }

    private void RouteGameEnded(GameResult gameResult)
    {
        stateModel.SetPhase(GamePhase.Ended);
    }

    private void RegisterKill(BaseCharacter victim)
    {
        stateModel.RegisterKill(victim);
    }

    private void RegisterVote(BaseCharacter voter, BaseCharacter target)
    {
        stateModel.RegisterVote(voter, target);
    }

    private void CompleteTask(BaseCharacter completer)
    {
        stateModel.CompleteTask(completer);
    }
}

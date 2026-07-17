using System.Linq;
using UnityEngine;
using static GameConstants;

public class GameManager : MonoBehaviour
{
    public GameStateModel gameStateModel;
    void Awake()
    {
        gameStateModel = new GameStateModel();
    }

    private void CompleteTask(BaseCharacter completer)
    {
        stateModel.CompleteTask(completer);
    }

    private void OnEnable()
    {
        GameEvents.OnNightStarted += RouteNightStart;
        GameEvents.OnDayStarted += RouteDayStart;
        GameEvents.OnBodyReported += RouteBodyReported;
        GameEvents.OnGameEnded += RouteGameEnded;
        GameEvents.OnPlayerKilled += RegisterKill;
        GameEvents.OnPlayerKilled += RouteToWinConditions;
        GameEvents.OnVoteCast += HandleVoteCast;
        GameEvents.OnTaskCompleted += CompleteTask;
        GameEvents.OnArcadeMapLoaded += InitializePlayers;
        GameEvents.OnArcadeMapLoaded += AssignRoles;
    }

    private void OnDisable()
    {
        GameEvents.OnNightStarted -= RouteNightStart;
        GameEvents.OnDayStarted -= RouteDayStart;
        GameEvents.OnBodyReported -= RouteBodyReported;
        GameEvents.OnGameEnded -= RouteGameEnded;
        GameEvents.OnPlayerKilled -= RegisterKill;
        GameEvents.OnPlayerKilled -= RouteToWinConditions;
        GameEvents.OnVoteCast -= HandleVoteCast;
        GameEvents.OnTaskCompleted -= CompleteTask;
        GameEvents.OnArcadeMapLoaded -= InitializePlayers;
        GameEvents.OnArcadeMapLoaded -= AssignRoles;
    }
     private void RouteNightStart(int round) 
    => gameStateModel.SetPhase(GamePhase.Night);

    private void RouteDayStart()
    => gameStateModel.SetPhase(GamePhase.Day);

    private void RouteBodyReported(BaseCharacter reporter) 
    => gameStateModel.SetPhase(GamePhase.Voting);

    private void RouteGameEnded(GameResult result)
    => gameStateModel.SetPhase(GamePhase.Ended);

    private void RouteToWinConditions(BaseCharacter victim)
    => CheckWinConditions();

    private void RegisterKill(BaseCharacter victim)
    {
        gameStateModel.RegisterKill(victim);
    }

    private void HandleVoteCast(BaseCharacter voter, BaseCharacter target)
    {
        gameStateModel.RegisterVote(voter, target);
        
        if (gameStateModel.VoteTally == gameStateModel.AlivePlayersCount)
        {
            // Evaluate votes here and dispatch appropriate win events safely
        }
        CheckWinConditions();
    }

    private void CompleteTask(BaseCharacter completer)
    {
        gameStateModel.CompleteTask(completer);
        CheckWinConditions();
    }

    private void AssignRoles()
    {
        RoleFactory.AssignRoles();
    }

    private void InitializePlayers()
    {
        var players = FindObjectsByType<BaseCharacter>().ToList();
        gameStateModel.InitializePlayers(players);
    }

    private void CheckWinConditions()
    {

        var alivePlayers = gameStateModel.GetAlivePlayers();
        bool isHauntedAlive = alivePlayers.Any(player => player.Role == CharacterRole.Haunted);

        //Possible if Priest kills Haunted
        if (!isHauntedAlive)
        {
            GameEvents.GameEnded(GameResult.HuntersWin);
            Debug.Log("Priest has killed Haunted and Hunters win!");
            return; 
        }
        //if only one alive hunter is left and the haunted is still alive
        //mutually exclusive with the above condition
        else if (gameStateModel.AlivePlayersCount == 2)
        {
            GameEvents.GameEnded(GameResult.HauntedWins);
            Debug.Log("Only 1 Hunter left and Haunted wins!");
            return;
        }
        
        if(gameStateModel.TasksRemaining == 0)
        {
            GameEvents.GameEnded(GameResult.HuntersWin);
            Debug.Log("All tasks finished and Hunters win");
            return;
        }

        // check if Haunted is voted out
    }
}

using System.Linq;
using UnityEngine;
using static GameConstants;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameStateModel gameStateModel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        gameStateModel = new GameStateModel();
    }

    void Update()
    {
        
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
        var voteData = gameStateModel.GetVotes();
        
        // Default states
        BaseCharacter votedOut = null;
        bool isTie = true;

        if (voteData != null && voteData.Count > 0)
        {
            int mostVotes = voteData.Values.Max();
            int winnersCount = voteData.Count(entry => entry.Value == mostVotes);

            // If there's exactly one winner, it's not a tie
            if (winnersCount == 1)
            {
                isTie = false;
                votedOut = voteData.FirstOrDefault(x => x.Value == mostVotes).Key;
            }
        }

        // Process character death if someone was cleanly voted out
        if (!isTie && votedOut != null)
        {
            votedOut.OnCharacterDeath();
            CheckWinConditions();
        }

        // Broadcast the results directly to anyone listening (like the UI)
        GameEvents.VotingFinished(votedOut, isTie);

        // Reset the model data safely now that processing is done
        gameStateModel.ClearVotes();
    }
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

        if (!isHauntedAlive)
        {
            GameEvents.GameEnded(GameResult.HuntersWin);
            Debug.Log("Haunted is dead and Hunters win!");
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

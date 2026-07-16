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
            
            BaseCharacter votedOut = null;
            bool isTie = true;
            bool hasMajoritySkipped = gameStateModel.SkipCount * 2 > gameStateModel.AlivePlayersCount;

            if (voteData != null && voteData.Count > 0 && !hasMajoritySkipped)
            {
                int mostVotes = voteData.Values.Max();
                int winnersCount = voteData.Count(entry => entry.Value == mostVotes);

                if (winnersCount == 1)
                {
                    isTie = false;
                    votedOut = voteData.FirstOrDefault(x => x.Value == mostVotes).Key;
                }
            }


            if (!isTie && votedOut != null)
            {
                votedOut.OnCharacterDeath();
                gameStateModel.RegisterVotedOut(votedOut);
                CheckWinConditions();
            }

            gameStateModel.ResetSkipCount();
            GameEvents.VotingFinished(votedOut, isTie);
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
            Debug.Log("Haunted is dead and Hunters win!");
            GameEvents.GameEnded(GameResult.HuntersWin);
            return; 
        }

        else if (gameStateModel.AlivePlayersCount == 2)
        {
            Debug.Log("Only 1 Hunter left and Haunted wins!");
            GameEvents.GameEnded(GameResult.HauntedWins);
            return;
        }
        
        if (gameStateModel.TasksRemaining == 0)
        {
            Debug.Log("All tasks finished and Hunters win");
            GameEvents.GameEnded(GameResult.HuntersWin);
            return;
        }

    }
}

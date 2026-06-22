using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameConstants;

public class GameStateModel : MonoBehaviour
{
    private GamePhase currentPhase;
    private List<BaseCharacter> alivePlayers;
    private Dictionary<BaseCharacter,CharacterRole> roles;
    private Dictionary<BaseCharacter,int> votes;
    private HashSet<BaseCharacter> playersWhoVoted;
    private int voteTally;
    private int tasksRemaining;
    private int roundNumber;
    private int discussionCount;

    void Start()
    {
        roles = new Dictionary<BaseCharacter, CharacterRole>();
        votes = new Dictionary<BaseCharacter,int>();
        playersWhoVoted = new HashSet<BaseCharacter>();
    }

    public GamePhase CurrentPhase
    {
        get
        {
            return currentPhase;
        }
    }

    public int VoteTally
    {
        get
        {
            return voteTally;
        }
        private set
        {
            voteTally = value;
        }
    }

    public int TasksRemaining
    {
        get
        {
            return tasksRemaining;
        }
        private set
        {
            tasksRemaining = value;
        }
    }

    public int RoundNumber
    {
        get
        {
            return roundNumber;
        }
        private set
        {
            roundNumber = value;
        }
    }

    public int DiscussionCount
    {
        get
        {
            return discussionCount;
        }
        private set
        {
            discussionCount = value;
        }
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
        GameEvents.OnArcadeMapLoaded += GetAllPlayers;
        GameEvents.OnArcadeMapLoaded += AssignRoles;
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
        GameEvents.OnArcadeMapLoaded -= GetAllPlayers;
        GameEvents.OnArcadeMapLoaded -= AssignRoles;
    }
    private void RouteNightStart(int round) 
    => SetPhase(GamePhase.Night);

    private void RouteDayStart()
    => SetPhase(GamePhase.Day);

    private void RouteBodyReported(BaseCharacter reporter) 
    => SetPhase(GamePhase.Voting);

    private void RouteGameEnded(GameResult result)
    => SetPhase(GamePhase.Ended);

    // called once on ArcadeMapLoaded
    private void GetAllPlayers()
    {
        alivePlayers = FindObjectsByType<BaseCharacter>().ToList();
    }

    private void AssignRoles()
    {
        RoleFactory.AssignRoles();
        foreach(BaseCharacter character in alivePlayers)
        {
            roles.Add(character,character.Role);
        }
    }

    public void SetPhase(GamePhase gamePhase)
    {
        currentPhase = gamePhase;
    }

    public void RegisterKill(BaseCharacter victim)
    {
        alivePlayers.Remove(victim);
        if(alivePlayers.Count == 2)
        {
            GameEvents.GameEnded(GameResult.HauntedWins);
        }
    }

    public void RegisterVote(BaseCharacter voter, BaseCharacter target)
    {
        if(voter == null || target == null)
        {
            return;
        }

        if(playersWhoVoted.Contains(voter))
        {
            //player shouldn't be able to vote twice!
            return;
        }

        if(!votes.ContainsKey(target))
        {
            votes[target] = 1;
        }
        else
        {
            votes[target]++;
        }

        playersWhoVoted.Add(voter);

        voteTally++;

        if(voteTally == alivePlayers.Count)
        {
            //stop voting and count votes
        }
    }

    public void CompleteTask(BaseCharacter completer)
    {
        // to do: logic for individual characters on task completed
        tasksRemaining--;
    }
}

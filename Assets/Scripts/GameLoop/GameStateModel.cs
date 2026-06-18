using System;
using System.Collections.Generic;
using System.Diagnostics;
using static GameConstants;

public class GameStateModel
{
    private GamePhase currentPhase;
    private List<BaseCharacter> alivePlayers = new List<BaseCharacter>();
    private Dictionary<int,CharacterRole> roles = new Dictionary<int, CharacterRole>();
    private Dictionary<BaseCharacter,int> votes = new Dictionary<BaseCharacter,int>();
    private HashSet<BaseCharacter> playersWhoVoted = new HashSet<BaseCharacter>();
    private int voteTally;
    private int tasksRemaining;
    private int roundNumber;
    private int discussionCount;

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
    => SetPhase(GamePhase.Night);

    private void RouteDayStart()
    => SetPhase(GamePhase.Day);

    private void RouteBodyReported(BaseCharacter reporter) 
    => SetPhase(GamePhase.Voting);

    private void RouteGameEnded(GameResult result)
    => SetPhase(GamePhase.Ended);

    // called once on game start
    private void GetAllPlayers(List<BaseCharacter> allPlayersList)
    {
        alivePlayers.AddRange(allPlayersList);
    }

    public void SetPhase(GamePhase gamePhase)
    {
        currentPhase = gamePhase;
    }

    public void RegisterKill(BaseCharacter victim)
    {
        alivePlayers.Remove(victim);
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

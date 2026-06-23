using System;
using System.Collections.Generic;
using static GameConstants;

public class GameStateModel
{
    private GamePhase currentPhase;
    private List<BaseCharacter> alivePlayers = new List<BaseCharacter>();
    private Dictionary<int,CharacterRole> roles = new Dictionary<int, CharacterRole>();
    private Dictionary<BaseCharacter,int> votes = new Dictionary<BaseCharacter,int>();
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

    public void GetAllPlayers(List<BaseCharacter> allPlayersList)
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
        // Add a new key, value pair to votes if target doesn't exist already
        // If the target already exists, increase the value(int)
        if (votes.ContainsKey(target))
        {
            votes[target]++;
        }
        else
        {
            votes[target] = 1;
        }

        voteTally++;
    }

    public void CompleteTask(BaseCharacter completer)
    {
        tasksRemaining--;
    }
    
    public void ClearVotes()
    {
        votes.Clear();
        voteTally = 0;
    }
}

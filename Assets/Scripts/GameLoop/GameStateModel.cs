using System.Collections.Generic;
using static GameConstants;

public class GameStateModel
{
    private GamePhase currentPhase;
    private List<BaseCharacter> alivePlayers = new List<BaseCharacter>();
    private Dictionary<int,CharacterRole> roles = new Dictionary<int, CharacterRole>();
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
        private set
        {
            currentPhase = value;
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

    // called once on game start
    private void GetAllPlayers(List<BaseCharacter> allPlayersList)
    {
        alivePlayers.AddRange(allPlayersList);
    }

    public void SetPhase()
    {
        //logic for phases
    }

    public void RegisterKill(BaseCharacter victim)
    {
        alivePlayers.Remove(victim);
    }

    public void RegisterVote()
    {
        voteTally++;
    }
}

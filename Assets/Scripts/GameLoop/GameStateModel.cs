using System.Collections.Generic;
using static GameConstants;

public class GameStateModel
{
    private GamePhase currentPhase;
    private List<BaseCharacter> alivePlayers = new List<BaseCharacter>();
    private Dictionary<BaseCharacter, CharacterRole> roles = new Dictionary<BaseCharacter, CharacterRole>();
    private Dictionary<BaseCharacter, int> votes = new Dictionary<BaseCharacter, int>();
    private HashSet<BaseCharacter> playersWhoVoted = new HashSet<BaseCharacter>();
    private int voteTally;
    private int tasksRemaining = 10;
    private int roundNumber;
    private int discussionCount;

    // Use property shortcuts for cleaner reading
    public GamePhase CurrentPhase => currentPhase;
    public int VoteTally => voteTally;
    public int TasksRemaining => tasksRemaining;
    public int RoundNumber => roundNumber;
    public int DiscussionCount => discussionCount;
    public int AlivePlayersCount => alivePlayers.Count;

    public void SetPhase(GamePhase gamePhase) => currentPhase = gamePhase;

    // Receives the players gathered by the GameManager
    public void InitializePlayers(List<BaseCharacter> players)
    {
        alivePlayers = players;
    }

    // Stores assigned role statuses natively if needed
    public void PopulateRoles()
    {
        roles.Clear();
        foreach (var character in alivePlayers)
        {
            roles.Add(character, character.Role);
        }
    }

    public void RegisterKill(BaseCharacter victim)
    {
        if (alivePlayers.Contains(victim))
        {
            alivePlayers.Remove(victim);
        }
    }

    public void RegisterVote(BaseCharacter voter, BaseCharacter target)
    {
        if (voter == null || target == null || playersWhoVoted.Contains(voter))
        {
            return;
        }

        if (!votes.ContainsKey(target))
        {
            votes[target] = 1;
        }
        else
        {
            votes[target]++;
        }

        playersWhoVoted.Add(voter);
        voteTally++;

        if (voteTally == alivePlayers.Count)
        {
            // End of voting logic goes here...
        }
    }

    public void CompleteTask(BaseCharacter completer)
    {
        if (tasksRemaining > 0)
        {
            tasksRemaining--;
        }
    }

    public List<BaseCharacter> GetAlivePlayers()
    {
        return alivePlayers;
    }
}

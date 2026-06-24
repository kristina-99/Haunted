public class VoteCommand : ICommand
{
    private BaseCharacter voter;
    private BaseCharacter target;
    public VoteCommand(BaseCharacter voter, BaseCharacter target)
    {
        this.voter = voter;
        this.target = target;
    }

    public void Execute()
    {
        GameEvents.VoteCast(voter, target);
    }
}

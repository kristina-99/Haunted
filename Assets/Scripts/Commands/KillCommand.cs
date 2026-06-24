public class KillCommand : ICommand
{
    private const float KillRange = 3.0f;
    private BaseCharacter attacker;
    private BaseCharacter target;
    private HauntedRole hauntedRole;
    private float currentDistance;

    public KillCommand(BaseCharacter attacker, BaseCharacter target)
    {
        hauntedRole = attacker.gameObject.GetComponent<HauntedRole>();
        this.attacker = attacker;
        this.target = target;
    }

    public void Execute()
    {
        //check if currentPhase is Night!!!

        currentDistance = Extensions.CalculateDistance(target,attacker);
        if (hauntedRole != null && hauntedRole.CanKill && (currentDistance <= KillRange) && !hauntedRole.IsInTheSafeZone)
        {
            target.OnCharacterDeath();
            GameEvents.PlayerKilled(target);
            hauntedRole.DisableKill();
        }
    }
}
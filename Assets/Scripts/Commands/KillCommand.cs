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

        //subscribing here because Enable and Disable don't work on pure C# classes
        if(hauntedRole != null)
        {
            GameEvents.OnNightStarted += hauntedRole.AllowKill;
        }
    }

    public void Execute()
    {
        currentDistance = Extensions.CalculateDistance(target,attacker);
        if (hauntedRole != null && hauntedRole.CanKill && (currentDistance <= KillRange) && !hauntedRole.IsInTheSafeZone)
        {
            target.OnCharacterDeath();
            GameEvents.PlayerKilled(target);
            hauntedRole.DisableKill();
        }
    }

    public void Dispose()
    {
        // call manually to unsubscribe
        if (hauntedRole != null)
        {
            GameEvents.OnNightStarted -= hauntedRole.AllowKill;
        }
    }
}
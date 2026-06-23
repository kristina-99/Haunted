using System.Collections.Generic;
using UnityEngine;

public class KillCommand : ICommand
{
    private BaseCharacter attacker;
    private BaseCharacter target;
    private HauntedRole hauntedRole;

    public KillCommand(BaseCharacter attacker, BaseCharacter target)
    {
        hauntedRole = attacker.gameObject.GetComponent<HauntedRole>();
        this.attacker = attacker;
        this.target = target;
    }

    public void Execute()
    {
        if (hauntedRole != null)
        {
            hauntedRole.Kill(target);
        }
    }
}

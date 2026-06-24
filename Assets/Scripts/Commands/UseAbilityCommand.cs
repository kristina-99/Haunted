using UnityEngine;
using UnityEngine.TextCore.Text;
using static GameConstants;
using static GameEvents;

public class UseAbilityCommand : ICommand
{
    private Animator animator;
    private RoleBase characterRole;

    private BaseCharacter target;
    
    public UseAbilityCommand(Animator animator, RoleBase characterRole, BaseCharacter target)
    {
        this.animator = animator;
        this.characterRole = characterRole;
        this.target = target;
        OnDayStarted += SetAbilityOff;
        OnNightStarted += RouteNightStart;
    }

    public void Execute()
    {
        animator.SetTrigger("UseAbility");
        characterRole.UseAbility(target);
    }

    private void SetAbilityOn()
    {
        //Haunted role and priest role abilities are once per game
        //Normal role ability is always allowed
        if(!(characterRole is HauntedRole) && !(characterRole is NormalRole) && !(characterRole is PriestRole))
        {
            characterRole.AllowAbility();
        }
    }

    private void SetAbilityOff()
    {
        if(!(characterRole is NormalRole))
        {
            characterRole.DisableAbility();
        }
    }

    private void RouteNightStart(int round) 
    => SetAbilityOn();
}

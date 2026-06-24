using UnityEngine;

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
    }

    public void Execute()
    {
        animator.SetTrigger("UseAbility");

        // check for current GamePhase
        if(!(characterRole is HauntedRole) && !(characterRole is NormalRole) && !(characterRole is PriestRole) && characterRole.CanUseAbility)
        {
            characterRole.UseAbility(target);
        }    
    }
}

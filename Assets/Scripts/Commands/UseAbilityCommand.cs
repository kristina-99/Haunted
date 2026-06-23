using UnityEngine;
using UnityEngine.TextCore.Text;
using static GameConstants;
using static GameEvents;

public class UseAbilityCommand : ICommand
{
    private Animator animator;
    private RoleBase characterRole;
    
    public UseAbilityCommand(Animator animator, RoleBase characterRole)
    {
        this.animator = animator;
        this.characterRole = characterRole;
    }

    public void Execute()
    {
        animator.SetTrigger("UseAbility");
        characterRole.UseAbility();
    }
}

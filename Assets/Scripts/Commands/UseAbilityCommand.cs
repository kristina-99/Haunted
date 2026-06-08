using UnityEngine;
using UnityEngine.TextCore.Text;
using static GameEvents;

public class UseAbilityCommand : ICommand
{
    private Animator animator;
    
    public UseAbilityCommand(Animator animator)
    {
        this.animator = animator;
    }

    public void Execute()
    {
        animator.SetTrigger("UseAbility");
    }
}

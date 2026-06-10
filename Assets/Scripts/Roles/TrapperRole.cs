using UnityEngine;

public class TrapperRole : RoleBase
{
    public override void UseAbility(BaseCharacter target)
    {
        //after using the ability
        canUseAbility = false;
    }
}

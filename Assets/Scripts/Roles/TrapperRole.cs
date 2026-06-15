using UnityEngine;

public class TrapperRole : RoleBase
{
    public override void UseAbility()
    {
        //after using the ability
        canUseAbility = false;
    }
}

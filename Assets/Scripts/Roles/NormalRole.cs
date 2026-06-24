using UnityEngine;

public class NormalRole : RoleBase
{
    public override void UseAbility(BaseCharacter target)
    {
        //task speed - consistent for the whole game
        canUseAbility = true;
    }
}

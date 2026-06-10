using UnityEngine;

public class NormalRole : RoleBase
{
    public override void UseAbility(BaseCharacter character)
    {
        //task speed
        canUseAbility = false;
    }
}

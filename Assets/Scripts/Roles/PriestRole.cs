using UnityEngine;
using static GameConstants;

public class PriestRole : RoleBase
{
    private BaseCharacter character;
    public override void UseAbility(BaseCharacter target)
    {
        if(target.Role == CharacterRole.Haunted)
        {
            target.GetKilled();
        }
        else
        {
            character = GetComponent<BaseCharacter>();
            character.GetKilled();
        }

        canUseAbility = false;
    }
}

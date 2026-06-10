using UnityEngine;
using static GameConstants;

public class PriestRole : RoleBase
{
    private BaseCharacter character;
    public override void UseAbility(BaseCharacter target)
    {
        if(target.Role == CharacterRole.Haunted)
        {
            target.getKilled();
        }
        else
        {
            character = GetComponent<BaseCharacter>();
            character.getKilled();
        }

        canUseAbility = false;
    }
}

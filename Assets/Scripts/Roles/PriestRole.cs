using UnityEngine;
using static GameConstants;

public class PriestRole : RoleBase
{
    private BaseCharacter character;
    private BaseCharacter target;
    public override void UseAbility()
    {
        target = transform.GetClosestTarget(character);
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

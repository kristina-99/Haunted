using UnityEngine;
using static GameConstants;

public class PriestRole : RoleBase
{
    private BaseCharacter character;
    private BaseCharacter target;

    void Awake()
    {
        character = GetComponent<BaseCharacter>();
    }

    public override void UseAbility()
    {
        target = transform.GetClosestTarget(character);
        if(target.Role == CharacterRole.Haunted)
        {
            target.GetKilled();
            Debug.Log("Congratulations, you have killed the Haunted and won the game!");
        }
        else
        {
            character.GetKilled();
        }

        canUseAbility = false;
    }
}

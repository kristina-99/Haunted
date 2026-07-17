using UnityEngine;
using static GameConstants;

public class PriestRole : RoleBase
{
    private BaseCharacter character;

    void Awake()
    {
        character = GetComponent<BaseCharacter>();
    }

    public override void UseAbility(BaseCharacter target)
    {
        if (target.Role == CharacterRole.Haunted)
        {
            target.OnCharacterDeath();
            GameEvents.PlayerKilled(target);
        }
        else
        {
            character.OnCharacterDeath();
            GameEvents.PlayerKilled(character);
            Debug.Log("You couldn't identify the haunted have died");
        }

        canUseAbility = false;
    }
}
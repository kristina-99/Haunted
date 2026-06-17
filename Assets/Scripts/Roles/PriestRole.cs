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
        if(target.Role == CharacterRole.Haunted)
        {
            target.OnCharacterDeath();
            GameEvents.PlayerKilled(target);
            Debug.Log("Congratulations, you have killed the Haunted and won the game!");
            GameEvents.GameEnded(GameResult.HuntersWin);
        }
        else
        {
            character.OnCharacterDeath();
            GameEvents.PlayerKilled(character);
        }

        canUseAbility = false;
    }
}
using System;

public static class Extensions
{
    static bool IsAlive(BaseCharacter character)
    {
        if(character.Health > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static bool IsHunter(BaseCharacter character)
    {
        if(character.Role != GameConstants.CharacterRole.Haunted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static bool IsHaunted(BaseCharacter character)
    {
        if(character.Role == GameConstants.CharacterRole.Haunted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

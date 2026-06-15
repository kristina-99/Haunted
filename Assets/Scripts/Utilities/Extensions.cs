using UnityEngine;

public static class Extensions
{
    static bool IsAlive(BaseCharacter character)
    {
        if(character.IsAlive)
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

    public static BaseCharacter GetClosestTarget(this Transform origin, BaseCharacter self)
    {
        BaseCharacter closest = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPos = origin.position;

        foreach (BaseCharacter character in Object.FindObjectsByType<BaseCharacter>())
        {
            if (character == self) continue; 

            float dSqr = (character.transform.position - currentPos).sqrMagnitude;
            if (dSqr < closestDistanceSqr)
            {
                closestDistanceSqr = dSqr;
                closest = character;
            }
        }
        return closest;
    }
}

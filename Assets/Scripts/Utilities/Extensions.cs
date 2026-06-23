using System;
using System.Numerics;
using UnityEngine.TextCore;


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
            if (character == self || !IsAlive(character)) continue; 

            float dSqr = (character.transform.position - currentPos).sqrMagnitude;
            if (dSqr < closestDistanceSqr)
            {
                closestDistanceSqr = dSqr;
                closest = character;
            }
        }
        return closest;
    }

    public static void MoveSphereCollider(SphereCollider collider, Vector3 destination)
    {
        collider.transform.position = destination;
        collider.center = Vector3.zero;
    }

    public static void SpawnDeadBody(GameObject deadBodyPrefab, Vector3 destination)
    {
        Object.Instantiate(deadBodyPrefab,destination,Quaternion.identity);
    }
}

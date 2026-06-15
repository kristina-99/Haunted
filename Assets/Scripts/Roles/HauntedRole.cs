using NUnit.Framework;
using UnityEngine;

public class HauntedRole : RoleBase
{
    private float distanceFromTarget;
    private bool isInTheSafeZone = false;
    private const float AttackDistance = 3.0f;
    private bool canKill = true;

    public override void UseAbility(BaseCharacter target)
    {
        //else lights out

        //after using the ability
        canUseAbility = false;
    }

    public void Kill(BaseCharacter target)
    {
        if(canKill)
        {
            CalculateDistance(target);
            if(distanceFromTarget <= AttackDistance && isInTheSafeZone == false)
            {
                target.getKilled();
            }
        }
        canKill = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SafeZone"))
        {
            isInTheSafeZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("SafeZone"))
        {
            isInTheSafeZone = false;
        }
    }

    private void CalculateDistance(BaseCharacter target)
    {
        distanceFromTarget = Vector3.Distance(target.gameObject.transform.position, this.gameObject.transform.position);
    }
}

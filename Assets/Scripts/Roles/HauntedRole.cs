using NUnit.Framework;
using UnityEngine;

public class HauntedRole : RoleBase
{
    private float distanceFromtarget;
    private bool isInTheSafeZone = false;
    private const float AttackDistance = 3.0f;
    public override void UseAbility(BaseCharacter target)
    {
        CalculateDistance(target);
        if(distanceFromtarget <= AttackDistance && isInTheSafeZone == false)
        {
            target.getKilled();
        }
        //else lights out

        //after using the ability
        canUseAbility = false;
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
        float distanceFromTarget = Vector3.Distance(target.gameObject.transform.position, this.transform.position);
    }
}

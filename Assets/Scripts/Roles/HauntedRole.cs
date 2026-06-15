using NUnit.Framework;
using UnityEngine;

public class HauntedRole : RoleBase
{
    private float distanceFromTarget;
    private const float AttackDistance = 3.0f;
    private const float LightsOnPeriod = 30f;
    private bool isInTheSafeZone = false;
    private bool canKill = true;
    private bool isLightOn = false;

    public override void UseAbility()
    {
        if(canUseAbility)
        {
            isLightOn = true;
            Debug.Log("Lights are on!");
            Invoke("TurnOffLights", LightsOnPeriod);
        }
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
                target.GetKilled();
                GameEvents.PlayerKilled(target);
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

    private void TurnOffLights()
    {
        isLightOn = false;
        Debug.Log("Lights are off!");
    }
}

using UnityEngine;

public class HauntedRole : RoleBase
{
    private float distanceFromTarget;
    private const float AttackDistance = 3.0f;
    private const float LightsOffPeriod = 30f;
    private const float StunTime = 10f;
    private bool isInTheSafeZone = false;
    private bool canKill = true;
    private bool isLightOn = true;

    public bool IsInTheSafeZone
    {
        get
        {
            return isInTheSafeZone;
        }
    }

    public bool CanKill
    {
        get
        {
            return canKill;
        }
    }

    public override void UseAbility(BaseCharacter target)
    {
        if(canUseAbility)
        {
            isLightOn = false;
            Debug.Log("Lights are off!");
            Invoke("TurnOnLights", LightsOffPeriod);
        }
        
        canUseAbility = false;
    }

    public void AllowKill(int roundNumber)
    {
        canKill = true;
    }

    public void DisableKill()
    {
        canKill = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Trap"))
        {
           GameEvents.HauntedStunned();
        }
    }

    private void OnTriggerStay(Collider other)
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

    private void TurnOnLights()
    {
        isLightOn = true;
        Debug.Log("Lights are back on!");
    }
}
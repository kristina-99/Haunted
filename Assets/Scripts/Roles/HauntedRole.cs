using Unity.VisualScripting;
using UnityEngine;

public class HauntedRole : RoleBase
{
    private const float LightsOffPeriod = 30f;
    private bool isInTheSafeZone = false;
    private bool canKill = true;
    private bool isLightOn = false;

    protected override void HandleNightStarted(int roundNumber)
    {
        AllowKill(roundNumber);
    }

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
            isLightOn = true;
            Debug.Log("Lights are off!");
            Invoke("TurnOffLights", LightsOffPeriod);
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

    private void TurnOffLights()
    {
        isLightOn = false;
        Debug.Log("Lights are on!");
    }
}
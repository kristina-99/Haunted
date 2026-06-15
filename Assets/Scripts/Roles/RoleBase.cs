using UnityEngine;

public abstract class RoleBase : MonoBehaviour
{
    protected bool canUseAbility = true;
    public abstract void UseAbility();
    private void AllowAbility()
    {
        canUseAbility = true;
    }

    private void OnEnable()
    {
        GameEvents.OnNightStarted += RouteNightStart;
    }
    private void OnDisable()
    {
        GameEvents.OnNightStarted -= RouteNightStart;
    }
    private void RouteNightStart(int round) 
    => AllowAbility();
}

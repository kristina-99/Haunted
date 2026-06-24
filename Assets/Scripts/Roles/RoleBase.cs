using UnityEngine;

public abstract class RoleBase : MonoBehaviour
{
    protected bool canUseAbility = true;
    public abstract void UseAbility(BaseCharacter target);

    public bool CanUseAbility
    {
        get
        {
            return canUseAbility;
        }
    }

    protected virtual void OnEnable()
    {
        GameEvents.OnNightStarted += HandleNightStarted;
    }

    protected virtual void OnDisable()
    {
        GameEvents.OnNightStarted -= HandleNightStarted;
    }

    protected virtual void HandleNightStarted(int roundNumber) { }
    
    public void AllowAbility()
    {
        canUseAbility = true;
    }

    public void DisableAbility()
    {
        canUseAbility = false;
    }
}

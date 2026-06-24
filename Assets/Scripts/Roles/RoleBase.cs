using UnityEngine;

public abstract class RoleBase : MonoBehaviour
{
    protected bool canUseAbility = true;
    public abstract void UseAbility(BaseCharacter target);
    public void AllowAbility()
    {
        canUseAbility = true;
    }

    public void DisableAbility()
    {
        canUseAbility = false;
    }
}

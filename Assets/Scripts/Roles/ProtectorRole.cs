using UnityEngine;

public class ProtectorRole : RoleBase
{
    public SphereCollider protectedZone;
    public override void UseAbility(BaseCharacter target)
    {
        protectedZone.center = transform.InverseTransformPoint(gameObject.transform.position);
        protectedZone.enabled = true;  
        canUseAbility = false;
    }

}

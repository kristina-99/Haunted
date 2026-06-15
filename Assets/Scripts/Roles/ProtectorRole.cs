using UnityEngine;

public class ProtectorRole : RoleBase
{
    public SphereCollider protectedZone;
    public override void UseAbility()
    {
        protectedZone.center = transform.InverseTransformPoint(gameObject.transform.position);
        protectedZone.enabled = true;  
        canUseAbility = false;
    }

}

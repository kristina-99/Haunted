using UnityEngine;

public class ProtectorRole : RoleBase
{
    private GameObject safeZoneObject;
    private SphereCollider protectedZone;

    void Awake()
    {
        safeZoneObject = GameObject.FindWithTag("SafeZone");
        if (safeZoneObject != null)
        {
            protectedZone = safeZoneObject.GetComponent<SphereCollider>();
        }
    }

    protected override void HandleNightStarted(int roundNumber)
    {
        AllowAbility();
    }

    public override void UseAbility(BaseCharacter target)
    {
        Extensions.MoveSphereCollider(protectedZone,transform.position);
        protectedZone.enabled = true;
        canUseAbility = false;
    }
}

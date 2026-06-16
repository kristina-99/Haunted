using UnityEngine;

public class ProtectorRole : RoleBase
{
    private GameObject safeZoneObject;
    private SphereCollider protectedZone;

    void OnEnable()
    {
        GameEvents.OnNightStarted += ResetAbility;
    }

    void OnDisable()
    {
        GameEvents.OnNightStarted -= ResetAbility;
    }

    void Awake()
    {
        safeZoneObject = GameObject.FindWithTag("SafeZone");
        if (safeZoneObject != null)
        {
            protectedZone = safeZoneObject.GetComponent<SphereCollider>();
        }
    }

    public override void UseAbility()
    {
        protectedZone.transform.position = transform.position;
        protectedZone.center = Vector3.zero;
        protectedZone.enabled = true;
        canUseAbility = false;
    }

    private void ResetAbility(int roundNumber)
    {
        protectedZone.enabled = false;
        canUseAbility = true;
    }
}

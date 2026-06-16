using UnityEngine;

public class TrapperRole : RoleBase
{
    private GameObject trapObject;
    private SphereCollider trapZone;

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
        trapObject = GameObject.FindWithTag("Trap");
        if (trapObject != null)
        {
            trapZone = trapObject.GetComponent<SphereCollider>();
        }
    }
    public override void UseAbility()
    {
        Extensions.MoveSphereCollider(trapZone,transform.position);
        trapZone.enabled = true;
        canUseAbility = false;
    }

    private void ResetAbility(int roundNumber)
    {
        trapZone.enabled = false;
        canUseAbility = true;
    }
}

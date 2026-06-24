using UnityEngine;

public class TrapperRole : RoleBase
{
    private GameObject trapObject;
    private SphereCollider trapZone;

    void Awake()
    {
        trapObject = GameObject.FindWithTag("Trap");
        if (trapObject != null)
        {
            trapZone = trapObject.GetComponent<SphereCollider>();
        }
    }

    protected override void HandleNightStarted(int roundNumber)
    {
        AllowAbility();
    }

    public override void UseAbility(BaseCharacter target)
    {
        Extensions.MoveSphereCollider(trapZone,transform.position);
        trapZone.enabled = true;
        canUseAbility = false;
        Debug.Log("You have set a trap!");
    }
}

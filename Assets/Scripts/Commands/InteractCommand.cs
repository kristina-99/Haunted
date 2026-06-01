using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class InteractCommand : ICommand
{
    public GameObject currentHitObject;
    private const float SphereRadius = 1.5f;
    private Rigidbody rb;
    private RaycastHit hit;
    private float maxDistance = 5f;
    private float currentHitDistance;
    private Vector3 origin;
    private Vector3 direction;
    private Animator animator;

    public InteractCommand(Rigidbody rb, Animator animator)
    {
        this.rb = rb;
        this.animator = animator;
    }

    public void Execute()
    {
        
        origin = rb.transform.position;
        direction = rb.transform.forward;

        int layerMask = LayerMask.GetMask("Default");

        if(Physics.SphereCast(origin,SphereRadius,direction,out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.GameObject();
            currentHitDistance = hit.distance;
            animator.SetTrigger("Interact");
        }
        else
        {
            currentHitDistance = maxDistance;
            currentHitObject = null;
        }
    }
}

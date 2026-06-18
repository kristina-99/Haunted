using UnityEngine;

public class InteractCommand : ICommand
{
    private Rigidbody rb;
    private Animator animator;
    private const float SphereRadius = 0.5f;
    private const float MaxDistance = 5.0f;
    public GameObject CurrentHitObject { get; private set; }
    public float CurrentHitDistance { get; private set; }

    public InteractCommand(Rigidbody rb, Animator animator)
    {
        this.rb = rb;
        this.animator = animator;
    }

    public void Execute()
    {
        Vector3 origin = rb.transform.position;
        Vector3 direction = rb.transform.forward;

        if (Physics.SphereCast(origin, SphereRadius, direction, out RaycastHit hit, MaxDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal))
        {
            CurrentHitObject = hit.transform.gameObject;
            CurrentHitDistance = hit.distance;

            if (CurrentHitObject.TryGetComponent(out IInteractable interactable))
            {
                animator.SetTrigger("Interact");
                
                var playerController = rb.GetComponent<PlayerController>();
                interactable.Interact(playerController);
            }
        }
        else
        {
            CurrentHitDistance = MaxDistance;
            CurrentHitObject = null;
        }
    }
}
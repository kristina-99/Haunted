using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class InteractCommand : ICommand
{
    public GameObject currentHitObject;
    private const float SphereRadius = 0.5f;
    private const float MaxDistance = 5.0f;
    private Rigidbody rb;
    private RaycastHit hit;
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

        int layerMask = LayerMask.GetMask("Cabinet");

        if(Physics.SphereCast(origin,SphereRadius,direction,out hit, MaxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            animator.SetTrigger("Interact");
        }
        else
        {
            currentHitDistance = MaxDistance;
            currentHitObject = null;
        }

        DetectCabinet();
    }

    private void DetectCabinet()
    {
        if(currentHitObject != null)
        {
            IMiniGame miniGame = currentHitObject.GetComponent<IMiniGame>();
            Debug.Log("This minigame is " + miniGame.DisplayName);
        }
    }
}

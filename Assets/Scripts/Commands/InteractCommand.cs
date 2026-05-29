using UnityEngine;

public class InteractCommand : MonoBehaviour, ICommand
{
    public LayerMask layerMask;
    private Rigidbody rb;

    public InteractCommand(Rigidbody rb)
    {
        this.rb = rb;
    }

    public void Execute()
    {
        
        Vector3 origin = rb.transform.position;
        float radius = 1.5f;
        Vector3 direction = rb.transform.forward;
        RaycastHit hit;

        if(Physics.SphereCast(origin,radius,direction,out hit,layerMask))
        {
            
        }
        //sphere cast - first param center point/the center of the character
        //second param - radius(in the development plan)
        //third param - direction
        // hit info
        //maxDistance
        //layer mask to determine which colliders can interact based on layer
    }
}

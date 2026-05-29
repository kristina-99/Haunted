using UnityEngine;

public class MoveCommand : MonoBehaviour, ICommand
{
    private Rigidbody rb;
    private float velocity;

    public MoveCommand(Rigidbody rb, float velocity)
    {
        this.rb = rb;
        this.velocity = velocity;
    }

    public void Execute()
    {
        rb.linearVelocity += new Vector3(velocity,0,0);
        Quaternion.LookRotation(rb.linearVelocity.normalized);
    }
}

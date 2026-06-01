using UnityEngine;

public class MoveCommand : ICommand
{
    //public Animator animator;
    private Rigidbody rb;
    private float velocityX;
    private float velocityZ;

    public MoveCommand(Rigidbody rb, float velocityX, float velocityZ, Animator animator)
    {
        this.rb = rb;
        this.velocityX = velocityX;
        this.velocityZ = velocityZ;
        animator.SetFloat("VelocityX", velocityX);
        animator.SetFloat("VelocityZ", velocityZ);
    }

    public void Execute()
    {
        rb.linearVelocity += new Vector3(velocityX , 0, velocityZ);
        Rotate();
    }

    private void Rotate()
    {
        if(rb.linearVelocity.z > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity.normalized);
            rb.MoveRotation(targetRotation);
        }
    }
}

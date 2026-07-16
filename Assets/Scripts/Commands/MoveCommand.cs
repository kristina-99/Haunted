using UnityEngine;

public class MoveCommand : ICommand
{
    private Rigidbody rb;
    private Animator animator;
    private float velocityX;
    private float velocityZ;

    public MoveCommand(Rigidbody rb, float velocityX, float velocityZ, Animator animator)
    {
        this.rb = rb;
        this.velocityX = velocityX;
        this.velocityZ = velocityZ;
        this.animator = animator;
    }

    public void Execute()
    {
        if(animator != null)
        {
            animator.SetFloat("VelocityX", velocityX);
            animator.SetFloat("VelocityZ", velocityZ);
            rb.linearVelocity += new Vector3(velocityX , 0, velocityZ);
            Rotate();
        }
    }

    private void Rotate()
    {
        if (velocityX != 0 || velocityZ != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity.normalized);
            rb.MoveRotation(targetRotation);
        }
    }
}
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using static GameConstants;

[RequireComponent(typeof(Animator))]
public abstract class BaseCharacter : MonoBehaviour
{
    public GameObject deadBody;
    private static readonly int DeadHash = Animator.StringToHash("Dead");
    private const float DeathDelay = 1f;
    protected Animator animator;
    private bool isAlive = true;
    private CharacterRole role;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
        set
        {
            isAlive = value;
        }
    }

    public CharacterRole Role
    {
        get
        {
            return role;
        }
        set
        {
            role = value;
        }
    }

    public void GetKilled()
    {
        isAlive = false;
        animator.SetBool("Dead", true);
        Destroy(this.gameObject, DeathDelay);
        Extensions.SpawnDeadBody(deadBody,transform.position);
    }

    public abstract void OnRoleAction();

    void QueueCommand()
    {
        
    }
    
}

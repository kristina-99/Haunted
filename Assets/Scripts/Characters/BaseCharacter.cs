using UnityEngine;
using UnityEngine.Assertions.Must;
using static GameConstants;

[RequireComponent(typeof(Animator))]
public abstract class BaseCharacter : MonoBehaviour
{
    private const float DeathDelay = 3f;
    private Animator animator;
    private bool isAlive = true;
    private CharacterRole role;

    void Awake()
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

    public void getKilled()
    {
        isAlive = false;
        animator.SetBool("Dead",true);
        Destroy(this.gameObject, DeathDelay);
    }

    public abstract void OnRoleAction();

    void QueueCommand()
    {
        
    }
    
}

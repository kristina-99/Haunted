using UnityEngine;
using UnityEngine.Assertions.Must;
using static GameConstants;

public abstract class BaseCharacter : MonoBehaviour
{
    private int health;
    private CharacterRole role;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
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
        health = 0;
        //play death animation//
        Destroy(this);
    }

    public abstract void OnRoleAction();

    void QueueCommand()
    {
        
    }
    
}

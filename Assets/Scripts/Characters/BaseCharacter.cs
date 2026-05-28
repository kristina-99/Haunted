using UnityEngine;
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

    public abstract void OnRoleAction();

    void QueueCommand()
    {
        
    }
    
}

using UnityEngine;

public class BotAgent : BaseCharacter
{
    public override void OnRoleAction()
    {
        throw new System.NotImplementedException();
    }

    void OnEnable()
    {
        GameEvents.OnDayStarted += VoteRandomly;
    }
    void OnDisable()
    {
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void VoteRandomly()
    {
        
    }
}

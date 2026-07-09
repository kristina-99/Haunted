using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;

public class BotAgent : BaseCharacter
{
    private Queue<ICommand> actionQueue = new Queue<ICommand>();

    void FixedUpdate()
    {
        if(actionQueue.Count != 0)
        {
            ExecuteNextCommand();
        }
    }
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
        GameEvents.OnDayStarted -= VoteRandomly;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void VoteRandomly()
    {
        if (this == null) return;
        
        List<BaseCharacter> alivePlayers = GameManager.Instance.gameStateModel.GetAlivePlayers();
        int votedCharacterIndex = UnityEngine.Random.Range(0, alivePlayers.Count);
        BaseCharacter votedCharacter = alivePlayers[votedCharacterIndex];

        VoteCommand voteCommand = new VoteCommand(this,votedCharacter);
        UnityEngine.Debug.Log($"{gameObject.name} voted for: {votedCharacter.gameObject.name}");
        ScheduleCommand(voteCommand);
        
    }

    private void ScheduleCommand(ICommand command)
    {
        actionQueue.Enqueue(command);
    }

    private void ExecuteNextCommand()
    {
        ICommand activeCommand = actionQueue.Dequeue();
        activeCommand.Execute();
    }
}

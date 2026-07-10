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

    BaseCharacter votedCharacter = null;
    float votingProbability = 2f / 3f;

    if (UnityEngine.Random.value <= votingProbability)
    {
        var alivePlayers = GameManager.Instance.gameStateModel.GetAlivePlayers();
        votedCharacter = alivePlayers[UnityEngine.Random.Range(0, alivePlayers.Count)];
    }

    string targetName = votedCharacter != null ? votedCharacter.gameObject.name : "Skip/No one";
    UnityEngine.Debug.Log($"{gameObject.name} voted for: {targetName}");

    ScheduleCommand(new VoteCommand(this, votedCharacter));
        
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

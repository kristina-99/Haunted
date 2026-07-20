using System.Collections.Generic;

public class BotAgent : BaseCharacter
{
    private Queue<ICommand> actionQueue = new Queue<ICommand>();

    void FixedUpdate()
    {
        if (actionQueue.Count != 0)
        {
            ExecuteNextCommand();
        }
    }

    public override void OnRoleAction()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {

    }

    void Update()
    {
        
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

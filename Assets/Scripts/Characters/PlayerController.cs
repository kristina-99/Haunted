using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : BaseCharacter
{
    private GameStateModel gameStateModel;
    private const float Speed = 1f;
    private Rigidbody rigidBody;
    private MoveCommand moveCommand;
    private float inputHorizontal;
    private float inputVertical;
    private static Queue<ICommand> actionQueue = new Queue<ICommand>();

    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();
        gameStateModel = new GameStateModel();
    }

    void FixedUpdate()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        moveCommand = new MoveCommand(rigidBody, inputHorizontal * Speed, inputVertical * Speed, animator);
        ScheduleCommand(moveCommand);
        ExecuteNextCommand();
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

    void OnUseAbility()
    {
        UseAbilityCommand useAbilityCommand = new UseAbilityCommand(animator, gameObject.GetComponent<RoleBase>());
        ScheduleCommand(useAbilityCommand);
    }

    void OnKill()
    {
        KillCommand killCommand = new KillCommand(this, transform.GetClosestTarget(this));
        ScheduleCommand(killCommand);
    }

    void OnInteract()
    {
        InteractCommand interactCommand = new InteractCommand(rigidBody, animator);
        ScheduleCommand(interactCommand);
    }

    public override void OnRoleAction()
    {
        throw new System.NotImplementedException();
    }
}
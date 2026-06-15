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
    private BaseCharacter myCharacter;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        gameStateModel = new GameStateModel();
        myCharacter = GetComponent<BaseCharacter>();
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

    private BaseCharacter GetClosestTarget()
    {
        BaseCharacter closest = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach(BaseCharacter character in BaseCharacter.FindObjectsByType<BaseCharacter>())
        {
            if (character == myCharacter) continue; 

            float dSqr = (character.transform.position - currentPos).sqrMagnitude;
            if (dSqr < closestDistanceSqr)
            {
                closestDistanceSqr = dSqr;
                closest = character;
            }
        }
        return closest;
    }

    void OnUseAbility()
    {
        UseAbilityCommand useAbilityCommand = new UseAbilityCommand(animator);
        ScheduleCommand(useAbilityCommand);
    }

    void OnKill()
    {
        KillCommand killCommand = new KillCommand(myCharacter, GetClosestTarget());
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
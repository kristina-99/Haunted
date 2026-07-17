using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : BaseCharacter
{
    private const float Speed = 1f;
    private Rigidbody rigidBody;
    private MoveCommand moveCommand;
    private float inputHorizontal;
    private float inputVertical;
    private static Queue<ICommand> actionQueue = new Queue<ICommand>();
    private bool canMove = true;
    private bool isNearDeadBody = false;


    private void OnEnable()
    {
        GameEvents.OnHauntedStunned += FreezeCharacter;
    }

    private void OnDisable()
    {
        GameEvents.OnHauntedStunned -= FreezeCharacter;
    }

    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            inputHorizontal = Input.GetAxis("Horizontal");
            inputVertical = Input.GetAxis("Vertical");
            moveCommand = new MoveCommand(rigidBody, inputHorizontal * Speed, inputVertical * Speed, animator);
            ScheduleCommand(moveCommand);
            ExecuteNextCommand();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        int indexLayer = LayerMask.NameToLayer("DeadBodies");

        if (other.gameObject.layer == indexLayer)
        {
            isNearDeadBody = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        int indexLayer = LayerMask.NameToLayer("DeadBodies");

        if (other.gameObject.layer == indexLayer)
        {
            isNearDeadBody = false;
        }
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

    void FreezeCharacter()
    {
        if (canMove)
        {
            StartCoroutine(FreezeRoutine());
        }
    }

    System.Collections.IEnumerator FreezeRoutine()
    {
        canMove = false;
        Debug.Log("You are stunned and can't move!");
        yield return new WaitForSeconds(30f);
        canMove = true;
        Debug.Log("You can move again!");
    }

    void OnUseAbility()
    {
        UseAbilityCommand useAbilityCommand = new UseAbilityCommand(animator, gameObject.GetComponent<RoleBase>(), transform.GetClosestTarget(this));
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

    void OnBodyReport()
    {
        if (isNearDeadBody)
        {
            ReportCommand reportCommand = new ReportCommand();
            ScheduleCommand(reportCommand);
            GameEvents.BodyReported(this);
        }
    }

    public override void OnRoleAction()
    {
        throw new System.NotImplementedException();
    }

    public void OnVoteCast(BaseCharacter target)
    {
        VoteCommand voteCommand = new VoteCommand(this,target);
        ScheduleCommand(voteCommand);
    }

}
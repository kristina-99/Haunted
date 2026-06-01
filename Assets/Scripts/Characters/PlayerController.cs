using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody rigidBody;
    private float speed = 1f;
    private MoveCommand moveCommand;
    private InteractCommand interactCommand;
    private float inputHorizontal;
    private float inputVertical;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        moveCommand = new MoveCommand(rigidBody, inputHorizontal * speed, inputVertical * speed, animator);
        moveCommand.Execute();
    }

    void OnUseAbility()
    {
        //use ability
    }

    void OnInteract()
    {
        interactCommand = new InteractCommand(rigidBody, animator);
        interactCommand.Execute();
    }
}

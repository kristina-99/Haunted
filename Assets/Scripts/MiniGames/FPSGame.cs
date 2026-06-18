using UnityEngine;
using static GameConstants;

public class FPSGame : MonoBehaviour, IMiniGame, IInteractable
{
    public CabinetConfigSO cabinetConfig;
    private MiniGameType miniGameType;
    private string displayGame;
    private float taskDuration;
    private RoomId assignedRoom;

    public MiniGameType MiniGameType 
    { 
        get
        {
            return miniGameType;
        }
    }
    public string DisplayName 
    { 
        get
        {
            return displayGame;
        }
    }
    public float TaskDuration 
    {
        get
        {
            return taskDuration;
        }
    }
    public RoomId AssignedRoom 
    {
        get
        {
            return assignedRoom;
        }
    }

    public string GetPromptText()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(PlayerController player)
    {
        //logic to start the mini game
        Debug.Log("This is the FPS Game cabinet!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        miniGameType = cabinetConfig.miniGameType;
        displayGame = cabinetConfig.displayName;
        taskDuration = cabinetConfig.taskDuration;
        assignedRoom = cabinetConfig.assignedRoom;
    }
}

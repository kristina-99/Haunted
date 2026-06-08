using UnityEngine;
using static GameConstants;

public class MKGame : MonoBehaviour, IMiniGame
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        miniGameType = cabinetConfig.miniGameType;
        displayGame = cabinetConfig.displayName;
        taskDuration = cabinetConfig.taskDuration;
        assignedRoom = cabinetConfig.assignedRoom;
    }
}

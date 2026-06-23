using UnityEngine;
using static GameConstants;

[CreateAssetMenu(fileName = "CabinetConfigSO", menuName = "Game/Cabinet Config")]
public class CabinetConfigSO : ScriptableObject
{
    public MiniGameType miniGameType;
    public string displayName;
    public float taskDuration; // seconds
    public RoomId assignedRoom;
}

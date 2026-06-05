using UnityEngine;
using static GameConstants;

[CreateAssetMenu(fileName = "MiniGameSOScript", menuName = "Scriptable Objects/MiniGameSOScript")]
public class MiniGameSOScript : ScriptableObject
{
    public MiniGameType type;
    public float duration;
    public RoomId roomId;
}

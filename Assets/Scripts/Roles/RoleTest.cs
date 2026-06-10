using Unity.VisualScripting;
using UnityEngine;
using static GameConstants;

[RequireComponent(typeof(BaseCharacter))]
public class RoleTest : MonoBehaviour
{
    private BaseCharacter player;
    public CharacterRole selectedRole;

    private void Awake()
    {
        player.Role = selectedRole;
    }
}

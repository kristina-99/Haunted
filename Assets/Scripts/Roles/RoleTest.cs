using System;
using UnityEngine;
using static GameConstants;

[RequireComponent(typeof(BaseCharacter))]
public class RoleTest : MonoBehaviour
{
    public CharacterRole selectedRole;
    private BaseCharacter player;

    private CharacterRole currentRole;

    private string roleScriptName;

    private void Start()
    {
        player = GetComponent<BaseCharacter>();
        currentRole = player.Role;
        
    }

    private void Update()
    {
        ChangeRole();
    }

    private void ChangeRole()
    {
        if (currentRole != selectedRole)
        {
            RemoveFormerScript();
            roleScriptName = $"{selectedRole}Role";
            AttachNewScript(roleScriptName);
            currentRole = selectedRole;
            player.Role = currentRole;
        }        
    }

    private void RemoveFormerScript()
    {
        RoleBase formerScript = GetComponent<RoleBase>();
            if (formerScript != null)
            {
                Destroy(formerScript);
            }
    }

    private void AttachNewScript(string roleScriptName)
    {
        Type componentType = Type.GetType($"{roleScriptName}, Assembly-CSharp");
        player.gameObject.AddComponent(componentType);
    }
}

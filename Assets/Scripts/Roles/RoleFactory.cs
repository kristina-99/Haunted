using System;
using System.Collections.Generic;
using static GameConstants;

public static class RoleFactory
{
    private static List<CharacterRole> availableRoles;
    private static List<BaseCharacter> allCharacters;
    private static string roleScriptName;
    public static void AssignRoles()
    {
        GetAllRoles();
        GetAllCharacters();
        foreach(BaseCharacter character in allCharacters)
        {
            character.Role = ChooseRole();
            roleScriptName = $"{character.Role}Role";
            Type componentType = Type.GetType($"{roleScriptName}, Assembly-CSharp");
            character.gameObject.AddComponent(componentType);
        }
    }

    private static CharacterRole ChooseRole()
    {
        if(availableRoles.Count == 0)
        {
            return CharacterRole.Normal;
        }
        else
        {
            Random random = new Random();
            int index = random.Next(availableRoles.Count);
            CharacterRole choosenRole = availableRoles[index];
            availableRoles.Remove(availableRoles[index]);
            return choosenRole;
        }
    }

    private static void GetAllCharacters()
    {
        allCharacters = new List<BaseCharacter>();
        allCharacters.AddRange(BaseCharacter.FindObjectsByType<BaseCharacter>());
    }

    private static void GetAllRoles()
    {
        availableRoles = new List<CharacterRole>((CharacterRole[])Enum.GetValues(typeof(CharacterRole)));
    }
}

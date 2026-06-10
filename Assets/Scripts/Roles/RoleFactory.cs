using System;
using System.Collections.Generic;
using static GameConstants;

public static class RoleFactory
{
    private static List<CharacterRole> availableRoles = new List<CharacterRole>((CharacterRole[])Enum.GetValues(typeof(CharacterRole)));
    public static void AssignRole()
    {
        
    }

    private static CharacterRole ChooseRole()
    {
        Random random = new Random();
        int index = random.Next(availableRoles.Count);
        CharacterRole choosenRole = availableRoles[index];
        availableRoles.Remove(availableRoles[index]);
        return choosenRole;
    }
}

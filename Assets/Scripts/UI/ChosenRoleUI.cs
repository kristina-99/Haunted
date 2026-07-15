using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Extensions;

public class ChosenRoleUI : MonoBehaviour
{
    public Image chosenRole;
    public List<Sprite> allRoles;

    void OnEnable()
    {
        GameEvents.OnArcadeMapLoaded += AssignRoleSprite;
    }

    void OnDisable()
    {
        GameEvents.OnArcadeMapLoaded -= AssignRoleSprite;
    }

    private void AssignRoleSprite()
    {
        BaseCharacter player = GetAlivePlayers().Find(player => player is PlayerController);
        //List order should match the enum order!
        chosenRole.sprite = allRoles[(int)player.Role];       
    }
}

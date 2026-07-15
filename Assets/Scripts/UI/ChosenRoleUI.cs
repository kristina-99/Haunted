using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChosenRoleUI : MonoBehaviour
{
    private Image chosenRole;
    [SerializeField] List<Sprite> allRoles;
    void Start()
    {
        BaseCharacter player = Extensions.GetAlivePlayers().Find(player => player is PlayerController);
        //List order should match the enum order!
        //chosenRole = allRoles[(int)player.Role];
    }

    void Update()
    {
        
    }
}

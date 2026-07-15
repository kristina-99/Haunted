using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Extensions;

public class ChosenRoleUI : UIPanel
{
    public Image chosenRole;
    public List<Sprite> allRoles;

    void OnEnable()
    {
        GameEvents.OnArcadeMapLoaded += AssignRoleSprite;
        GameEvents.OnGameStarted += RouteGameStarted;
    }
    void OnDisable()
    {
        GameEvents.OnArcadeMapLoaded -= AssignRoleSprite;
        GameEvents.OnGameStarted -= RouteGameStarted;
    }

    private void RouteGameStarted() 
    => StartCoroutine(ShowRoleScreen());

    void Start()
    {
        ResetUI();
    }

    private void AssignRoleSprite()
    {
        BaseCharacter player = GetAlivePlayers().Find(player => player is PlayerController);
        //List order should match the enum order!
        chosenRole.sprite = allRoles[(int)player.Role];       
    }

    private IEnumerator ShowRoleScreen()
    {
        yield return new WaitForSeconds(3f);
        Show(2f);
        yield return new WaitForSeconds(2f);
        Hide(2f);
    }
}

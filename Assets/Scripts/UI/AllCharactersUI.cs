using System.Collections;
using DG.Tweening;
using UnityEngine;
using static GameEvents;

public class AllCharactersUI : UIPanel
{
    void Start()
    {
        ResetUI();
    }

    void OnEnable()
    {
        OnGameStarted += HandleOnGameStarted;
    }

    void OnDisable()
    {
        OnGameStarted -= HandleOnGameStarted;       
    }

    private void HandleOnGameStarted()
{
    StartCoroutine(ShowAndHide());
}

    private IEnumerator ShowAndHide()
    {
        Show(1f);
        yield return new WaitForSeconds(2f);
        Hide(2f);
    }

}

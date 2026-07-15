using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SceneSequenceManager : MonoBehaviour
{
    [SerializeField] private StartMenuUI startMenuUI;
    [SerializeField] private AllCharactersUI allCharactersUI;
    [SerializeField] private ChosenRoleUI chosenRoleUI;

    void OnEnable()
    {
        GameEvents.OnGameStarted += HandleGameStarted;
    }

    void OnDisable()
    {
        GameEvents.OnGameStarted -= HandleGameStarted;
    }

    private void HandleGameStarted()
    {
        StartCoroutine(PlayUISequence());
    }

    private IEnumerator PlayUISequence()
    {
        // Step 1: Hide the Start Menu
        yield return startMenuUI.Hide(1f).WaitForCompletion();

        // Step 2: Show All Characters, wait 2 seconds, then hide
        yield return allCharactersUI.Show(1f).WaitForCompletion();
        yield return new WaitForSeconds(2f); 
        yield return allCharactersUI.Hide(2f).WaitForCompletion();

        // Step 3: Show Chosen Role, wait 2 seconds, then hide
        yield return chosenRoleUI.Show(2f).WaitForCompletion();
        yield return new WaitForSeconds(2f);
        yield return chosenRoleUI.Hide(2f).WaitForCompletion();
    }
}

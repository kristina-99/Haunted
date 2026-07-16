using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SceneSequenceManager : MonoBehaviour
{
    [SerializeField] private StartMenuPanel startMenuPanel;
    [SerializeField] private AllCharactersPanel allCharactersPanel;
    [SerializeField] private ChosenRolePanel chosenRolePanel;

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
        yield return startMenuPanel.Hide(1f).WaitForCompletion();

        // Step 2: Show All Characters, wait 2 seconds, then hide
        yield return allCharactersPanel.Show(1f).WaitForCompletion();
        yield return new WaitForSeconds(2f); 
        yield return allCharactersPanel.Hide(2f).WaitForCompletion();

        // Step 3: Show Chosen Role, wait 2 seconds, then hide
        yield return chosenRolePanel.Show(2f).WaitForCompletion();
        yield return new WaitForSeconds(2f);
        yield return chosenRolePanel.Hide(2f).WaitForCompletion();
        GameEvents.StartScenesFinished();
    }
}

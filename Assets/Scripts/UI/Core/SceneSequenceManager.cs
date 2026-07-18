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

        yield return startMenuPanel.Hide(1f).WaitForCompletion();

        yield return allCharactersPanel.Show(1f).WaitForCompletion();
        yield return new WaitForSeconds(2f); 
        yield return allCharactersPanel.Hide(2f).WaitForCompletion();

        yield return chosenRolePanel.Show(2f).WaitForCompletion();
        yield return new WaitForSeconds(2f);
        yield return chosenRolePanel.Hide(2f).WaitForCompletion();
        GameEvents.StartScenesFinished();
    }
}

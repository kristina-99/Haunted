using System.Collections;
using UnityEngine;
using static GameConstants;

public class PhaseManager : MonoBehaviour
{
    private const float NightDuration = 100f;
    private const float DayDuration = 120f;
    private int roundCounter = 0;
    
    private Coroutine activePhaseRoutine;
    private bool interruptNightPhase = false;
    private bool gameOver = false;

    void OnEnable()
    {
        GameEvents.OnBodyReported += InterruptNightPhase;
        GameEvents.OnGameEnded += GameOver;
    }

    void OnDisable()
    {
        GameEvents.OnBodyReported -= InterruptNightPhase;
        GameEvents.OnGameEnded -= GameOver;      
    }

    void Update()
    {
        if (activePhaseRoutine == null && !gameOver)
        {
            activePhaseRoutine = StartCoroutine(PhaseRoutine());
        }
    }

    IEnumerator PhaseRoutine()
    {
        roundCounter++;
        interruptNightPhase = false;
        GameEvents.NightStarted(roundCounter);

        float nightEndTime = Time.time + NightDuration;
        while (Time.time < nightEndTime && !interruptNightPhase)
        {
            yield return null; 
        }
        
        if (!gameOver)
        {
            GameEvents.DayStarted();
            yield return new WaitForSeconds(DayDuration);
        }

        activePhaseRoutine = null;
    }

    private void InterruptNightPhase(BaseCharacter reportedBody)
    {
        interruptNightPhase = true;
    }

    private void GameOver(GameResult gameResult)
    {
        gameOver = true;

        if (activePhaseRoutine != null)
        {
            StopCoroutine(activePhaseRoutine);
            activePhaseRoutine = null;
        }

        Debug.Log("The game is over and the PhaseRoutine is stopped!");
    }
}
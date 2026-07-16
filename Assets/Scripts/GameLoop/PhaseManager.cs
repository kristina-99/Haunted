using System.Collections;
using UnityEngine;
using static GameConstants;

public class PhaseManager : MonoBehaviour
{
    private const float NightDuration = 15f;
    private const float DayDuration = 120f;
    private const float TransitionDuration = 12f;
    private int roundCounter = 0;
    
    private Coroutine activePhaseRoutine;
    private bool interruptNightPhase = false;
    private bool interruptDayPhase = false;
    private bool gameStarted = false;
    private bool gameOver = false;

    public float GetDayDuration()
    {
        return DayDuration;
    }

    public float GetNightDuration()
    {
        return NightDuration;
    }

    void OnEnable()
    {
        GameEvents.OnBodyReported += InterruptNightPhase;
        GameEvents.OnGameEnded += GameOver;
        GameEvents.OnVotingFinished += RouteVotingFinished;
        GameEvents.OnStartScenesFinished += StartGame;
    }

    void OnDisable()
    {
        GameEvents.OnBodyReported -= InterruptNightPhase;
        GameEvents.OnGameEnded -= GameOver;      
        GameEvents.OnVotingFinished -= RouteVotingFinished;
        GameEvents.OnStartScenesFinished -= StartGame;
    }

    private void RouteVotingFinished(BaseCharacter votedOut, bool isTie)
    => InterruptDayPhase();

    void Update()
    {
        if (activePhaseRoutine == null && gameStarted && !gameOver)
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
        Debug.Log("The Night phase has begun");
        while (Time.time < nightEndTime && !interruptNightPhase)
        {
            yield return null; 
        }
        
        interruptDayPhase = false;
        GameEvents.DayStarted();
        float dayTimeEndTime = Time.time + DayDuration;
        Debug.Log("The Day phase has begun");
        while (Time.time < dayTimeEndTime && !interruptDayPhase)
        {
            yield return null; 
        }

        GameEvents.TransitionStarted();
        yield return new WaitForSeconds(TransitionDuration);

        activePhaseRoutine = null;
    }
    
    private void StartGame()
    {
        gameStarted = true;
        Debug.Log("Game has started");
    }

    private void InterruptNightPhase(BaseCharacter reportedBody)
    {
        Debug.Log("Night phase is interrupted");
        interruptNightPhase = true;
    }

    private void InterruptDayPhase()
    {
        Debug.Log("Voting has finished and day phase is interrupted!");
        interruptDayPhase = true;
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
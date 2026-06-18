using System.Collections;
using UnityEngine;
using static GameConstants;

public class PhaseManager : MonoBehaviour
{
    private const float NightDuration = 100f;
    private  const float DayDuration = 120f;
    private float counter = 0f;
    private int roundCounter = 0;
    private Coroutine gameLoopRoutine;
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

    void Start()
    {
        gameLoopRoutine = StartCoroutine(GameLoopRoutine());
    }

    void Update()
    {
        counter++;
        if(counter == 20f)
        {
            RoleFactory.AssignRoles();
        }

    }

    IEnumerator GameLoopRoutine()
    {
        while(!gameOver)
        {
            roundCounter++;
            interruptNightPhase = false; 
            GameEvents.NightStarted(roundCounter);

            float nightEndTime = Time.time + NightDuration;
            yield return new WaitUntil(() => Time.time >= nightEndTime || interruptNightPhase);

            if (gameOver) break; 

            GameEvents.DayStarted();
            yield return new WaitForSeconds(DayDuration);
        }
        
        Debug.Log("Game Over!");
        gameLoopRoutine = null;
    }

    private void InterruptNightPhase(BaseCharacter reportedBody)
    {
        interruptNightPhase = true;
    }

    private void GameOver(GameResult gameResult)
    {
        gameOver = true;

        if (gameLoopRoutine != null)
        {
            StopCoroutine(gameLoopRoutine);
            gameLoopRoutine = null;
        }
    }

}

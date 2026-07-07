using TMPro;
using UnityEngine;
using static GameEvents;


public class PhaseTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float remainingTime;
    private PhaseManager phaseManager;
    private string currentPhase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void OnEnable()
    {
        OnDayStarted += RestartTimerDay;
        OnNightStarted += RestartTimerNight;
        OnBodyReported += RouteBodyReported;
        OnVotingFinished += StopDayTimer;
    }

    void OnDisable()
    {
        OnDayStarted -= RestartTimerDay;
        OnNightStarted -= RestartTimerNight;
        OnBodyReported -= RouteBodyReported;
        OnVotingFinished -= StopDayTimer;
    }

    private void RouteBodyReported(BaseCharacter reporter) 
    => RestartTimerDay();

    void Start()
    {
        phaseManager = FindAnyObjectByType<PhaseManager>();
        remainingTime = phaseManager.GetNightDuration();
        currentPhase = "Night time: ";
    }

    // Update is called once per frame
    void Update()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if(remainingTime < 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = currentPhase + string.Format("{0:00}:{1:00}",minutes,seconds);
    }

    private void RestartTimerDay()
    {
        remainingTime = phaseManager.GetDayDuration();
        currentPhase = "Day time: ";
    }

    private void RestartTimerNight(int round)
    {
        remainingTime = phaseManager.GetNightDuration();
        currentPhase = "Night time: ";
    }

    private void StopDayTimer()
    {
        remainingTime = 0;
    }
}

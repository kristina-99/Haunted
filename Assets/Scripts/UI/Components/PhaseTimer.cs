using TMPro;
using UnityEngine;
using static GameEvents;


public class PhaseTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private const float SecondsPerMinute = 60;
    private float remainingTime;
    private PhaseManager phaseManager;
    private string currentPhase;

    void OnEnable()
    {
        OnStartScenesFinished += RouteGameStarted;
        OnDayStarted += RestartTimerDay;
        OnNightStarted += RestartTimerNight;
        OnBodyReported += RouteBodyReported;
        OnVotingFinished += RouteVotingFinished;
        OnTransitionStarted += HandleTransitionPhase;
    }

    void OnDisable()
    {
        OnStartScenesFinished -= RouteGameStarted;
        OnDayStarted -= RestartTimerDay;
        OnNightStarted -= RestartTimerNight;
        OnBodyReported -= RouteBodyReported;
        OnVotingFinished -= RouteVotingFinished;
        OnTransitionStarted -= HandleTransitionPhase;
    }

    private void RouteGameStarted() 
    {
        timerText.enabled = true;
        RestartTimerNight(1);
    }

    private void RouteBodyReported(BaseCharacter reporter) 
    => RestartTimerDay();

    private void RouteVotingFinished(BaseCharacter votedOut, bool isTie)
    => StopDayTimer();

    void Start()
    {
        phaseManager = FindAnyObjectByType<PhaseManager>();
        timerText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        if (currentPhase != "Transition")
        {
            int minutes = Mathf.FloorToInt(remainingTime / SecondsPerMinute);
            int seconds = Mathf.FloorToInt(remainingTime % SecondsPerMinute);
            timerText.text = currentPhase + string.Format("{0:00}:{1:00}",minutes,seconds);
        }
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

    private void HandleTransitionPhase()
    {
        timerText.text = "";
        currentPhase = "Transition";
    }
}

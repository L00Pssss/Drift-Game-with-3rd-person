using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum RaceState
{
    Passed,
    CoundDown,
    Race,
    Preparation
}
public class RaceStateTracker : MonoBehaviour
{
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction PeparationStarted;
    
    [SerializeField] private Timer m_countdownTimer;
    [SerializeField] private Timer raceTimer;

    
    public Timer CountDownTimer => m_countdownTimer;
    public Timer RaceTimer => raceTimer;
    
    private RaceState state;
    
    public RaceState State => state;
    private void Start()
    {
        StartState(RaceState.Preparation);
        
        m_countdownTimer.enabled = false;
        raceTimer.enabled = false;
        
        m_countdownTimer.Finished += OnCountdownTimerFinished;
        
        raceTimer.Finished += OnRaceTimerFinished;

    }

    private void OnDestroy()
    {
        m_countdownTimer.Finished -= OnCountdownTimerFinished;
        
        raceTimer.Finished -= OnRaceTimerFinished;

    }

    private void OnRaceTimerFinished()
    {
        Completed?.Invoke();
    }

    private void OnCountdownTimerFinished()
    {
        StartRace();
    }
    private void StartState(RaceState state)
    {
        this.state = state;
    }
    private void StartRace()
    {
        if (state != RaceState.CoundDown) return;
        StartState(RaceState.Race);

        Started?.Invoke();

        raceTimer.enabled = true;
    }
    
    public void CompleteRace()
    {
        if (state != RaceState.Race) return;
        StartState(RaceState.Passed);

        Completed?.Invoke();
    }
    
    public void StartRaceTimer()
    {
        if (state != RaceState.Preparation) return;
        StartState(RaceState.CoundDown);

        m_countdownTimer.enabled = true;
        PeparationStarted?.Invoke();
    }
    
    
}

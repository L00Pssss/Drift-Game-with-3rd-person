using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum RaceState
{
    Passed,
    CountDown,
    Race,
    Preparation
}
public class RaceStateTracker : MonoBehaviour
{
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction PreparationStarted;
    
    [SerializeField] private Timer _countDownTimer;
    [SerializeField] private Timer raceTimer;

    public Timer CountDownTimer => _countDownTimer;
    public Timer RaceTimer => raceTimer;
    
    private RaceState state;
    
    public RaceState State => state;
    private void Start()
    {
        StartState(RaceState.Preparation);
        
        _countDownTimer.enabled = false;
        raceTimer.enabled = false;
        
        _countDownTimer.Finished += OnCountdownTimerFinished;
        
        raceTimer.Finished += OnRaceTimerFinished;

    }

    private void OnDestroy()
    {
        _countDownTimer.Finished -= OnCountdownTimerFinished;
        
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
        if (state != RaceState.CountDown) return;
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
    { ;
        if (state != RaceState.Preparation) return;
        
        StartState(RaceState.CountDown);

        _countDownTimer.enabled = true;
        PreparationStarted?.Invoke();
    }
    
    
}

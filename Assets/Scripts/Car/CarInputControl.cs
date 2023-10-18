using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private RaceStateTracker _raceState;

    private bool isStart;

    private void Update()
    {
        if(_raceState.State == RaceState.Race)
        {
            _car.ThrottleControl = Input.GetAxis("Vertical");
            _car.SteerControl = Input.GetAxis("Horizontal");
            _car.BrakeControl = Input.GetAxis("Jump");
        }

        if (_raceState.State == RaceState.Passed)
        {
            _car.BrakeControl = 1;
        }
    }
}

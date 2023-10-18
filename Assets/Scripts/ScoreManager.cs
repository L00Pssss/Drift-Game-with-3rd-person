using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public event UnityAction<float> UpdateScore;

    [SerializeField] private WheelEffect _wheelEffect;
    [SerializeField] private float _timeSlider = 0.5f;
    [SerializeField] private float _coefficient = 2f;
    [SerializeField] private float _divisor = 10;

    private float slidingStartTime;
    private float slidingEndTime;

    private float score;
    
    
    private float slidingDuration;

    private int endSlidingCount;
    private int startSlidingCount;

    private void Start()
    {
        _wheelEffect.OnStartSliding += WheelEffectOnOnStartSliding;
        _wheelEffect.OnEndSliding += WheelEffectOnEndSliding;
    }

    private void OnDestroy()
    {
        _wheelEffect.OnStartSliding -= WheelEffectOnOnStartSliding;
        _wheelEffect.OnEndSliding -= WheelEffectOnEndSliding;
    }


    public float CalculateCash()
    {
        return Mathf.Floor(score / _divisor);
    }


    private void WheelEffectOnEndSliding(float time)
    {
        slidingStartTime = time;
        Calculate();
    }

    private void WheelEffectOnOnStartSliding(float time)
    {
        slidingEndTime = time;
        Calculate();
    }
    
    private void Calculate()
    {
        slidingDuration = Mathf.Abs(slidingStartTime - slidingEndTime);

        if (slidingDuration > _timeSlider)
        {
            score += slidingDuration * _coefficient;
        }
        score += slidingDuration;
        UpdateScore?.Invoke(score);
    }
}

using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class UIResult : MonoBehaviour
{
    [SerializeField] private RaceStateTracker _raceStateTracker;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private UIScore _uiScore;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _textCash;
    [SerializeField] private TextMeshProUGUI _textPoint;

    [SerializeField] private float _bonus = 2;

    private void Start()
    {
        RewardedAdsButton.Instance.UpdateScore += InstanceOnUpdateScore;
        _raceStateTracker.Completed += RaceStateTrackerOnCompleted;
    }
    private void OnDestroy()
    {
        RewardedAdsButton.Instance.UpdateScore -= InstanceOnUpdateScore;
        _raceStateTracker.Completed -= RaceStateTrackerOnCompleted;
    }

    
    private void RaceStateTrackerOnCompleted()
    {
        ShowPanel();
    }
    private void InstanceOnUpdateScore()
    {
        _textCash.text = "Cash For Drifting: " + _scoreManager.CalculateCash() * _bonus;
    }

    public void ShowPanel()
    {
        _panel.GameObject().SetActive(true);
        _textCash.text = "Cash For Drifting: " + _scoreManager.CalculateCash();
        _textPoint.text = "Points: " + _uiScore.Text.text;
        RewardedAdsButton.Instance.LoadAd();
    }

    public void RewardedAdClicked()
    {
        RewardedAdsButton.Instance.ShowAd();
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UICountDownTimer : MonoBehaviour
{
    [SerializeField] private RaceStateTracker _raceStateTracker;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    
    
    private PlayerReferences _playerReferences;

    public void Initialize(PlayerReferences playerReferences)
    {
        _playerReferences = playerReferences;
        _raceStateTracker = _playerReferences.RaceStateTracker;
        
        _playerReferences.Player.PlayerInitialized += PlayerOnPlayerInitialized;
    }

    private void PlayerOnPlayerInitialized()
    {
        _raceStateTracker.Started += OnRaceStarted;
        _raceStateTracker.PreparationStarted += OnPreparationStarted;
    }

    private void Start()
    {
        _textMeshProUGUI.enabled = false;
    }

    private void OnDestroy()
    {
        _raceStateTracker.Started -= OnRaceStarted;
        _raceStateTracker.PreparationStarted -= OnPreparationStarted;
    }

    private void OnPreparationStarted()
    {
        _textMeshProUGUI.enabled = true;
        enabled = true;
    }

    private void OnRaceStarted()
    {
        _textMeshProUGUI.enabled = false;
        enabled = false;
    }


    private void Update()
    {
        if(_raceStateTracker == null) return;
        _textMeshProUGUI.text = _raceStateTracker.CountDownTimer.Value.ToString("F0"); // ��� �� �� ���� ���� ����� �������. 

        if (_textMeshProUGUI.text == "0")
            _textMeshProUGUI.text = "GO!";
    }


}
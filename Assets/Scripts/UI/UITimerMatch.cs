using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UITimerMatch : MonoBehaviour
{
    [SerializeField] private RaceStateTracker _raceStateTracker;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private bool iStart = false;
    
    
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
    }
    
    private void OnDestroy()
    {
        _raceStateTracker.Started -= OnRaceStarted;
        _playerReferences.Player.PlayerInitialized -= PlayerOnPlayerInitialized;
    }
    private void OnRaceStarted()
    {
        iStart = true;
    }
    
    private void Update()
    {
        if(_raceStateTracker == null) return;
        if(iStart)
            _textMeshProUGUI.text = _raceStateTracker.RaceTimer.Value.ToString("F0");

        if (_textMeshProUGUI.text == "0")
        {
            _raceStateTracker.CompleteRace();
        }
    }
}
